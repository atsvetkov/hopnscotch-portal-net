define(['services/dataservice', 'knockout'], function (dataservice, ko) {
    var numberOfLeads = ko.observable();
    var numberOfContacts = ko.observable();
    var numberOfUsers = ko.observable();

    var vm = {
        activate: activate,
        runImport: runImport,
        title: 'Admin',
        numberOfLeads: numberOfLeads,
        numberOfContacts: numberOfContacts,
        numberOfUsers: numberOfUsers
    };

    function refreshTotals() {
        return dataservice.refreshTotals(numberOfLeads, numberOfContacts, numberOfUsers);
    };

    function activate() {
        return refreshTotals();
    };

    function runImport() {
        return dataservice.runImport(numberOfLeads, numberOfContacts, numberOfUsers);
    };

    return vm;
}
);