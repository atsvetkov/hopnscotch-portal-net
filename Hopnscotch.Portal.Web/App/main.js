require.config({
    paths: {
        "text": "../scripts/text",
        "durandal": "../scripts/durandal",
        "knockout": "../scripts/knockout-3.1.0",
        "jquery": "../scripts/jquery-2.1.0"
    }
});

define(function(require) {
    var system = require('durandal/system');
    var app = require('durandal/app');

    system.debug(true);
    app.start().then(function() {
        app.setRoot('shell');
    });
});