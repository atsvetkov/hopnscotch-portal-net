define(['services/datacontext', 'services/viewHelper', 'knockout'], function (datacontext, viewHelper, ko) {
    var users = ko.observableArray([]);

    var attached = function (view) {
        viewHelper.bindEntityDetailsRouteOnClick(view, '.user-row', '#/userdetails/');
    };

    function activate() {
        return datacontext.getUsers(users);
    };

    function refresh() {
        return datacontext.getUsers(users, true);
    };

    var vm = {
        activate: activate,
        attached: attached,
        users: users,
        title: 'Users',
        refresh: refresh
    };

    return vm;
}
);