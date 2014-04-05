define(['services/datacontext', 'knockout'], function (datacontext, ko) {
    var message = ko.observable('Welcome, this is your amazing dashboard!');

    var vm = {
        activate: activate,
        title: 'Dashboard',
        message: message
    };

    function activate() {
    };

    return vm;
}
);