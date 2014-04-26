define(['services/datacontext', 'plugins/router', 'knockout', 'services/session', 'config'], function (datacontext, router, ko, session, config) {
    var users = ko.observableArray([]);
    var contacts = ko.observableArray([]);
    var teacherLeads = ko.observableArray([]);
    
    function activate() {
        return refresh();
    };

    function refreshTeacherData() {
        return Q.all([
            datacontext.getUsers(users),
            datacontext.getContacts(contacts),
            datacontext.getTeacherLeadsByName(session.userName(), teacherLeads)
        ]);
    };

    function refreshAdminData() {
    };

    function refreshManagerData() {
    };

    function refreshStudentData() {
    };

    function refresh() {
        $.map(session.userRoles(), function(role) {
            if (role === config.roleNames.Teachers) {
                return refreshTeacherData();
            } else if (role === config.roleNames.Administrators) {
                return refreshAdminData();
            } else if (role === config.roleNames.Managers) {
                return refreshManagerData();
            } else if (role === config.roleNames.Students) {
                return refreshStudentData();
            }

            return Q.resolve();
        });
    };

    var viewDetails = function(lead) {
        if (lead && lead.id()) {
            var url = '#/leaddetails/' + lead.id();
            router.navigate(url);
        }
    };

    var attached = function (view) {
        bindEventToList(view, '.lead-row', viewDetails);
    };

    var bindEventToList = function(rootSelector, selector, callback, eventName) {
        var eName = eventName || 'click';
        $(rootSelector).on(eName, selector, function () {
            var lead = ko.dataFor(this);
            callback(lead);
            return false;
        });
    };

    var userIsInRole = function(role) {
        return session.userIsInRole(role);
    };

    var vm = {
        activate: activate,
        attached: attached,
        users: users,
        contacts: contacts,
        teacherLeads: teacherLeads,
        title: 'Dashboard',
        userIsInRole: userIsInRole
    };

    return vm;
}
);