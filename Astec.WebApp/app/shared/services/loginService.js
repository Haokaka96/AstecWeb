(function (app) {
    'use strict';
    app.service('loginService', ['$http', '$q', 'authenticationService', 'authData', 'notificationService', '$injector',
        function ($http, $q, authenticationService, authData, notificationService, $injector) {
            var userInfo;
            var deferred;
            this.login = function (userName, password) {
                deferred = $q.defer();
                var data = "grant_type=password&username=" + userName + "&password=" + password;
                $http.post('/oauth/token', data, {
                    headers:
                        { 'Content-Type': 'application/x-www-form-urlencoded' }
                }).then(function (response) {
                    userInfo = {
                        accessToken: response.data.access_token,
                        userName: userName
                    };
                    authenticationService.setTokenInfo(userInfo);
                    authData.authenticationData.IsAuthenticated = true;
                    authData.authenticationData.userName = userName;                     
                    authData.authenticationData.accessToken = userInfo.accessToken;
                    var stateService = $injector.get('$state');
                    notificationService.displaySuccess("Xin chào " + userName);
                    stateService.go('home');
                    deferred.resolve(null);
                }, function (response) {
                        notificationService.displayError(response.data.error_description);
                })
                    .then(function (err, status) {
                        authData.authenticationData.IsAuthenticated = false;
                        authData.authenticationData.userName = "";
                        deferred.resolve(err);
                    });
                return deferred.promise;
            }

            this.logOut = function () {
                authenticationService.removeToken();
                authData.authenticationData.accessToken = "";
                authData.authenticationData.IsAuthenticated = false;
                authData.authenticationData.userName = "";
            }
        }]);
})(angular.module('astec.common'));