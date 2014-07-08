define(['services/datacontext', 'services/viewHelper', 'knockout'], function (datacontext, viewHelper, ko) {
    var leads = ko.observableArray([]);

    var attached = function (view) {
        viewHelper.bindEntityDetailsRouteOnClick(view, '.lead-row', '#/leaddetails/');
    };
    
    function activate() {
        return datacontext.getLeads(leads);
    };

    function refresh() {
        return datacontext.getLeads(leads, true);
    };

    var vm = {
        activate: activate,
        attached: attached,
        leads: leads,
        title: 'Leads',
        refresh: refresh
    };

    return vm;
}
);