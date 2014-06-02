define(['services/datacontext', 'plugins/router', 'knockout', 'services/session', 'datepicker'], function (datacontext, router, ko, session) {
    var lesson = ko.observable();
    var contacts = ko.observableArray([]);

    var homeworkResultOptions = [0.0, 0.25, 0.50, 0.75, 1.0];
    
    var goBack = function () {
        router.navigateBack();
    };

    var save = function () {
        lesson().finalized(true);
        datacontext.saveChanges()
            .then(goBack);
    };

    function toggleLessonAttendances(visited) {
        if (lesson() && lesson().attendances()) {
            $.map(lesson().attendances(), function (attendance) {
                attendance.attended(visited);
            });
        }
    }

    var selectAll = function () {
        toggleLessonAttendances(true);
    };

    var selectNone = function () {
        toggleLessonAttendances(false);
    };
    
    var viewDetails = function (attendance) {
        if (attendance && attendance.id()) {
            // switch attendance state
            attendance.attended(!attendance.attended());
        }
    };

    var attached = function (view) {
        if (lesson().finalized()) {
            return;
        }

        bindEventToList(view, '.attendance-row', viewDetails);
    };

    var bindEventToList = function (rootSelector, selector, callback, eventName) {
        var eName = eventName || 'click';
        $(rootSelector).on(eName, selector, function () {
            var attendance = ko.dataFor(this);
            callback(attendance);

            return false;
        });
    };

    var setupAttendances = function() {
        if (lesson().finalized()) {
        } else {
            if (!lesson().attendances || lesson().attendances().length == 0) {
                var attendanceStubs = [];
                $.map(contacts(), function(c, i) {
                    attendanceStubs.push({
                        attended: false,
                        homeworkPercentage: 0,
                        lesson: lesson(),
                        contact: c
                    });
                });

                datacontext.createEntities('Attendance', attendanceStubs);
            }
        }

        return;
    };

    function activate(routeData) {
        var id = parseInt(routeData);

        return datacontext.getLessonById(id, lesson)
            .then(function() { return datacontext.getLeadContacts(lesson().lead().id(), contacts); })
            .then(setupAttendances);
    }

    var vm = {
        activate: activate,
        attached: attached,
        goBack: goBack,
        save: save,
        selectAll: selectAll,
        selectNone: selectNone,
        title: 'Lesson Details',
        lesson: lesson,
        homeworkResultOptions: homeworkResultOptions
    };
    
    return vm;
}
);