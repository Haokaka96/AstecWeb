(function (app) {
    app.controller('loginController', ['$scope','$rootScope', 'loginService', '$injector', 'notificationService',
        function ($scope,$rootScope, loginService, $injector, notificationService) {

            $scope.loginData = {
                userName: "",
                password: ""
            };

            $scope.loginSubmit = function () {
                loginService.login($scope.loginData.userName, $scope.loginData.password).then(function (response) {
                    if
                    if (response != null && response.data.error != undefined) {
                        notificationService.displayError(response.data.error_description);
                    }
                    else {
                       
                        $rootScope.user = $scope.loginData.userName;
                        var stateService = $injector.get('$state');
                        stateService.go('home');
                    }
                });
            }
        }]);
})(angular.module('astec'));