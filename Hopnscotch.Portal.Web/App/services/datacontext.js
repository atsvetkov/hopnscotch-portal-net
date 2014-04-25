define(['services/logger', 'durandal/system', 'services/model', 'config', 'knockout'], function (logger, system, model, config, ko) {
    var EntityQuery = breeze.EntityQuery;
    var manager = configureBreezeManager();

    var getLeads = function (leadsObservable, forceRemote) {
        return getEntities(leadsObservable, 'Leads', 'ResponsibleUser', 'name', 'Retrieved leads', forceRemote);
    };

    var getLeadById = function (leadId, leadObservable) {
        return manager.fetchEntityByKey('Lead', leadId, true)
            .then(fetchSucceded)
            .fail(queryFailed);

        function fetchSucceded(data) {
            leadObservable(data.entity);
        }
    };

    var getLessonById = function (lessonId, lessonObservable) {
        return manager.fetchEntityByKey('Lesson', lessonId, true)
            .then(fetchSucceded)
            .fail(queryFailed);

        function fetchSucceded(data) {
            lessonObservable(data.entity);
        }
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

        return manager.executeQuery(query)
            .then(querySucceded)
            .fail(queryFailed);

        function querySucceded(data) {
            if (teacherLeadsObservable) {
                teacherLeadsObservable(data.results);
            }

            log('Retrieved leads for teacher with ID=' + teacherId, data, true);
        }
    };

    var getTeacherLeadsByName = function (teacherName, teacherLeadsObservable) {
        var condition = breeze.Predicate('responsibleUser.login', '==', teacherName);
        var query = EntityQuery.from('Leads')
            .where(condition)
            .expand('ResponsibleUser')
            .orderBy('name');

        return manager.executeQuery(query)
            .then(querySucceded)
            .fail(queryFailed);

        function querySucceded(data) {
            if (teacherLeadsObservable) {
                teacherLeadsObservable(data.results);
            }

            log('Retrieved leads for teacher with Name=' + teacherName, data, true);
        }
    };

    var getLeadLessons = function (leadId, lessonsObservable) {
        var condition = breeze.Predicate('leadId', '==', leadId);
        var query = EntityQuery.from('Lessons')
            .where(condition)
            .orderBy('date')
            .expand('Lead, Attendances');

        return manager.executeQuery(query)
            .then(querySucceded)
            .fail(queryFailed);

        function querySucceded(data) {
            if (lessonsObservable) {
                lessonsObservable(data.results);
            }

            log('Retrieved lessons for lead with ID=' + leadId, data, true);
        }
    };

    var getLeadContacts = function (leadId, contactsObservable) {
        var query = EntityQuery.from('ContactsOfLead')
            .withParameters({ leadId: leadId })
            .orderBy('name');

        return manager.executeQuery(query)
            .then(querySucceded)
            .fail(queryFailed);

        function querySucceded(data) {
            if (contactsObservable) {
                contactsObservable(data.results);
            }

            log('Retrieved contacts for lead with ID=' + leadId, data, true);
        }
    };
    
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

    var primeData = function () {
        return Q.all([getLookups(), getUsers(null, true)]);
    };

    var runImport = function (numberOfLeads, numberOfContacts, numberOfUsers, numberOfLevels) {
        var query = EntityQuery.from('Import');

        return manager.executeQuery(query)
            .then(querySucceded)
            .fail(queryFailed);

        function querySucceded(data) {
            var result = data.results[0];

            numberOfLeads(result.numberOfLeads);
            numberOfContacts(result.numberOfContacts);
            numberOfUsers(result.numberOfUsers);
            numberOfLevels(result.numberOfLevels);

            log('Import successful', data, true);
        }
    };

    var runClearImport = function (numberOfLeads, numberOfContacts, numberOfUsers, numberOfLevels) {
        var query = EntityQuery.from('ClearImport');

        return manager.executeQuery(query)
            .then(querySucceded)
            .fail(queryFailed);

        function querySucceded(data) {
            var result = data.results[0];

            numberOfLeads(result.numberOfLeads);
            numberOfContacts(result.numberOfContacts);
            numberOfUsers(result.numberOfUsers);
            numberOfLevels(result.numberOfLevels);

            log('Clear/Import successful', data, true);
        }
    };

    var runClear = function (numberOfLeads, numberOfContacts, numberOfUsers, numberOfLevels) {
        var query = EntityQuery.from('Clear');

        return manager.executeQuery(query)
            .then(querySucceded)
            .fail(queryFailed);

        function querySucceded(data) {
            var result = data.results[0];

            numberOfLeads(result.numberOfLeads);
            numberOfContacts(result.numberOfContacts);
            numberOfUsers(result.numberOfUsers);
            numberOfLevels(result.numberOfLevels);

            log('Clear successful', data, true);
        }
    };

    var refreshTotals = function (numberOfLeads, numberOfContacts, numberOfUsers, numberOfLevels) {
        var query = EntityQuery.from('Refresh');

        return manager.executeQuery(query)
            .then(querySucceded)
            .fail(queryFailed);

        function querySucceded(data) {
            var result = data.results[0];

            numberOfLeads(result.numberOfLeads);
            numberOfContacts(result.numberOfContacts);
            numberOfUsers(result.numberOfUsers);
            numberOfLevels(result.numberOfLevels);

            log('Refresh successful', data, true);
        }
    };

    var saveChanges = function() {
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
        runClearImport: runClearImport,
        runClear: runClear,
        refreshTotals: refreshTotals,
        getTeacherLeads: getTeacherLeads,
        getTeacherLeadsByName: getTeacherLeadsByName,
        getLeadLessons: getLeadLessons,
        getLeadContacts: getLeadContacts,
        getLeadById: getLeadById,
        getLessonById: getLessonById,
        getUserDisplayNameByLogin: getUserDisplayNameByLogin,
        saveChanges: saveChanges
    };

    return datacontext;

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
});