define(['services/datacontext', 'knockout'], function (datacontext, ko) {
    var contacts = ko.observableArray([]);

    var vm = {
        activate: activate,
        contacts: contacts,
        title: 'Contacts',
        refresh: refresh
    };

    function activate() {
        return datacontext.getContacts(contacts);
    };

    function refresh() {
        return datacontext.getContacts(contacts, true);
    };

    return vm;
}
);