define(['services/datacontext', 'plugins/router', 'knockout', 'services/session'], function (datacontext, router, ko, session) {
    var message = ko.observable('Welcome, this is your amazing dashboard!');
    var users = ko.observableArray([]);
    var contacts = ko.observableArray([]);
    var teacherLeads = ko.observableArray([]);
    var leadLessons = ko.observableArray([]);
    var leadContacts = ko.observableArray([]);
    var selectedLead = ko.observable();
    
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
            datacontext.getContacts(contacts),
            datacontext.getTeacherLeadsByName(session.userName(), teacherLeads)
        ]);
    };

    var viewDetails = function(lead) {
        if (lead && lead.id()) {
            var url = '#/leaddetails/' + lead.id();
            router.navigate(url);
        }
    };

    var attached = function (view) {
        bindEventToList(view, '.dashboard-lead', viewDetails);
    };

    var bindEventToList = function(rootSelector, selector, callback, eventName) {
        var eName = eventName || 'click';
        $(rootSelector).on(eName, selector, function () {
            var lead = ko.dataFor(this);
            callback(lead);
            return false;
        });
    };

    var vm = {
        activate: activate,
        attached: attached,
        users: users,
        contacts: contacts,
        teacherLeads: teacherLeads,
        title: 'Dashboard',
        message: message,
        selectedLead: selectedLead,
        leadLessons: leadLessons,
        leadContacts: leadContacts,
        leadChanged: leadChanged
    };

    return vm;
}
);