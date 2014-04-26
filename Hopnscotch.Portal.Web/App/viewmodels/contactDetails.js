define(['services/datacontext', 'plugins/router', 'knockout', 'services/session'], function (datacontext, router, ko, session) {
    var contact = ko.observable();
    
    var goBack = function () {
        router.navigateBack();
    };
    
    //var viewDetails = function (lesson) {
    //    if (lesson && lesson.id()) {
    //        var url = '#/lessondetails/' + lesson.id();
    //        console.log(url);
    //        router.navigate(url);
    //    }
    //};

    //var attached = function (view) {
    //    bindEventToList(view, '.lead-lesson', viewDetails);
    //};

    //var bindEventToList = function (rootSelector, selector, callback, eventName) {
    //    var eName = eventName || 'click';
    //    $(rootSelector).on(eName, selector, function () {
    //        var lesson = ko.dataFor(this);
    //        callback(lesson);
    //        return false;
    //    });
    //};

    function activate(routeData) {
        var id = parseInt(routeData);

        return Q.all([
            datacontext.getContactById(id, contact)
            //datacontext.getLeadLessons(id, leadLessons),
            //datacontext.getLeadContacts(id, leadContacts)
        ]);
    }

    var vm = {
        activate: activate,
        //attached: attached,
        goBack: goBack,
        title: 'Contact Details',
        contact: contact
    };
    
    return vm;
}
);