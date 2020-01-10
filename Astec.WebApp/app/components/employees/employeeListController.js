(function (app) {
    'use strict';

    app.controller('employeeListController', employeeListController);

    employeeListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox'];

    function employeeListController($scope, apiService, notificationService, $ngBootbox) {
        $scope.loading = true;
        $scope.data = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.deleteItem = deleteItem;
        $scope.exportExcel = exportExcel;
        $scope.exportPdf = exportPdf;
        function exportExcel() {
            var config = {
                params: {
                    filter: $scope.keyword
                }
            }
            
            apiService.get('/api/employee/ExportXls', config, function (response) {
                if (response.status = 200) {
                    window.location.href = response.data.Message;
                }
            }, function (error) {
                notificationService.displayError(error);

            });
        }
        function exportPdf(id) {
            var config = {
                params: {
                    id: id
                }
            }
            apiService.get('/api/employee/ExportPdf', config, function (response) {
                if (response.status = 200) {
                    window.location.href = response.data.Message;
                }
            }, function (error) {
                notificationService.displayError(error);

            });
        }
        function deleteItem(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?')
                .then(function () {
                    var config = {
                        params: {
                            id: id
                        }
                    }
                    apiService.del('/api/employee/delete', config, function () {
                        notificationService.displaySuccess('Đã xóa thành công.');
                        search();
                    },
                        function () {
                            notificationService.displayError('Xóa không thành công.');
                        });
                });
        }
        function search(page) {
            page = page || 0;

            $scope.loading = true;
            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    filter: $scope.filterExpression
                }
            }

            apiService.get('/api/employee/getlistpaging', config, dataLoadCompleted, dataLoadFailed);
        }

        function dataLoadCompleted(result) {
            $scope.data = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loading = false;

            if ($scope.filterExpression && $scope.filterExpression.length) {
                notificationService.displayInfo(result.data.Items.length + ' items found');
            }
        }
        function dataLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterExpression = '';
            search();
        }
        $scope.loadEmlployeeDetail =function(id) {
            apiService.get('/api/employee/getbyid/' + id, null, function (result) {
                $scope.employee = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }
        $scope.search();
    }
})(angular.module('astec.employees'));