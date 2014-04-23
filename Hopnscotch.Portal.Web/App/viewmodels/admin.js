define(['services/datacontext', 'knockout'], function (datacontext, ko) {
    var numberOfLeads = ko.observable();
    var numberOfContacts = ko.observable();
    var numberOfUsers = ko.observable();
    var numberOfLevels = ko.observable();

    var inProgress = ko.observable(false);

    var vm = {
        activate: activate,
        runImport: runImport,
        runClearImport: runClearImport,
        title: 'Admin',
        numberOfLeads: numberOfLeads,
        numberOfContacts: numberOfContacts,
        numberOfUsers: numberOfUsers,
        numberOfLevels: numberOfLevels,
        inProgress: inProgress
    };

    function refreshTotals() {
        return datacontext.refreshTotals(numberOfLeads, numberOfContacts, numberOfUsers, numberOfLevels);
    };

    function activate() {
        return refreshTotals();
    };

    function runImport() {
        inProgress(true);

        return datacontext.runImport(numberOfLeads, numberOfContacts, numberOfUsers, numberOfLevels)
            .then(function () {
                inProgress(false);
            });
    };

    function runClearImport() {
        inProgress(true);

        return datacontext.runClearImport(numberOfLeads, numberOfContacts, numberOfUsers, numberOfLevels)
            .then(function () {
                inProgress(false);
            });
    };

    return vm;
}
);