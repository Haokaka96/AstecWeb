(function () {

    //angular module
    var myApp = angular.module('myApp', ['astec.common']);

    //test controller
    myApp.controller('myController', function ($scope,apiService,commonService) {

        function loadParentModule() {
            debugger;
        apiService.get('api/module/getallparents', null, function (result) {
            console.log(result);
            $scope.parentModules = commonService.getTree(result.data, "Id", "ParentId");
            $scope.parentModules.forEach(function (item) {
                recur(item, 0, $scope.flatFolders);
            });
        }, function () {
            console.log('Cannot get list parent');
        });
    }
        function times(n, str) {
            var result = '';
            for (var i = 0; i < n; i++) {
                result += str;
            }
            return result;
        };
    function recur(item, level, arr) {
        arr.push({
            Name: times(level, '–') + ' ' + item.Name,
            ID: item.ID,
            Level: level,
            Indent: times(level, '–')
        });
        if (item.children) {
            item.children.forEach(function (item) {
                recur(item, level + 1, arr);
            });
        }
    };
        loadParentModule();
    });

})();
