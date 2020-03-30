(function (app) {
    'use strict';

    app.controller('employeeListController', employeeListController);

    employeeListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox','$filter'];

    function employeeListController($scope, apiService, notificationService, $ngBootbox,$filter) {
        $scope.loading = true;
        $scope.data = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.deleteItem = deleteItem;
        $scope.exportExcel = exportExcel;
        $scope.exportPdf = exportPdf;

        $scope.selectAll = selectAll;

        $scope.deleteMultiple = deleteMultiple;

        //Xóa nhiều bản ghi
        function deleteMultiple() {
            debugger;
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.Id);
            });
            var config = {
                params: {
                    checkedList: JSON.stringify(listId)
                }
            }
            apiService.del('/api/employee/deletemulti', config, function (result) {
                debugger;
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                search();
            }, function(error) {
                    debugger;
                    console.log(error);
                notificationService.displayError('Xóa không thành công');
            });
        }

        //Chọn nhiều bản ghi
        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.data, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.data, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("data", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        //Xuất file excel
        function exportExcel() {
            var config = {
                params: {
                    filter: $scope.filterExpression
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

        //Bỏ không dùng
        //Xuất file pdf
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

        //Xóa 1 bản ghi
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

        //Tìm kiếm
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

        //Load danh sách nhân viên
        function dataLoadCompleted(result) {
            
            $scope.data = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loading=false;

            if ($scope.filterExpression && $scope.filterExpression.length) {
                notificationService.displayInfo(result.data.Items.length + ' items found');
            }
        }

        //Lỗi khi load danh sách nhân viên
        function dataLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterExpression = '';
            search();
        }

        //Load thông tin của 1 nhân viên
        $scope.loadEmlployeeDetail = function (id) {
            apiService.get('/api/employee/getbyid/' + id, null, function (result) {   
                $scope.employee = result.data;   
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }
        $scope.search();
    }
})(angular.module('astec.employees'));