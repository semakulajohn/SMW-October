angular
    .module('homer').controller('OnGoingProjectController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            

            var onGoingProjectsPromise = $http.get('/webapi/ProjectApi/GetAllOnGoingProjects', {});
            onGoingProjectsPromise.then(
                function (payload) {
                    $scope.onGoingProjects = payload.data;
                }
            );




        }]);


angular
    .module('homer').controller('CompletedProjectController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            
            var completedProjectsPromise = $http.get('/webapi/ProjectApi/GetAllCompletedProjects', {});
            completedProjectsPromise.then(
                function (payload) {
                    $scope.completedProjects = payload.data;
                }
            );




        }]);


angular
    .module('homer')
    .controller('ProjectDetailController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval', 'resizeService', 'FileUploader', 'appSettingsService',
    function ($scope, ngTableParams, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval, resizeService, FileUploader, appSettingsService) {

        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }

        var projectId = $scope.projectId;
        var action = $scope.action;
        var uploader = $scope.uploader = new FileUploader({
            url: '/upload/?parentId=' + $scope.mediaFolderId
        });
       

        $scope.mediaTypes = 2;

      if (action == 'view') {

            var promise = $http.get('/webapi/ProjectApi/GetProject?projectId=' + projectId, {});
            promise.then(
                function (payload) {
                    var m = payload.data;

                    $scope.project = {
                        ProjectId: m.ProjectId,
                        Title: m.Title,
                        Description: m.Description,
                       
                       

                    };

                });


            //Load files and display in table
            var promise = $http.get('/webapi/MediaApi/GetFilesInFolder?folderId=' + $scope.mediaFolderId + '&mediaTypes=' + $scope.mediaTypes, {});
            promise.then(
                function (payload) {
                    //$scope.folder.Files = payload.data;
                    $scope.showFileProperties = false;
                    $scope.projectImages = payload.data;


                }
            );


         

        }


    }
    ]);

