define(['services/datacontext', 'knockout'], function (datacontext, ko) {
    var leads = ko.observableArray([]);

    var vm = {
        activate: activate,
        leads: leads,
        title: 'Leads',
        refresh: refresh
    };

    function activate() {
        return datacontext.getLeads(leads);
    };

    function refresh() {
        return datacontext.getLeads(leads, true);
    };

    return vm;
}
);