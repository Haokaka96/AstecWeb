(function (app) {
    'use strict';

    app.controller('applicationModuleAddController', applicationModuleAddController);

    applicationModuleAddController.$inject = ['$scope', 'apiService', 'notificationService', '$location', 'commonService'];

    function applicationModuleAddController($scope, apiService, notificationService, $location, commonService) {
        $scope.addModule = addModule;

        function addModule() {
            apiService.post('/api/module/Create', $scope.appModule, addSuccessed, addFailed);
        }

        function addSuccessed() {
            notificationService.displaySuccess($scope.appModule.Name + ' đã được thêm mới.');

            $location.url('application_modules');
        }
        function addFailed(response) {
            notificationService.displayError(response.data.Message);
            //notificationService.displayErrorValidation(response);
        }

    }
})(angular.module('astec.application_modules'));