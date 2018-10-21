angular
    .module('homer').controller('ClientController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/UserApi/GetAllClients');
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
                    name: 'First Name', field: 'FirstName'
                },

                 { name: 'Last Name', field: 'LastName' },

                  { name: 'Email Address', field: 'Email' },


                 { name: 'Action', field: 'Id', cellTemplate: '<div class="ui-grid-cell-contents"><a href="#/projects/{{row.entity.Id}}">Manage Projects</a></div>' },



            ];




        }]);


angular
    .module('homer').controller('ClientProjectController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var clientId = $scope.clientId;
            var promise = $http.get('/webapi/ProjectApi/GetAllClientProjects?clientId=' + clientId, {});
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                }
            );
            $scope.retrievedCleintId = $scope.clientId;
            $scope.gridData = {
                enableFiltering: true,
                columnDefs: $scope.columns,
                enableRowSelection: true
            };

            $scope.gridData.multiSelect = true;

            $scope.gridData.columnDefs = [

                {
                    name: 'Title', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/projects/edit/{{row.entity.ProjectId}}/{{row.entity.MediaFolderId}}">{{row.entity.Title}}</a> </div>',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },

                { name: 'Description', field: 'Description' },



            ];




        }]);
