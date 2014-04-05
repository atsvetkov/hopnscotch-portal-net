define(['services/datacontext', 'knockout'], function (datacontext, ko) {
    var numberOfLeads = ko.observable();
    var numberOfContacts = ko.observable();
    var numberOfUsers = ko.observable();
    var numberOfLevels = ko.observable();

    var vm = {
        activate: activate,
        runImport: runImport,
        title: 'Admin',
        numberOfLeads: numberOfLeads,
        numberOfContacts: numberOfContacts,
        numberOfUsers: numberOfUsers,
        numberOfLevels: numberOfLevels
    };

    function refreshTotals() {
        return datacontext.refreshTotals(numberOfLeads, numberOfContacts, numberOfUsers, numberOfLevels);
    };

    function activate() {
        return refreshTotals();
    };

    function runImport() {
        return datacontext.runImport(numberOfLeads, numberOfContacts, numberOfUsers, numberOfLevels);
    };

    return vm;
}
);