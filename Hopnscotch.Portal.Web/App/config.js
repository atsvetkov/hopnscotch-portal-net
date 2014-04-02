define(function () {
    toastr.options.timeOut = 4000;

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
    }];
    
    return {
        routes: routes
    };
});