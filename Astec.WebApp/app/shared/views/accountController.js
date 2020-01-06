/// <reference path="../../../node_modules/angular/angular.js" />
(function (app) {
    app.controller("accountController", ["$scope", "apiService", function ($scope, apiService) {
        $scope.user = '';
        apiService.get('/api/account/get', null, function (result) {
            $scope.user = result.data;
            console.log($scope.user)
        }, function (error) {
                console.log(error);
        });

    }]);
})(angular.module("astec"));