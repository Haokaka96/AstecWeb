(function (app) {
    app.controller('employeeEditController', ['apiService', '$scope', '$state', 'notificationService', '$stateParams', 'commonService', function (apiService, $scope, $state, notificationService, $stateParams, commonService) {
        $scope.UpdateEmployee = UpdateEmployee;

        function loadEmlployeeDetail() {
            apiService.get('api/employee/getbyid/' + $stateParams.id, null, function (result) {
                $scope.apartment = result.data;
            }, function (error) {
                    notificationService.displayError(error.data);
            });
        }

        function UpdateEmployee() {
            apiService.put('api/emloyee/update', $scope.employee,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật.');
                    $state.go('employees');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }
        loadEmlployeeDetail();
    }]);
})(angular.module('astec.employees'));