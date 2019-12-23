(function () {
    angular.module('astec.application_modules', ['astec.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('application_modules', {
            url: "/application_modules",
            templateUrl: "/app/components/modules/applicationModuleListView.html",
            parent: 'base',
            controller: "applicationModuleListController"
        })
            .state('add_application_module', {
                url: "/add_application_module",
                parent: 'base',
                templateUrl: "/app/components/modules/applicationModuleAddView.html",
                controller: "applicationModuleAddController"
            })
            .state('edit_application_module', {
                url: "/edit_application_module/:id",
                templateUrl: "/app/components/modules/applicationModuleEditView.html",
                controller: "applicationModuleEditController",
                parent: 'base',
            });
    }
})();