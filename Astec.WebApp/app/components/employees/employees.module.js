/// <reference path="../../../node_modules/angular/angular.js" />
(function (app) {
    
    app.config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        //$urlRouterProvider.otherwise("/admin");
        $stateProvider.state('employees', {
            url: "/employees",
            parent: "base",
            templateUrl: "/app/components/employees/employeeListView.html",
            controller: "employeeListController"
        }).state('add_employee', {
            url: "/add_employee",
            parent: "base",
            templateUrl: "/app/components/employees/employeeAddView.html",
            controller: "employeeAddController"
        }).state('edit_employee', {
            url: "/edit_employee/:id",
            parent: "base",
            templateUrl: "/app/components/employees/employeeEditView.html",
            controller: "employeeEditController"
        }).state('detail_employee', {
            //params: ['id'],
            url: "/detail_employee/:id",
            //parent: "base",
            templateUrl: "/app/components/employees/employeeDetailView.html",
            controller: "employeeDetailController"
        });
    }
})(angular.module('astec.employees', ['astec.common']));