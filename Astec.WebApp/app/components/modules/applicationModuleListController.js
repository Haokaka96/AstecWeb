(function (app) {
    'use strict';

    app.controller('applicationModuleListController', applicationModuleListController);

    applicationModuleListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox'];

    function applicationModuleListController($scope, apiService, notificationService, $ngBootbox) {
        $scope.loading = true;
        $scope.appModules = [];
        $scope.getModules = getModules;
        $scope.deleteItem = deleteItem;

        function deleteItem(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?')
                .then(function () {
                    var config = {
                        params: {
                            id: id
                        }
                    }
                    apiService.del('/api/module/delete', config, function () {
                        notificationService.displaySuccess('Đã xóa thành công.');
                        getModules();
                    },
                        function () {
                            notificationService.displayError('Xóa không thành công.');
                        });
                });
        }
        function getModules() {
            
            $scope.loading = true;
            apiService.get('/api/module/getall', null, dataLoadCompleted, dataLoadFailed);
        }

        function dataLoadCompleted(result) {
            $scope.appModules = result.data;
            $scope.loading = false; 
        }
        function dataLoadFailed(response) {
            notificationService.displayError(response.data);
        }
        getModules();
    }
})(angular.module('astec.application_modules'));