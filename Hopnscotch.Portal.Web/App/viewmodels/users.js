define(['services/datacontext', 'knockout'], function (datacontext, ko) {
    var users = ko.observableArray([]);

    var vm = {
        activate: activate,
        users: users,
        title: 'Users',
        refresh: refresh
    };

    function activate() {
        return datacontext.getUsers(users);
    };

    function refresh() {
        return datacontext.getUsers(users, true);
    };

    return vm;
}
);