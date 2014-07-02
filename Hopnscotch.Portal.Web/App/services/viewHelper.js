define(['jquery', 'knockout', 'plugins/router'], function ($, ko, router) {
    var bindEventToList = function (rootSelector, selector, callback, eventName) {
        var eName = eventName || 'click';
        $(rootSelector).on(eName, selector, function () {
            var record = ko.dataFor(this);
            callback(record);

            return false;
        });
    };

    var viewDetails = function (entity, urlPrefix) {
        if (entity && entity.id()) {
            var url = urlPrefix + entity.id();
            router.navigate(url);
        }
    };

    var bindEntityDetailsRouteOnClick = function (rootSelector, selector, urlPrefix) {
        $(rootSelector).on('click', selector, function () {
            var entity = ko.dataFor(this);
            if (entity && entity.id()) {
                var url = urlPrefix + entity.id();
                router.navigate(url);
            }

            return false;
        });
    };

    var helper = {
        bindEventToList: bindEventToList,
        viewDetails: viewDetails,
        bindEntityDetailsRouteOnClick: bindEntityDetailsRouteOnClick
    };

    return helper;
});