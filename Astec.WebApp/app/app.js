/// <reference path="../node_modules/angular/angular.js" />

(function () {
    angular.module('astec', ['astec.apartments','astec.employees',
        'astec.application_groups', 'astec.application_roles', 'astec.application_users', 'astec.application_modules',
        'astec.statistics',
        'astec.common','chart.js'])
        .config(config).config(configAuthentication).config(['$httpProvider', function($httpProvider) {
            $httpProvider.defaults.withCredentials = true;
        }]);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise('/login'); 
        $stateProvider.state('base', {
            url: '',
            templateUrl: '/app/shared/views/baseView.html',
            abstract: true
        }).state('login', {
            url: "/login",
            templateUrl: "/app/components/login/loginView.html",
            controller: "loginController"
        }).state('home', {
            url: "/admin",
            parent: 'base',
            templateUrl: "/app/components/home/homeView.html",
            controller: "homeController"
        }).state('sider_bar', {
            url: "/sider_bar",
            parent: 'base',
            templateUrl: "/app/components/sider_bars/siderbar.html",
            controller: "myController"
        });
        
    }
    function configAuthentication($httpProvider) {
        $httpProvider.interceptors.push(function ($q, $location) {
            return {
                request: function (config) {
                    return config;
                },
                requestError: function (rejection) {

                    return $q.reject(rejection);
                },
                response: function (response) {
                    if (response.status == "401") {
                        $location.path('/login');
                    }
                    //the same response/modified/or a new one need to be returned.
                    return response;
                },
                responseError: function (rejection) {

                    if (rejection.status == "401") {
                        $location.path('/login');
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }
    function chartJsProvider(ChartJsProvider) {
        ChartJsProvider.setOptions({ colors: ['#803690', '#00ADF9', '#DCDCDC', '#46BFBD', '#FDB45C', '#949FB1', '#4D5360'] });
    }
})();