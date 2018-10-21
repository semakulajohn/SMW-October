angular
    .module('homer').controller('RentalPropertyController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var propertyTypeId = 2;
          
            var rentalPropertiesPromise =  $http.get('/webapi/PropertyApi/GetPropertiesForAParticularPropertyType?propertyTypeId=' + propertyTypeId, {});
            rentalPropertiesPromise.then(
                function (payload) {
                    $scope.rentalProperties = payload.data;
                }
            );




        }]);


angular
    .module('homer').controller('ForSalePropertyController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var propertyTypeId = 1;

            var salePropertiesPromise = $http.get('/webapi/PropertyApi/GetPropertiesForAParticularPropertyType?propertyTypeId=' + propertyTypeId, {});
            salePropertiesPromise.then(
                function (payload) {
                    $scope.saleProperties = payload.data;
                }
            );




        }]);


angular
    .module('homer')
    .controller('PropertyDetailController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval', 'resizeService', 'FileUploader', 'appSettingsService',
    function ($scope, ngTableParams, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval, resizeService, FileUploader, appSettingsService) {

        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }
       
        var propertyId = $scope.propertyId;
        var action = $scope.action;
        var uploader = $scope.uploader = new FileUploader({
            url: '/upload/?parentId=' + $scope.mediaFolderId
        });
        $http.get('/webapi/PropertyApi/GetAllPropertyTypes').success(function (data, status) {
            $scope.propertyTypes = data;
        });

        $scope.mediaTypes = 2;

      

        if (action == 'view') {

            var promise = $http.get('/webapi/PropertyApi/GetProperty?propertyId=' + propertyId, {});
            promise.then(
                function (payload) {
                    var m = payload.data;

                    $scope.property = {
                        PropertyId: m.PropertyId,
                        Location: m.Location,
                        Description: m.Description,
                        PropertyTypeId: m.PropertyTypeId,
                        PropertyFee: m.PropertyFee,
                        MediaFolderId: m.MediaFolderId,
                        PropertyTypeName : m.PropertyTypeName,


                    };

                });


      


        //Load files and display in table
        var promise = $http.get('/webapi/MediaApi/GetFilesInFolder?folderId=' + $scope.mediaFolderId + '&mediaTypes=' + $scope.mediaTypes, {});
        promise.then(
            function (payload) {
                //$scope.folder.Files = payload.data;
                $scope.showFileProperties = false;
                $scope.propertyImages = payload.data;

                
            }
        );


      
       
        }
      
       
    }
    ]);



angular
    .module('homer').controller('LatestPropertyController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
           
            $scope.latestProperties = [];

            var latestPropertiesPromise = $http.get('/webapi/PropertyApi/GetLatestFourProperties', {});
       latestPropertiesPromise.then(
            function (payload) {
                $scope.latestProperties = payload.data;
            }
        );


        }]);

