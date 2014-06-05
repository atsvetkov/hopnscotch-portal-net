define(['services/datacontext', 'plugins/router', 'knockout', 'services/session', 'config'], function (datacontext, router, ko, session, config) {
    var users = ko.observableArray([]);
    var contacts = ko.observableArray([]);
    var teacherLeads = ko.observableArray([]);

    var lessonPeriod = function(m, y) {
        var month = ko.observable(m);
        var year = ko.observable(y);
        var displayValue = ko.computed(function() {
            return month() + ' ' + year();
        });

        return {
            month: month,
            year: year,
            displayValue: displayValue
        }
    };

    var LeadRegisterPrinter = function() {
        var leadToPrint = ko.observable();
        var leadToPrintLessons = ko.observableArray([]);
        var leadToPrintContacts = ko.observableArray([]);
        var leadToPrintSelectedPeriod = ko.observable();
        var leadToPrintPeriods = ko.observableArray([]);
        
        var setup = function() {
            // TODO: Optimize this by pre-creating a dictionary of lesson dates grouped by month+year combination
            var periods = [];
            var periodHashes = [];
            $.map(leadToPrintLessons(), function (lesson) {
                var lessonDate = moment(lesson.date());
                var period = new lessonPeriod(lessonDate.format('MMM'), lessonDate.year());
                var hash = period.displayValue();
                if ($.inArray(hash, periodHashes) === -1) {
                    periods.push(period);
                    periodHashes.push(hash);
                }
            });

            leadToPrintPeriods(periods);

            // set initial period to current month/year if exists
            var today = moment();
            var todayHash = new lessonPeriod(today.format('MMM'), today.year()).displayValue();
            $.map(leadToPrintPeriods(), function (period) {
                if (period.displayValue() === todayHash) {
                    leadToPrintSelectedPeriod(period);
                }
            });
        };
        
        var leadToPrintDateColumns = ko.computed(function() {
            var columns = [];
            $.map(leadToPrintLessons(), function(lesson) {
                var lessonDate = moment(lesson.date());
                if (lessonDate.year() == leadToPrintSelectedPeriod().year() && lessonDate.format('MMM') == leadToPrintSelectedPeriod().month()) {
                    columns.push(lessonDate.format('DD.MM.YY'));
                }
            });

            return columns;
        });

        var prepareLeadForPrinting = function (lead) {
            leadToPrint(lead);

            var id = leadToPrint().id();
            return Q.all([
                datacontext.getLeadLessons(id, leadToPrintLessons),
                datacontext.getLeadContacts(id, leadToPrintContacts)
            ]).then(function () {
                setup();
            });
        };

        var print = function() {
            window.print();
        };

        return {
            leadToPrint: leadToPrint,
            prepareLeadForPrinting: prepareLeadForPrinting,
            leadToPrintPeriods: leadToPrintPeriods,
            leadToPrintSelectedPeriod: leadToPrintSelectedPeriod,
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