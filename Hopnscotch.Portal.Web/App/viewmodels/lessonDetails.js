define(['services/datacontext', 'plugins/router', 'knockout', 'services/session', 'datepicker'], function (datacontext, router, ko, session) {
    var lesson = ko.observable();
    
    var goBack = function () {
        router.navigateBack();
    };

    var save = function () {
        lesson().completed(true);
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

    function activate(routeData) {
        
        var id = parseInt(routeData);

        return datacontext.getLessonById(id, lesson);
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