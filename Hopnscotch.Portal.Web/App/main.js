require.config({
    paths: {
        "text": "../App/durandal/amd/text",
        "plugins": "../App/durandal/plugins",
        "knockout": "../scripts/knockout-3.1.0",
        "jquery": "../scripts/jquery-2.1.0"
    }
});

define(function(require) {
    var system = require('durandal/system');
    var app = require('durandal/app');
    var router = require('durandal/plugins/router');
    var viewLocator = require('durandal/viewLocator');
    var logger = require('services/logger');

    system.debug(true);

    app.configurePlugins({
        router: true
    });

    app.start().then(function () {
        //router.useConvention();
        viewLocator.useConvention();

        app.setRoot('viewmodels/shell');

        router.handleInvalidRoute = function (route, params) {
            logger.logError('No route found', route, 'main', true);
        };
    });
});