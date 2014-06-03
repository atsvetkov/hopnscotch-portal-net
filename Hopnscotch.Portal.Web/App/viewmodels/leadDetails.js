define(['services/datacontext', 'plugins/router', 'knockout', 'services/session'], function (datacontext, router, ko, session) {
    var lead = ko.observable();
    var leadLessons = ko.observableArray([]);
    var leadContacts = ko.observableArray([]);
    var progress = function () {
        var lessonsOfStatus = function (status) {
            return ko.utils.arrayFilter(leadLessons(), function (lesson) {
                return lesson.status() === status;
            });
        }

        var lessonsTotal = ko.computed(function () {
            return leadLessons().length;
        });

        var lessonsPassed = ko.computed(function () {
            return ko.utils.arrayFilter(leadLessons(), function (lesson) {
                return moment(lesson.date()).isBefore(moment());
            }).length;
        });
        
        var lessonsPassedPercentage = ko.computed(function () {
            return (lessonsPassed() / lessonsTotal() * 100) + '%';
        });

        var lessonsPassedText = ko.computed(function () {
            return lessonsPassed() + '/' + lessonsTotal() + ' lessons passed';
        });

        var lessonsCompleted = ko.computed(function () {
            return lessonsOfStatus('Completed').length;
        });

        var lessonsCompletedPercentage = ko.computed(function () {
            return (lessonsCompleted() / lessonsTotal() * 100) + '%';
        });

        var lessonsCompletedText = ko.computed(function () {
            return lessonsCompleted() + '/' + lessonsTotal() + ' lessons completed';
        });

        var getCurrentStudentAttendance = function (contactId) {
            var alreadyCompletedLessons = lessonsOfStatus('Completed');
            var lessonsAttended = ko.utils.arrayFilter(alreadyCompletedLessons, function (lesson) {
                if (!lesson.attendances) {
                    return false;
                }

                var match = ko.utils.arrayFirst(lesson.attendances(), function (attendance) {
                    return attendance.contactId() === contactId;
                });

                if (!match) {
                    return false;
                }

                return match.attended();
            });

            return lessonsAttended.length + "/" + alreadyCompletedLessons.length;
        };

        return {
            lessonsTotal: lessonsTotal,
            lessonsPassed: lessonsPassed,
            lessonsPassedPercentage: lessonsPassedPercentage,
            lessonsPassedText: lessonsPassedText,
            lessonsCompleted: lessonsCompleted,
            lessonsCompletedPercentage: lessonsCompletedPercentage,
            lessonsCompletedText: lessonsCompletedText,
            getCurrentStudentAttendance: getCurrentStudentAttendance
        };
    }
    
    var goBack = function () {
        router.navigateBack();
    };
    
    var viewDetails = function (entity, urlPrefix) {
        if (entity && entity.id()) {
            var url = urlPrefix + entity.id();
            router.navigate(url);
        }
    };

    var viewLessonDetails = function (lesson) {
        viewDetails(lesson, '#/lessondetails/');
    };

    var viewContactDetails = function (contact) {
        viewDetails(contact, '#/contactdetails/');
    };

    var attached = function (view) {
        bindEventToList(view, '.lead-lesson', viewLessonDetails);
        bindEventToList(view, '.contact-lesson', viewContactDetails);
    };

    var bindEventToList = function (rootSelector, selector, callback, eventName) {
        var eName = eventName || 'click';
        $(rootSelector).on(eName, selector, function () {
            var entity = ko.dataFor(this);
            callback(entity);
            return false;
        });
    };

    function activate(routeData) {
        var id = parseInt(routeData);

        return Q.all([
            datacontext.getLeadById(id, lead),
            datacontext.getLeadLessons(id, leadLessons),
            datacontext.getLeadContacts(id, leadContacts)
        ]);
    }

    var vm = {
        activate: activate,
        attached: attached,
        goBack: goBack,
        title: 'Lead Details',
        lead: lead,
        leadLessons: leadLessons,
        leadContacts: leadContacts,
        progress: progress()
    };
    
    return vm;
}
);