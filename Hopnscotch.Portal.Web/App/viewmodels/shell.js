define(['durandal/system', 'services/logger', 'plugins/router', 'config', 'services/datacontext', 'services/session', 'services/security', 'jquery.utilities'],
    function(system, logger, router, config, datacontext, session, security) {
        var shell = {
            activate: activate,
            router: router,
            session: session,
            logout: logout,
            search: function () {
                //It's really easy to show a message box.
                //You can add custom options too. Also, it returns a promise for the user's response.
                app.showMessage('Search not yet implemented...');
            },

        };
        
        return shell;

        function activate() {
            return datacontext.primeData()
                .then(init)
                .fail(failedInit);
        };

        function failedInit(error) {
            var msg = 'App init failed: ' + error.message;
            logger.log(msg, error, system.getModuleId(shell), true);
        }

        function verifyStateMatch(fragment) {
            var state;

            if (typeof (fragment.access_token) !== "undefined") {
                state = sessionStorage["state"];
                sessionStorage.removeItem("state");

                if (state === null || fragment.state !== state) {
                    fragment.error = "invalid_state";
                }
            }
        }

        function logout() {
            security.logout().done(function () {
                logger.log('Signed out.', null, system.getModuleId(shell), true);
            }).fail(function () {
                logger.logError('Logout failed.', null, system.getModuleId(shell), true);
            }).always(function () {
                session.clearUser();
                router.navigate('#welcome', 'replace');
            });
        }

        function setupRouter() {
            router.map(config.routes).buildNavigationModel();

            router.guardRoute = function (routeInfo, params, instance) {
                if (typeof (params.config.requiredRoles) !== "undefined") {
                    var res = session.userIsInRole(params.config.requiredRoles);

                    if (!res) {
                        logger.log({
                            message: "Access denied. Navigation cancelled.",
                            showToast: true,
                            type: "warning"
                        });
                    }

                    return res;
                }
                else {
                    return true;
                }
            };

            return router.activate();
        }

        function init() {
            logger.log('Started', null, system.getModuleId(shell), true);

            var dfd = $.Deferred(), fragment = $.getFragment(), externalAccessToken, externalError, loginUrl;

            verifyStateMatch(fragment);

            window.location.hash = "";

            if (sessionStorage["associatingExternalLogin"]) {
                sessionStorage.removeItem("associatingExternalLogin");

                var externalAssociationResult = {};

                if (typeof (fragment.error) !== "undefined") {
                    externalAssociationResult.externalAccessToken = null;
                    externalAssociationResult.externalError = fragment.error;
                } else if (typeof (fragment.access_token) !== "undefined") {
                    externalAssociationResult.externalAccessToken = fragment.access_token;
                    externalAssociationResult.externalError = null;
                } else {
                    externalAssociationResult.externalAccessToken = null;
                    externalAssociationResult.externalError = null;
                }

                //save this for the manage VM to use
                sessionStorage["externalAssociationResult"] = JSON.stringify(externalAssociationResult);

                security.getUserInfo()
                    .done(function (data) {
                        if (data.userName) {
                            session.setUser(data);

                            window.location.href = "#manage";
                            setupRouter().done(function () {
                                dfd.resolve();
                            });
                        } else {
                            logger.logError('Error retrieving user information.', null, system.getModuleId(shell), true);
                            window.location.href = "#login";
                            setupRouter().done(function () {
                                dfd.resolve();
                            });
                        }
                    })
                    .fail(function () {
                        logger.logError('Error retrieving user information.', null, system.getModuleId(shell), true);
                        window.location.href = "#login";
                        setupRouter().done(function () {
                            dfd.resolve();
                        });
                    });
            } else if (typeof (fragment.error) !== "undefined") {
                logger.logError('External login failed.', null, system.getModuleId(shell), true);
                window.location.href = "#login";
                setupRouter().done(function () {
                    dfd.resolve();
                });

            } else if (typeof (fragment.access_token) !== "undefined") {
                security.getUserInfo(fragment.access_token)
                    .done(function (data) {
                        if (typeof (data.userName) !== "undefined" && typeof (data.hasRegistered) !== "undefined"
                            && typeof (data.loginProvider) !== "undefined") {
                            if (data.hasRegistered) {
                                data.accessToken = fragment.access_token;
                                session.setUser(data, false);
                                setupRouter().done(function () {
                                    dfd.resolve();
                                });
                            }
                            else if (typeof (sessionStorage["loginUrl"]) !== "undefined") {
                                sessionStorage["registerExternal"] = JSON.stringify({
                                    userName: data.userName,
                                    loginProvider: data.loginProvider,
                                    externalAccessToken: fragment.access_token,
                                    loginUrl: sessionStorage["loginUrl"],
                                    state: fragment.state
                                });

                                sessionStorage.removeItem("loginUrl");

                                window.location.href = "#registerExternal";

                                setupRouter().done(function () {
                                    dfd.resolve();
                                });
                            }
                            else {
                                logger.logError('Login failed.', null, system.getModuleId(shell), true);
                                window.location.href = "#login";
                                setupRouter().done(function () {
                                    dfd.resolve();
                                });
                            }
                        } else {
                            logger.logError('Login failed.', null, system.getModuleId(shell), true);
                            window.location.href = "#login";
                            setupRouter().done(function () {
                                dfd.resolve();
                            });
                        }
                    })
                    .fail(function () {
                        logger.logError('Login failed.', null, system.getModuleId(shell), true);
                        window.location.href = "#login";
                        setupRouter().done(function () {
                            dfd.resolve();
                        });
                    });
            } else if (session.rememberedToken()) {
                security.getUserInfo()
                    .done(function (data) {
                        if (data.userName) {
                            session.setUser(data);
                            setupRouter().done(function () {
                                dfd.resolve();
                            });
                        } else {
                            logger.logError('Login failed.', null, system.getModuleId(shell), true);
                            window.location.href = "#login";
                            setupRouter().done(function () {
                                dfd.resolve();
                            });
                        }
                    })
                    .fail(function () {
                        logger.logError('Login failed.', null, system.getModuleId(shell), true);
                        window.location.href = "#login";
                        setupRouter().done(function () {
                            dfd.resolve();
                        });
                    });
            }
            else {
                setupRouter().done(function () {
                    dfd.resolve();
                });
            }

            return dfd.promise();
        }
    }
);