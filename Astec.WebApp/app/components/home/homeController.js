(function (app) {
    app.controller('homeController', homeController);
    function homeController($scope) {
        $scope.labels = ["January", "February", "March", "April", "May", "June", "July"];
        $scope.series = ['Series A', 'Series B'];
        $scope.data = [
            [65, 59, 80, 81, 56, 55, 40],
            [28, 48, 40, 19, 86, 27, 90]
        ];
        $scope.onClick = function (points, evt) {
            console.log(points, evt);
        };
        $scope.datasetOverride = [{ yAxisID: 'y-axis-1' }, { yAxisID: 'y-axis-2' }];
        $scope.options = {
            scales: {
                yAxes: [
                    {
                        id: 'y-axis-1',
                        type: 'linear',
                        display: true,
                        position: 'left'
                    },
                    {
                        id: 'y-axis-2',
                        type: 'linear',
                        display: true,
                        position: 'right'
                    }
                ]
            }
        } 
    };

    app.controller("BubbleCtrl",
        function ($scope) {
            // see examples/bubble.js for random bubbles source code
            $scope.series = ['Series A', 'Series B'];

            $scope.data = [
                [{
                    x: 40,
                    y: 10,
                    r: 20
                }],
                [{
                    x: 10,
                    y: 40,
                    r: 50
                }]
            ];
        });
    
})(angular.module('astec'));