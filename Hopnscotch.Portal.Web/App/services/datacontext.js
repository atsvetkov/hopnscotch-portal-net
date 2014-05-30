define(['services/logger', 'durandal/system', 'services/model', 'config', 'knockout'], function (logger, system, model, config, ko) {
    var EntityQuery = breeze.EntityQuery;
    var manager = configureBreezeManager();

    function queryFailed(error, status) {
        var msg = 'Error getting data. ' + error.message;
        logger.log(msg, error, system.getModuleId(datacontext), true);
    }

    function configureBreezeManager() {
        breeze.NamingConvention.camelCase.setAsDefault();

        var mgr = new breeze.EntityManager(config.serviceUrl);
        model.configureMetadataStore(mgr.metadataStore);

        return mgr;
    }

    function getLocal(resource, ordering) {
        var query = EntityQuery.from(resource)
            .orderBy(ordering);
        return manager.executeQueryLocally(query);
    }

    function getLookups() {
        return EntityQuery.from('Lookups')
            .using(manager)
            .execute()
            .fail(queryFailed);
    }

    function log(msg, data, showToast) {
        logger.log(msg, data, system.getModuleId(datacontext), showToast);
    }

    function getEntities(observable, entityName, expandedProperties, ordering, message, forceRemote) {
        if (!forceRemote) {
            var p = getLocal(entityName, ordering);
            if (p.length > 0) {
                observable(p);
                return Q.resolve();
            }
        }

        var query = EntityQuery.from(entityName)
            .orderBy(ordering);

        if (expandedProperties) {
            query = query.expand(expandedProperties);
        }

        return runQuery(query, message, observable);
    }

    function runQuery(query, message, observable) {
        return manager.executeQuery(query)
            .then(querySucceded)
            .fail(queryFailed);

        function querySucceded(data) {
            if (observable) {
                observable(data.results);
            }

            log(message, data, true);
        }
    }

    function getEntityById(entityName, entityId, observable) {
        return manager.fetchEntityByKey(entityName, entityId, true)
            .then(fetchSucceded)
            .fail(queryFailed);

        function fetchSucceded(data) {
            observable(data.entity);
        }
    }

    function runAction(actionName, mapped, options) {
        var query = EntityQuery.from(actionName);
        if (options) {
            query = query.withParameters({ options: options });
        }

        return manager.executeQuery(query)
            .then(querySucceded)
            .fail(queryFailed);

        function querySucceded(data) {
            var result = data.results[0];

            for (var prop in result) {
                if (result.hasOwnProperty(prop)) {
                    if (mapped[prop]) {
                        mapped[prop](result[prop]);
                    } else {
                        mapped[prop] = ko.observable(result[prop]);
                    }
                }
            }

            log(actionName + ' successful', data, true);
        }
    }
    
    var getLeadById = function (leadId, leadObservable) {
        return getEntityById('Lead', leadId, leadObservable);
    };

    var getContactById = function (contactId, contactObservable) {
        return getEntityById('Contact', contactId, contactObservable);
    };

    var getLessonById = function (lessonId, lessonObservable) {
        return getEntityById('Lesson', lessonId, lessonObservable);
    };

    var getUserDisplayNameByLogin = function (login, displayNameObservable) {
        if (!login) {
            displayNameObservable('');
        }

        var condition = breeze.Predicate('login', '==', login);
        var query = EntityQuery.from('Users')
            .where(condition);

        return manager.executeQuery(query)
            .then(querySucceded)
            .fail(queryFailed);

        function querySucceded(data) {
            if (!data.results || data.results.length === 0) {
                displayNameObservable(login);
                return;
            }

            var user = data.results[0];
            displayNameObservable(user.firstName() + ' ' + user.lastName());
            log('Retrieved user with login=' + login, data, true);
        }
    };

    var getLeads = function (leadsObservable, forceRemote) {
        return getEntities(leadsObservable, 'Leads', 'ResponsibleUser', 'name', 'Retrieved leads', forceRemote);
    };

    var getContacts = function (contactsObservable, forceRemote) {
        return getEntities(contactsObservable, 'Contacts', 'ResponsibleUser', 'name', 'Retrieved contacts', forceRemote);
    };

    var getUsers = function (usersObservable, forceRemote) {
        return getEntities(usersObservable, 'Users', null, 'firstName, lastName', 'Retrieved users', forceRemote);
    };

    var getTeacherLeads = function (teacherId, teacherLeadsObservable) {
        var condition = breeze.Predicate('responsibleUserId', '==', teacherId);
        var query = EntityQuery.from('Leads')
            .where(condition)
            .orderBy('name')
            .expand('ResponsibleUser');

        var message = 'Retrieved leads for teacher with ID=' + teacherId;

        return runQuery(query, message, teacherLeadsObservable);
    };

    var getTeacherLeadsByName = function (teacherName, teacherLeadsObservable) {
        var condition = breeze.Predicate('responsibleUser.login', '==', teacherName);
        var query = EntityQuery.from('Leads')
            .where(condition)
            .expand('ResponsibleUser, LanguageLevel, Status')
            .orderBy('name');

        var message = 'Retrieved leads for teacher with Name=' + teacherName;

        return runQuery(query, message, teacherLeadsObservable);
    };

    var getLeadLessons = function (leadId, lessonsObservable) {
        var condition = breeze.Predicate('leadId', '==', leadId);
        var query = EntityQuery.from('Lessons')
            .where(condition)
            .orderBy('date')
            .expand('Lead, Attendances');

        var message = 'Retrieved lessons for lead with ID=' + leadId;

        return runQuery(query, message, lessonsObservable);
    };

    var getLeadContacts = function (leadId, contactsObservable) {
        var query = EntityQuery.from('ContactsOfLead')
            .withParameters({ leadId: leadId })
            .orderBy('name');

        var message = 'Retrieved contacts for lead with ID=' + leadId;

        return runQuery(query, message, contactsObservable);
    };
    
    var primeData = function () {
        return Q.all([getLookups(), getUsers(null, true)]);
    };

    var runImport = function (totals, options) {
        return runAction('Import', totals, options);
    };

    var runClear = function (totals) {
        return runAction('Clear', totals);
    };

    var refreshTotals = function (totals) {
        return runAction('Refresh', totals);
    };

    var createEntity = function (entityType, entity) {
        return manager.createEntity(entityType, entity);
    };

    var createEntities = function (entityType, entities) {
        var results = [];
        $.map(entities, function (entity) {
            results.push(manager.createEntity(entityType, entity));
        });

        return results;
    };

    var saveChanges = function () {
        return manager.saveChanges()
            .then(saveSucceded)
            .fail(saveFailed);

        function saveSucceded(saveResult) {
            log('Saved data successfully', saveResult, true);
        }

        function saveFailed(error) {
            var msg = 'Saved failed: ' + error.message;
            logger.log(msg, error, system.getModuleId(datacontext), true);
            error.message = msg;
            throw error;
        }
    };
    
    var datacontext = {
        getLeads: getLeads,
        getContacts: getContacts,
        getUsers: getUsers,
        primeData: primeData,
        runImport: runImport,
        runClear: runClear,
        refreshTotals: refreshTotals,
        getTeacherLeads: getTeacherLeads,
        getTeacherLeadsByName: getTeacherLeadsByName,
        getLeadLessons: getLeadLessons,
        getLeadContacts: getLeadContacts,
        getLeadById: getLeadById,
        getContactById: getContactById,
        getLessonById: getLessonById,
        getUserDisplayNameByLogin: getUserDisplayNameByLogin,
        createEntity: createEntity,
        createEntities: createEntities,
        saveChanges: saveChanges
    };

    return datacontext;
});