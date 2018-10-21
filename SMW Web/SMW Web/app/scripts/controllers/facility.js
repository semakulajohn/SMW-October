


angular
    .module('homer')
    .controller('FacilityDetailController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval', 'resizeService', 'FileUploader', 'appSettingsService',
    function ($scope, ngTableParams, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval, resizeService, FileUploader, appSettingsService) {

        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }

        var facilityId = $scope.facilityId;
        var action = $scope.action;
        var uploader = $scope.uploader = new FileUploader({
            url: '/upload/?parentId=' + $scope.mediaFolderId
        });

      

        $scope.mediaTypes = 2;



        if (action == 'view') {

                var promise = $http.get('/webapi/FacilityApi/GetFacility?facilityId=' + facilityId, {});
                promise.then(
                    function (payload) {
                        var m = payload.data;

                        $scope.facility = {
                            FacilityId: m.FacilityId,
                            Location: m.Location,
                            Description: m.Description,
                            FacilityTypeId: m.FacilityTypeId,
                            RentalPeriodId : m.RentalPeriodId,
                            MediaFolderId: m.MediaFolderId,
                            Dimensions :m.Dimensions,
                            FacilityTypeName: m.FacilityTypeName,
                            RentalPeriodName : m.RentalPeriodName,
                          


                        };

                    });
           
            //Load files and display in table
            var promise = $http.get('/webapi/MediaApi/GetFilesInFolder?folderId=' + $scope.mediaFolderId + '&mediaTypes=' + $scope.mediaTypes, {});
            promise.then(
                function (payload) {
                    //$scope.folder.Files = payload.data;
                    $scope.showFileProperties = false;
                    $scope.facilityImages = payload.data;


                }
            );



        }


    }
    ]);



angular
    .module('homer').controller('LatestFacilityController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;

            $scope.latestFacilities = [];

            var latestFacilitiesPromise = $http.get('/webapi/FacilityApi/GetLatestFacilities', {});
            latestFacilitiesPromise.then(
                 function (payload) {
                     $scope.latestFacilities = payload.data;
                 }
             );


        }]);

