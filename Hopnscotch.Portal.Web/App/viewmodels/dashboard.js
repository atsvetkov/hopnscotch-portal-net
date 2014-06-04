define(['services/datacontext', 'plugins/router', 'knockout', 'services/session', 'config'], function (datacontext, router, ko, session, config) {
    var users = ko.observableArray([]);
    var contacts = ko.observableArray([]);
    var teacherLeads = ko.observableArray([]);

    var LeadRegisterPrinter = function() {
        var leadToPrint = ko.observable();
        var leadToPrintLessons = ko.observableArray([]);
        var leadToPrintContacts = ko.observableArray([]);
        var leadToPrintYears = ko.observableArray(['2014', '2013']);
        var leadToPrintMonths = ko.observableArray(['June', 'May', 'April']);
        var leadToPrintSelectedYear = ko.observable();
        var leadToPrintSelectedMonth = ko.observable();

        var leadToPrintDateColumns = ko.computed(function() {
            var columns = ['Jun 01, 2014', 'May 29, 2014', 'May 25, 2014'];

            return columns;
        });

        var prepareLeadForPrinting = function (lead) {
            leadToPrint(lead);

            var id = leadToPrint().id();
            return Q.all([
                datacontext.getLeadLessons(id, leadToPrintLessons),
                datacontext.getLeadContacts(id, leadToPrintContacts)
            ]).then(function () {
                console.log('- lead to print: ' + leadToPrint().name());
                console.log('- lead has ' + leadToPrintContacts().length + ' students');
                console.log('- lead has ' + leadToPrintLessons().length + ' lessons');
            });
        };

        var print = function() {
            window.print();
        };

        return {
            leadToPrint: leadToPrint,
            prepareLeadForPrinting: prepareLeadForPrinting,
            leadToPrintYears: leadToPrintYears,
            leadToPrintMonths: leadToPrintMonths,
            leadToPrintSelectedYear: leadToPrintSelectedYear,
            leadToPrintSelectedMonth: leadToPrintSelectedMonth,
            leadToPrintDateColumns: leadToPrintDateColumns,
            leadToPrintContacts: leadToPrintContacts,
            print: print
        };
    };

    var leadRegisterPrinter = new LeadRegisterPrinter();
    
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
        leadRegisterPrinter: leadRegisterPrinter,
        title: 'Dashboard',
        userIsInRole: userIsInRole
    };

    return vm;
}
);