define(['services/logger', 'durandal/system', 'services/model', 'config'], function (logger, system, model, config) {
    var EntityQuery = breeze.EntityQuery;
    var manager = configureBreezeManager();
    var getLeads = function (leadsObservable) {
        var query = EntityQuery.from('Leads')
            .orderBy('name');

        return manager.executeQuery(query)
            .then(querySucceded)
            .fail(queryFailed);

        function querySucceded(data) {
            if (leadsObservable) {
                leadsObservable(data.results);
            }

            log('Retrieved leads', data, true);
        }
    };

    var getContacts = function (contactsObservable) {
        var query = EntityQuery.from('Contacts')
            .orderBy('name');

        return manager.executeQuery(query)
            .then(querySucceded)
            .fail(queryFailed);

        function querySucceded(data) {
            if (contactsObservable) {
                contactsObservable(data.results);
            }

            log('Retrieved contacts', data, true);
        }
    };

    var getUsers = function (usersObservable) {
        var query = EntityQuery.from('Users')
            .orderBy('firstName, lastName');

        return manager.executeQuery(query)
            .then(querySucceded)
            .fail(queryFailed);

        function querySucceded(data) {
            if (usersObservable) {
                usersObservable(data.results);
            }

            log('Retrieved users', data, true);
        }
    };

    var primeData = function () {
        return Q.all([getLookups(), getUsers()]);
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

    var datacontext = {
        getLeads: getLeads,
        getContacts: getContacts,
        getUsers: getUsers,
        primeData: primeData,
        runImport: runImport,
        refreshTotals: refreshTotals
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