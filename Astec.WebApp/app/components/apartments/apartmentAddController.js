(function (app) {
    app.controller('apartmentAddController', function (apiService, $scope, $http, $state, notificationService) {
        $scope.addApartment = addApartment;

        function addApartment() {
            apiService.post('/api/apartment/create', $scope.apartment,
                function (result) {
                    notificationService.displaySuccess(result.data.ApartmentName + ' đã được thêm mới.');
                    $state.go('apartments');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }

    });
})(angular.module('astec.apartments'));