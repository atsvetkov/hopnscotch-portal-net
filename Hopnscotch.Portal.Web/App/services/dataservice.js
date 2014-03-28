define(['services/logger', 'durandal/system', 'services/model'], function (logger, system, model) {
    var getLeads = function (leadsObservable) {
        leadsObservable([]);
        var options = {
            url: 'api/leads',
            type: 'GET',
            dataType: 'json'
        };

        function querySucceded(data) {
            var leads = [];
            data.sort(sortLeads);
            data.forEach(function (item) {
                var leadModel = new model.Lead(item);
                leads.push(leadModel);
            });

            leadsObservable(leads);
            log('Retrieved leads', leads, true);
        }

        return $.ajax(options)
            .then(querySucceded)
            .fail(queryFailed);
    };

    var getContacts = function (contactsObservable) {
        contactsObservable([]);
        var options = {
            url: 'api/contacts',
            type: 'GET',
            dataType: 'json'
        };

        function querySucceded(data) {
            var contacts = [];
            data.sort(sortContacts);
            data.forEach(function (item) {
                var contactModel = new model.Contact(item);
                contacts.push(contactModel);
            });

            contactsObservable(contacts);
            log('Retrieved contacts', contacts, true);
        }

        return $.ajax(options)
            .then(querySucceded)
            .fail(queryFailed);
    };

    var dataservice = {
        getLeads: getLeads,
        getContacts: getContacts
    };

    return dataservice;

    function queryFailed(xhr, status) {
        var msg = 'Error getting data. ' + status;
        logger.log(msg, data, system.getModuleId(dataservice), true);
    }

    function log(msg, data, showToast) {
        logger.log(msg, data, system.getModuleId(dataservice), showToast);
    }

    function sortLeads(l1, l2) {
        return l1.name > l2.name ? 1 : -1;
    }

    function sortContacts(c1, c2) {
        return c1.name > c2.name ? 1 : -1;
    }
});