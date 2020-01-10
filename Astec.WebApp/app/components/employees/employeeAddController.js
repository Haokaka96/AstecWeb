(function (app) {
    app.controller('employeeAddController', ['apiService', '$scope', '$state', 'notificationService',function (apiService, $scope,$state, notificationService) {
        $scope.addEmployee = addEmployee;
       // $scope.files = [];
        $scope.employee = {
            ImageName: [],
            Image:[]
        }
       
        function addEmployee() {
            apiService.post('/api/employee/create', $scope.employee,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('employees');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }
        $scope.chooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.employee.ImageName = fileUrl;
            }
            finder.popup();
        }
        //$scope.UploadFiles = function (files) {
        //    $scope.SelectedFiles = files;
        //    if ($scope.SelectedFiles && $scope.SelectedFiles.length) {
        //        Upload.upload({
        //            url: "/api/upload/uploadfiles",
        //            data: {
        //                files: $scope.SelectedFiles
        //            }
        //        }).then(function (response) {
        //            $timeout(function () {
        //                console.log($scope.SelectedFiles);
        //                $scope.Result = response.data;
        //            });
        //        }, function (response) {
        //            if (response.status > 0) {
        //                var errorMsg = response.status + ': ' + response.data;
        //                alert(errorMsg);
        //            }
        //        }, function (evt) {
        //            var element = angular.element(document.querySelector('#dvProgress'));
        //            $scope.Progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
        //            element.html('<div style="width: ' + $scope.Progress + '%">' + $scope.Progress + '%</div>');
        //        });
        //    }
        //};
    }]);
})(angular.module('astec.employees'));