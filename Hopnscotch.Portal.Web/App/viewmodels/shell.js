define(['durandal/system', 'services/logger', 'plugins/router', 'config', 'services/datacontext'],
    function(system, logger, router, config, datacontext) {
        var shell = {
            activate: activate,
            router: router
        };
        
        return shell;

        function activate() {
            return datacontext.primeData()
                .then(boot)
                .fail(failedInit);
        };

        function failedInit(error) {
            var msg = 'App init failed: ' + error.message;
            logger.log(msg, error, system.getModuleId(shell), true);
        }

        function boot() {
            logger.log('Started', null, system.getModuleId(shell), true);
            router.map(config.routes).buildNavigationModel();
            return router.activate();
        }
    }
);