define(function () {
    toastr.options.timeOut = 4000;

    var serviceUrl = 'breeze/breeze';

    var routes = [{
        route: '',
        moduleId: 'viewmodels/welcome',
        title: 'Welcome',
        nav: false
    }, {
        route: 'dashboard',
        moduleId: 'viewmodels/dashboard',
        title: 'Dashboard',
        nav: true,
        requiredRoles: ['RegisteredUsers']
    }, {
        route: 'admin',
        moduleId: 'viewmodels/admin',
        title: 'Admin',
        nav: true,
        requiredRoles: ['Administrators']
    }, {
        route: 'leads',
        moduleId: 'viewmodels/leads',
        title: 'Leads',
        nav: true,
        requiredRoles: ['Managers', 'Administrators']
    }, {
        route: 'leaddetails/:id',
        moduleId: 'viewmodels/leadDetails',
        title: 'View Lead',
        nav: false,
        requiredRoles: ['Teachers', 'Managers', 'Administrators']
    }, {
        route: 'lessondetails/:id',
        moduleId: 'viewmodels/lessonDetails',
        title: 'View Lesson',
        nav: false,
        requiredRoles: ['Teachers', 'Managers', 'Administrators']
    }, {
        route: 'contacts',
        moduleId: 'viewmodels/contacts',
        title: 'Contacts',
        nav: true,
        requiredRoles: ['Managers', 'Administrators']
    }, {
        route: 'users',
        moduleId: 'viewmodels/users',
        title: 'Users',
        nav: true,
        requiredRoles: ['Managers', 'Administrators']
    }, {
        route: 'login',
        moduleId: 'viewmodels/login',
        title: 'Login',
        nav: false
    }, {
        route: 'manage',
        moduleId: 'viewmodels/manage',
        title: 'Manage',
        nav: false,
        requiredRoles: ['RegisteredUsers']
    }, {
        route: 'register',
        moduleId: 'viewmodels/register',
        title: 'Register',
        nav: false
    }, {
        route: 'registerExternal',
        moduleId: 'viewmodels/registerExternal',
        title: 'Register External',
        nav: false
    }];
    
    return {
        routes: routes,
        serviceUrl: serviceUrl
    };
});