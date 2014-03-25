define(['durandal/system', 'logger'],
    function(system, logger) {
        var shell = {
            activate: activate
        };

        function activate() {
            logger.log('Started',
                null,
                system.getModuleId(shell),
                true);
        };

        return shell;
    }
);