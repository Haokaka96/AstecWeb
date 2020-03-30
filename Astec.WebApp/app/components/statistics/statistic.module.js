(function (app) {

    app.config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        //$urlRouterProvider.otherwise("/admin");
        $stateProvider.state('statistic_employee', {
            url: "/statistic_employee",
            parent: "base",
            templateUrl: "/app/components/statistics/employeeStatisticView.html",
            controller: "employeeStatisticController"
        });
    }
})(angular.module('astec.statistics', ['astec.common','chart.js']));