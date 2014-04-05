define(['services/datacontext', 'knockout'], function (datacontext, ko) {
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
        return datacontext.getContacts(contacts);
    };

    return vm;
}
);