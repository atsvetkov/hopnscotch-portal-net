define(['services/datacontext', 'plugins/router', 'knockout', 'services/session', 'datepicker'], function (datacontext, router, ko, session) {
    var lesson = ko.observable();
    var contacts = ko.observableArray([]);
    
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
        bindEventToList(view, '.attendance-row', viewDetails);

        $('.input-group.date').datepicker({
            language: "ru",
            autoclose: true,
            todayHighlight: true
        });

        $('.input-group.date').datepicker('update', lesson().date());
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
            console.log('lesson already finalized...');
        } else {
            if (!lesson().attendances || lesson().attendances().length == 0) {
                console.log('no attendances yet...');

                var attendanceStubs = [];
                $.map(contacts(), function(c, i) {
                    console.log('contact' + i + ': ' + c.name());
                    attendanceStubs.push({
                        attended: false,
                        homeworkPercentage: 0,
                        lesson: lesson(),
                        contact: c
                    });
                });

                var attendances = datacontext.createEntities('Attendance', attendanceStubs);
                console.log(attendances.length + ' new attendances have been created!');
            }
        }

        console.log('setup complete!');
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
        lesson: lesson
    };
    
    return vm;
}
);