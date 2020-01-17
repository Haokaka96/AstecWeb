(function (app) {
    app.controller('employeeEditController', ['apiService', '$scope', '$state', 'notificationService', '$stateParams', 'commonService', function (apiService, $scope, $state, notificationService, $stateParams, commonService) {
        $scope.UpdateEmployee = UpdateEmployee;

        function loadEmlployeeDetail() {
            apiService.get('/api/employee/getbyid/' + $stateParams.id, null, function (result) {
                $scope.employee = result.data;
            }, function (error) {
                    notificationService.displayError(error.data);
            });
        }

        function UpdateEmployee() {
            if ($scope.employee.ImageName == null)
                $scope.employee.ImageName = '/Uploads/Images/img_avatar.png';
            apiService.put('/api/employee/update', $scope.employee,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật.');
                    $state.go('employees');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }
        $scope.chooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.employee.ImageName = fileUrl;
            }
            finder.popup();
        }
        loadEmlployeeDetail();
    }]);
})(angular.module('astec.employees'));