/// <reference path="../../../node_modules/angular/angular.js" />
(function (app) {
    
    app.config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        //$urlRouterProvider.otherwise("/admin");
        $stateProvider.state('apartments', {
            url: "/apartments",
            parent: "base",
            templateUrl: "/app/components/apartments/apartmentListView.html",
            controller: "apartmentListController"
        }).state('add_Apartment', {
            url: "/add_Apartment",
            parent: "base",
            templateUrl: "/app/components/apartments/apartmentAddView.html",
            controller: "apartmentAddController"
        }).state('edit_Apartment', {
            url: "/edit_Apartment/:id",
            parent: "base",
            templateUrl: "/app/components/apartments/apartmentEditView.html",
            controller: "apartmentEditController"
        });
    }
})(angular.module('astec.apartments', ['astec.common']));