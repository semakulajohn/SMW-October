angular
    .module('homer').controller('HomeController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', '$interval',
    function ($scope, ngTableParams, $http, $filter, $location, Utils, $interval) {
        var rentalId = $scope.rentalId;
        //$scope.rentalId = rentalId;
        $scope.latestProperties = [];
        $scope.data = [];
        $scope.featuredProperties = [];

        //var promise = $http.get('/webapi/PropertyApi/GetAllProperties', {});
        //promise.then(
        //    function (payload) {
        //        $scope.data = payload.data;
        //        $scope.tableParams = new ngTableParams({
        //            page: 1,
        //            count: 20,
        //            sorting: { CreatedOn: 'desc' }
        //        }, {
        //            getData: function ($defer, params) {
        //                var filteredData = $filter('filter')($scope.data, $scope.filter);
        //                var orderedData = params.sorting() ?
        //                                    $filter('orderBy')(filteredData, params.orderBy()) :
        //                                    filteredData;

        //                params.total(orderedData.length);
        //                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
        //            },
        //            $scope: $scope
        //        });
        //    }
        //);

        //$scope.$watch("filter.$", function () {
        //    if (!Utils.isUndefinedOrNull($scope.tableParams)) {
        //        $scope.tableParams.reload();
        //    }
        //});

      

        var featuredPropertiesPromise = $http.get('/webapi/PropertyApi/GetFeaturedProperties', {});
        featuredPropertiesPromise.then(
            function (payload) {
                $scope.featuredProperties = payload.data;
            }
        );

       
        var latestProjectsPromise = $http.get('/webapi/ProjectApi/GetLatestEightProjects', {});
        latestProjectsPromise.then(
            function (payload) {
                $scope.latestProjects = payload.data;
            }
        );

    }
    ]);


