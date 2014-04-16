require.config({
    paths: {
        'text': '../App/durandal/amd/text',
        //'durandal': '../App/durandal',
        'plugins': '../App/durandal/plugins',
        'transitions': '../App/durandal/transitions',
        //'knockout': '../scripts/knockout-3.1.0',
        'knockout.validation': '../scripts/knockout.validation',
        'bootstrap': '../scripts/bootstrap',
        'jquery': '../scripts/jquery-2.1.0',
        'jquery.utilities': '../scripts/jquery.utilities'
    }
});

define('knockout', ko);

define(function(require) {
    var system = require('durandal/system');
    var app = require('durandal/app');
    var router = require('durandal/plugins/router');
    var viewLocator = require('durandal/viewLocator');
    var logger = require('services/logger');
    var session = require('services/session');

    system.debug(true);

    app.title = 'Hop&Scotch Attendance';

    app.configurePlugins({
        router: true
    });

    app.start().then(function () {
        //router.useConvention();
        viewLocator.useConvention();

        app.setRoot('viewmodels/shell', 'entrance');

        router.handleInvalidRoute = function (route, params) {
            logger.logError('No route found', route, 'main', true);
        };
    });

    function configureKnockout() {
        ko.validation.init({
            insertMessage: true,
            decorateElement: true,
            errorElementClass: 'has-error',
            errorMessageClass: 'help-block'
        });

        if (!ko.utils.cloneNodes) {
            ko.utils.cloneNodes = function (nodesArray, shouldCleanNodes) {
                for (var i = 0, j = nodesArray.length, newNodesArray = []; i < j; i++) {
                    var clonedNode = nodesArray[i].cloneNode(true);
                    newNodesArray.push(shouldCleanNodes ? ko.cleanNode(clonedNode) : clonedNode);
                }
                return newNodesArray;
            };
        }

        ko.bindingHandlers.ifIsInRole = {
            init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                ko.utils.domData.set(element, '__ko_withIfBindingData', {});
                return { 'controlsDescendantBindings': true };
            },
            update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                var withIfData = ko.utils.domData.get(element, '__ko_withIfBindingData'),
                    dataValue = ko.utils.unwrapObservable(valueAccessor()),
                    shouldDisplay = session.userIsInRole(dataValue),
                    isFirstRender = !withIfData.savedNodes,
                    needsRefresh = isFirstRender || (shouldDisplay !== withIfData.didDisplayOnLastUpdate),
                    makeContextCallback = false;

                if (needsRefresh) {
                    if (isFirstRender) {
                        withIfData.savedNodes = ko.utils.cloneNodes(ko.virtualElements.childNodes(element), true /* shouldCleanNodes */);
                    }

                    if (shouldDisplay) {
                        if (!isFirstRender) {
                            ko.virtualElements.setDomNodeChildren(element, ko.utils.cloneNodes(withIfData.savedNodes));
                        }
                        ko.applyBindingsToDescendants(makeContextCallback ? makeContextCallback(bindingContext, dataValue) : bindingContext, element);
                    } else {
                        ko.virtualElements.emptyNode(element);
                    }

                    withIfData.didDisplayOnLastUpdate = shouldDisplay;
                }
            }
        };
    }
});