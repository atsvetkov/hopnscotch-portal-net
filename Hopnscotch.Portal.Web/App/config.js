define(function () {
    toastr.options.timeOut = 4000;

    var serviceUrl = 'api/breeze';

    var routes = [{
        route: '',
        moduleId: 'viewmodels/dashboard',
        name: 'Dashboard',
        title: 'Dashboard',
        nav: true
    },{
        route: 'admin',
        moduleId: 'viewmodels/admin',
        name: 'Admin',
        title: 'Admin',
        nav: true
    }, {
        route: 'leads',
        moduleId: 'viewmodels/leads',
        name: 'Leads',
        title: 'Leads',
        nav: true
    }, {
        route: 'contacts',
        moduleId: 'viewmodels/contacts',
        name: 'Contacts',
        title: 'Contacts',
        nav: true
    }, {
        route: 'users',
        moduleId: 'viewmodels/users',
        name: 'Users',
        title: 'Users',
        nav: true
    }];
    
    return {
        routes: routes,
        serviceUrl: serviceUrl
    };
});