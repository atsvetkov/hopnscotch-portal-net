define(function () {
    toastr.options.timeOut = 4000;

    var serviceUrl = 'breeze/breeze';

    var routes = [{
        route: '',
        moduleId: 'viewmodels/welcome',
        name: 'Welcome',
        title: 'Welcome',
        nav: true
    }, {
        route: 'dashboard',
        moduleId: 'viewmodels/dashboard',
        name: 'Dashboard',
        title: 'Dashboard',
        nav: true,
        requiredRoles: ['RegisteredUsers']
    }, {
        route: 'admin',
        moduleId: 'viewmodels/admin',
        name: 'Admin',
        title: 'Admin',
        nav: true,
        requiredRoles: ['Administrators']
    }, {
        route: 'leads',
        moduleId: 'viewmodels/leads',
        name: 'Leads',
        title: 'Leads',
        nav: true,
        requiredRoles: ['RegisteredUsers']
    }, {
        route: 'contacts',
        moduleId: 'viewmodels/contacts',
        name: 'Contacts',
        title: 'Contacts',
        nav: true,
        requiredRoles: ['RegisteredUsers']
    }, {
        route: 'users',
        moduleId: 'viewmodels/users',
        name: 'Users',
        title: 'Users',
        nav: true,
        requiredRoles: ['RegisteredUsers']
    }, {
        route: 'login',
        moduleId: 'viewmodels/login',
        name: 'Login',
        title: 'Login',
        nav: false
    }, {
        route: 'manage',
        moduleId: 'viewmodels/manage',
        name: 'Manage',
        title: 'Manage',
        nav: false,
        //requiredRoles: ['RegisteredUsers']
    }];
    
    return {
        routes: routes,
        serviceUrl: serviceUrl
    };
});