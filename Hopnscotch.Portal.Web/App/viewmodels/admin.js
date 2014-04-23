define(['services/datacontext', 'knockout'], function (datacontext, ko) {
    var numberOfLeads = ko.observable();
    var numberOfContacts = ko.observable();
    var numberOfUsers = ko.observable();
    var numberOfLevels = ko.observable();

    var importInProgress = ko.observable(false);
    var clearInProgress = ko.observable(false);
    var clearImportInProgress = ko.observable(false);

    var inProgress = ko.computed(function () {
        return importInProgress() || clearInProgress() || clearImportInProgress();
    });

    var vm = {
        activate: activate,
        runImport: runImport,
        runClearImport: runClearImport,
        runClear: runClear,
        title: 'Admin',
        numberOfLeads: numberOfLeads,
        numberOfContacts: numberOfContacts,
        numberOfUsers: numberOfUsers,
        numberOfLevels: numberOfLevels,
        inProgress: inProgress,
        importInProgress: importInProgress,
        clearInProgress: clearInProgress,
        clearImportInProgress: clearImportInProgress
    };

    function refreshTotals() {
        return datacontext.refreshTotals(numberOfLeads, numberOfContacts, numberOfUsers, numberOfLevels);
    };

    function activate() {
        return refreshTotals();
    };

    function runImport() {
        importInProgress(true);

        return datacontext.runImport(numberOfLeads, numberOfContacts, numberOfUsers, numberOfLevels)
            .then(function () {
                importInProgress(false);
            });
    };

    function runClearImport() {
        clearImportInProgress(true);

        return datacontext.runClearImport(numberOfLeads, numberOfContacts, numberOfUsers, numberOfLevels)
            .then(function () {
                clearImportInProgress(false);
            });
    };

    function runClear() {
        clearInProgress(true);

        return datacontext.runClear(numberOfLeads, numberOfContacts, numberOfUsers, numberOfLevels)
            .then(function () {
                clearInProgress(false);
            });
    };

    return vm;
}
);