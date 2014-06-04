define(function () {
    toastr.options.timeOut = 4000;

    var serviceUrl = 'breeze/breeze';

    var roleNames = {
        Administrators: 'Administrators',
        Managers: 'Managers',
        Teachers: 'Teachers',
        Students: 'Students',
        Users: 'RegisteredUsers',
    };

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
        requiredRoles: [roleNames.Users]
    }, {
        route: 'admin',
        moduleId: 'viewmodels/admin',
        title: 'Admin',
        nav: true,
        requiredRoles: [roleNames.Administrators]
    }, {
        route: 'leads',
        moduleId: 'viewmodels/leads',
        title: 'Leads',
        nav: true,
        requiredRoles: [roleNames.Managers, roleNames.Administrators]
    }, {
        route: 'leaddetails/:id',
        moduleId: 'viewmodels/leadDetails',
        title: 'View Lead',
        nav: false,
        requiredRoles: [roleNames.Teachers, roleNames.Managers, roleNames.Administrators]
    }, {
        route: 'contactdetails/:id',
        moduleId: 'viewmodels/contactDetails',
        title: 'View Contact',
        nav: false,
        requiredRoles: [roleNames.Teachers, roleNames.Managers, roleNames.Administrators]
    }, {
        route: 'lessondetails/:id',
        moduleId: 'viewmodels/lessonDetails',
        title: 'View Lesson',
        nav: false,
        requiredRoles: [roleNames.Teachers, roleNames.Managers, roleNames.Administrators]
    }, {
        route: 'contacts',
        moduleId: 'viewmodels/contacts',
        title: 'Contacts',
        nav: true,
        requiredRoles: [roleNames.Managers, roleNames.Administrators]
    }, {
        route: 'users',
        moduleId: 'viewmodels/users',
        title: 'Users',
        nav: true,
        requiredRoles: [roleNames.Managers, roleNames.Administrators]
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
        requiredRoles: [roleNames.Users]
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
        serviceUrl: serviceUrl,
        roleNames: roleNames
    };
});