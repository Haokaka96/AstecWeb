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

        function loadParentModule() {
            apiService.get('/api/module/getallparents', null, function (result) {
                console.log(result);
                $scope.parentCategories = commonService.getTree(result.data, "Id", "ParentId");
                $scope.parentCategories.forEach(function (item) {
                    recur(item, 0, $scope.flatFolders);
                });
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        function times(n, str) {
            var result = '';
            for (var i = 0; i < n; i++) {
                result += str;
            }
            return result;
        };
        function recur(item, level, arr) {
            arr.push({
                Name: times(level, '–') + ' ' + item.Name,
                ID: item.ID,
                Level: level,
                Indent: times(level, '–')
            });
            if (item.children) {
                item.children.forEach(function (item) {
                    recur(item, level + 1, arr);
                });
            }
        };
        loadParentModule();
    }
})(angular.module('astec.application_modules'));