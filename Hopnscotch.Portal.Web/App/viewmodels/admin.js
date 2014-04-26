define(['services/datacontext', 'knockout'], function (datacontext, ko) {
    var numberOfLeads = ko.observable();
    var numberOfContacts = ko.observable();
    var numberOfUsers = ko.observable();
    var numberOfLevels = ko.observable();

    var totals = {};

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
        totals: totals,
        inProgress: inProgress,
        importInProgress: importInProgress,
        clearInProgress: clearInProgress,
        clearImportInProgress: clearImportInProgress
    };

    function refreshTotals() {
        return datacontext.refreshTotals(totals);
    };

    function activate() {
        return refreshTotals();
    };

    function runImport() {
        importInProgress(true);

        return datacontext.runImport(totals)
            .then(function () {
                importInProgress(false);
            });
    };

    function runClearImport() {
        clearImportInProgress(true);

        return datacontext.runClearImport(totals)
            .then(function () {
                clearImportInProgress(false);
            });
    };

    function runClear() {
        clearInProgress(true);

        return datacontext.runClear(totals)
            .then(function () {
                clearInProgress(false);
            });
    };

    return vm;
}
);