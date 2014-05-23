define(['services/datacontext', 'knockout'], function (datacontext, ko) {
    var numberOfLeads = ko.observable();
    var numberOfContacts = ko.observable();
    var numberOfUsers = ko.observable();
    var numberOfLevels = ko.observable();

    var totals = {};
    var options = {};

    var importInProgress = ko.observable(false);
    var clearInProgress = ko.observable(false);

    options.saveImportData = ko.observable(false);
    options.simulateImport = ko.observable(false);
    options.startFromScratch = ko.observable(false);
    
    var inProgress = ko.computed(function () {
        return importInProgress() || clearInProgress();
    });

    var vm = {
        activate: activate,
        runImport: runImport,
        runClear: runClear,
        title: 'Admin',
        numberOfLeads: numberOfLeads,
        numberOfContacts: numberOfContacts,
        numberOfUsers: numberOfUsers,
        numberOfLevels: numberOfLevels,
        totals: totals,
        options: options,
        inProgress: inProgress,
        importInProgress: importInProgress,
        clearInProgress: clearInProgress,
    };

    function refreshTotals() {
        return datacontext.refreshTotals(totals);
    };

    function activate() {
        return refreshTotals();
    };

    function runImport() {
        importInProgress(true);

        return datacontext.runImport(totals, options)
            .then(function () {
                importInProgress(false);
            });
    };

    function runClear() {
        clearInProgress(true);

        return datacontext.runClear(totals)
            .then(function () {
                clearInProgress(false);
                console.log('Leads: ' + totals.numberOfLeads());
            });
    };

    return vm;
}
);