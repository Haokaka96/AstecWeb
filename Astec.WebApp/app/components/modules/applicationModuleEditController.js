(function (app) {
    'use strict';

    app.controller('applicationModuleEditController', applicationModuleEditController);

    applicationModuleEditController.$inject = ['$scope', 'apiService', 'notificationService', '$location', '$stateParams'];

    function applicationModuleEditController($scope, apiService, notificationService, $location, $stateParams) {
        $scope.appModule = {}


        $scope.updateModule = updateModule;

        function updateModule() {
            apiService.put('/api/module/update', $scope.appModule, addSuccessed, addFailed);
        }
        function loadDetail() {
            apiService.get('/api/module/getbyid/' + $stateParams.id, null,
                function (result) {
                    $scope.appModule = result.data;
                },
                function (result) {
                    notificationService.displayError(result.data);
                });
        }

        function addSuccessed() {
            notificationService.displaySuccess($scope.appModule.Name + ' đã được cập nhật thành công.');

            $location.url('application_modules');
        }
        function addFailed(response) {
            notificationService.displayError(response.data.Message);
            //notificationService.displayErrorValidation(response);
        }
        loadDetail();
    }
})(angular.module('astec.application_modules'));