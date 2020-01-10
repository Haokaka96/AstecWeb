

(function (app) {
    app.controller("employeeStatisticController", ["$scope", 'apiService', 'notificationService','$filter', function ($scope, apiService, notificationService, $filter) {
        $scope.tableData = [];
        $scope.fromDate = '';
        $scope.toDate = '';
        var config = {
            params: {
                fromDate:$scope.fromDate,
                toDate: $scope.toDate
            }
        }
        $scope.filter = function () {
            getEmployeeStatistic();
        }
        $scope.labels = [];
        $scope.series = ['Nam', 'Nữ'];
        $scope.chartData = [];
        $scope.onClick = function (points, evt) {
           // console.log(points, evt);
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
        };

       
        function getEmployeeStatistic() {
            debugger;
            if ($scope.fromDate == '')
                $scope.fromDate = '01/01/2018';
            if ($scope.toDate=='')
                $scope.toDate = '01/12/2020';
            apiService.get("/api/statistic/getEmployee?fromDate=" + $scope.fromDate + "&toDate=" + $scope.toDate, null,
                function (response) {
                    

                    $scope.tableData = response.data;
                    var labels = [];
                    var chartData = [];
                    var male = [];
                    var female = [];
                    $.each(response.data, function (i, item) {
                        labels.push($filter('date')(item.DateOfBirth, 'yyyy'));
                        male.push(item.Gender==true);
                        female.push(item.Gender == false);

                    });
                    chartData.push(male);
                    chartData.push(female);
                    $scope.chartData = chartData;
                    $scope.labels = labels;
                },
                function (error) {
                    console.log(error);
                    //notificationService.displayWarning("Không thể tải dữ liệu.");
                });
        }
        //getEmployeeStatistic();
        $scope.filter();

    }]);
})(angular.module("astec.statistics"));