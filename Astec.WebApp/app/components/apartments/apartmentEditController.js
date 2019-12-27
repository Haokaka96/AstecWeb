(function (app) {
    app.controller('apartmentEditController', ['apiService', '$scope', '$state', 'notificationService', '$stateParams', 'commonService', function (apiService, $scope, $state, notificationService, $stateParams, commonService) {
        $scope.UpdateApartment = UpdateApartment;

        function loadApartmentDetail() {
            apiService.get('/api/apartment/getbyid/' + $stateParams.id, null, function (result) {
                $scope.apartment = result.data;
            }, function (error) {
                    notificationService.displayError(error.data);
            });
        }

        function UpdateApartment() {
            apiService.put('/api/apartment/update', $scope.apartment,
                function (result) {
                    notificationService.displaySuccess(result.data.ApartmentName + ' đã được cập nhật.');
                    $state.go('apartments');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }
        loadApartmentDetail();
    }]);
})(angular.module('astec.apartments'));