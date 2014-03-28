define(['services/dataservice'], function (dataservice) {
    var contacts = ko.observableArray([]);
    var initialized = false;

    var vm = {
        activate: activate,
        contacts: contacts,
        title: 'Contacts',
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
        return dataservice.getContacts(contacts);
    };

    return vm;
}
);