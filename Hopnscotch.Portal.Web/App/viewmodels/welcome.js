define(['services/datacontext', 'knockout'], function (datacontext, ko) {
    var message = ko.observable('Welcome to Hop&Scotch Attendance application!');

    var vm = {
        activate: activate,
        title: 'Login',
        message: message
    };

    function activate() {
        return;
    };

    return vm;
}
);