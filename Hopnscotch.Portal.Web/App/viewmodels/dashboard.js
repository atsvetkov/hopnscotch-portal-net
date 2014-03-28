define(['services/dataservice'], function (dataservice) {
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