define(['durandal/system', 'services/logger', 'plugins/router', 'config'],
    function(system, logger, router, config) {
        var shell = {
            activate: activate,
            router: router
        };
        
        return shell;

        function activate() {
            logger.log('Started', null, system.getModuleId(shell), true);
            router.map(config.routes).buildNavigationModel();
            return router.activate();
        };
    }
);