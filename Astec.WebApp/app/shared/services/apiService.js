/// <reference path="../../../node_modules/angular/angular.js" />

(function (app) {
    app.factory('apiService', apiService);
    apiService.$inject = ['$http', 'notificationService', 'authenticationService'];
    function apiService($http, notificationService, authenticationService) {
        return {
            get: get,
            post: post,
            put: put,
            del:del
        }

        function get(url, params, success, failure) {
            authenticationService.setHeader();
            $http.get(url, params).then(function(result) {
                result.headers("Access-Control-Allow-Origin", "*");
                result.headers("Access-Control-Allow-Headers", "X-Requested-With");
                success(result);
            }, function (error) {
                if (error.status === 401) {
                    notificationService.displayError('Authenticate is required.');
                }
                else if (failure != null)
                    failure(error);
            });
        }
        function post(url, params, success, failure) {
            authenticationService.setHeader();
            $http.post(url, params).then(function(result) {
                result.header("Access-Control-Allow-Origin", "*");
                result.header("Access-Control-Allow-Headers", "X-Requested-With");
                success(result);
            }, function (error) {
                if (error.status === 401) {
                    notificationService.displayError('Authenticate is required.');
                }
                else if (failure != null)
                    failure(error);
            });
        }
        function put(url, params, success, failure) {
            authenticationService.setHeader();
            $http.put(url, params).then(function(result) {
                result.header("Access-Control-Allow-Origin", "*");
                result.header("Access-Control-Allow-Headers", "X-Requested-With");
                success(result);
            }, function (error) {
                if (error.status === 401) {
                    notificationService.displayError('Authenticate is required.');
                }
                else if (failure != null)
                    failure(error);
            });
        }
        function del(url, params, success, failure) {
            authenticationService.setHeader();
            $http.delete(url, params).then(function(result) {
                result.header("Access-Control-Allow-Origin", "*");
                result.header("Access-Control-Allow-Headers", "X-Requested-With");
                success(result);
            }, function (error) {
                if (error.status === 401) {
                    notificationService.displayError('Authenticate is required.');
                }
                else if (failure != null)
                    failure(error);
            });
        }
    }
})(angular.module('astec.common'));