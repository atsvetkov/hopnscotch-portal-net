define(['jquery', 'services/datacontext', 'plugins/router', 'knockout', 'services/session', 'services/security', 'config', 'datepicker'], function ($, datacontext, router, ko, session, security, config) {
    var user = ko.observable();
    var roles = ko.observableArray([]);

    var Role = function(roleName, isIn) {
        var name = ko.observable('');
        var isInRole = ko.observable(false);

        if (roleName) {
            name(roleName);
        }

        if (isIn) {
            isInRole(isIn);
        }

        return {
            name: name,
            isInRole: isInRole
        }
    };

    var goBack = function () {
        router.navigateBack();
    };

    var save = function () {
        var userRoles = [];

        roles().forEach(function (r) {
            if (r.isInRole()) {
                userRoles.push(r.name());
            }
        });

        user().userRoles = userRoles;

        security.saveUser(user())
            .then(goBack);
    };

    function setupUserRoles() {
        roles([]);

        for (var p in config.roleNames) {
            var roleName = config.roleNames[p];
            var isIn = $.inArray(roleName, user().userRoles) > -1;
            
            roles.push(new Role(roleName, isIn));
        };
    };

    function activate(routeData) {
        var id = parseInt(routeData);
        console.log('id: ' + id);
        return security.getUser(id, user)
            .then(function () {
                console.log('setup roles');
                setupUserRoles();
            });
    }

    var vm = {
        activate: activate,
        goBack: goBack,
        save: save,
        title: 'User Details',
        user: user,
        roles: roles
    };
    
    return vm;
}
);