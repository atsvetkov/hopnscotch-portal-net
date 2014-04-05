define(['services/datacontext', 'knockout'], function (datacontext, ko) {
    var users = ko.observableArray([]);
    var initialized = false;

    var vm = {
        activate: activate,
        users: users,
        title: 'Users',
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
        return datacontext.getUsers(users);
    };

    return vm;
}
);