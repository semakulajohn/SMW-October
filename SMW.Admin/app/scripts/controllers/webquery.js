
angular
    .module('homer').controller('WebQueryController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/WebQueryApi/GetAllWebQueries');
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                }
            );

            $scope.gridData = {
                enableFiltering: true,
                columnDefs: $scope.columns,
                enableRowSelection: true
            };

            $scope.gridData.multiSelect = true;

            $scope.gridData.columnDefs = [

                {
                    name: 'Name', field:'Name',
                   
                },

                { name: 'Body', field: 'Body' },
                { name: 'EmailAddress', field: 'EmailAddress' },
                { name: 'CreatedOn', field: 'CreatedOn', sort: {
                        direction: uiGridConstants.DESC,
                        priority: 1
                    } },
               //  { name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/webqueries/edit/{{row.entity.WebQueryId}}">Edit</a> </div>' },


            ];




        }]);



