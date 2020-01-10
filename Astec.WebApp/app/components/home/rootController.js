(function (app) {
    app.controller('rootController', rootController);

    rootController.$inject = ['authData', 'loginService', '$scope', 'authenticationService'];

    function rootController(authData, loginService, $scope) {
        $scope.logOut = function () {
            loginService.logOut();
            window.location.href = "/admin";
            //$state.go('login'); 
        }
        $scope.authentication = authData.authenticationData;
       // $scope.sideBar = "/app/shared/views/sideBar.html";
        //authenticationService.validateRequest();
    }
})(angular.module('astec'));