define(['services/datacontext', 'knockout'], function (datacontext, ko) {
    var message = ko.observable('Welcome, this is your amazing dashboard!');
    var users = ko.observableArray([]);
    var contacts = ko.observableArray([]);
    var teacherLeads = ko.observableArray([]);
    var leadLessons = ko.observableArray([]);
    var leadContacts = ko.observableArray([]);
    var selectedUser = ko.observable();
    var selectedLead = ko.observable();

    var vm = {
        activate: activate,
        users: users,
        contacts: contacts,
        teacherLeads: teacherLeads,
        title: 'Dashboard',
        message: message,
        selectedUser: selectedUser,
        selectedLead: selectedLead,
        leadLessons: leadLessons,
        leadContacts: leadContacts,
        userChanged: userChanged,
        leadChanged: leadChanged
    };

    function userChanged() {
        return datacontext.getTeacherLeads(selectedUser().id(), teacherLeads);
    }

    function leadChanged() {
        return Q.all([
            datacontext.getLeadLessons(selectedLead().id(), leadLessons),
            datacontext.getLeadContacts(selectedLead().id(), leadContacts)]);
    }

    function activate() {
        return refresh();
    };

    function refresh() {
        return Q.all([
            datacontext.getUsers(users),
            datacontext.getContacts(contacts)
        ]);
    };

    return vm;
}
);