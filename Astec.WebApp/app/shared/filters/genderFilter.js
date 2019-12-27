(function (app) {
    app.filter('genderFilter', function () {

        return function (input) {
            if (input == true)
                return 'Nam';
            else
                return 'Nữ';
        }
    });
})(angular.module('astec.common'));