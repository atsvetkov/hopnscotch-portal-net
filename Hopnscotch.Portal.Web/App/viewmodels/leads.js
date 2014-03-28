define(['services/dataservice'], function (dataservice) {
    var leads = ko.observableArray([]);
    var initialized = false;

    var vm = {
        activate: activate,
        leads: leads,
        title: 'Leads',
        refresh: refresh
    };

    function activate() {
        if (initialized) {
            return true;
        }

        initialized = true;
        return refresh();
    };

    function refresh() {
        return dataservice.getLeads(leads);
    };

    return vm;
}
);