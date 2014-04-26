define(['services/datacontext', 'plugins/router', 'knockout', 'services/session'], function (datacontext, router, ko, session) {
    var lead = ko.observable();
    var leadLessons = ko.observableArray([]);
    var leadContacts = ko.observableArray([]);

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
        leadContacts: leadContacts
    };
    
    return vm;
}
);