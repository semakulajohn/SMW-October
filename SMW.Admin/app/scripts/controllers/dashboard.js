
///**
//    * Data for Bar chart
//    */
//angular
//    .module('homer').controller('GraphController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
//        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
//            $scope.loadingSpinner = true;

//            //var promise = $http.get('/webapi/UserApi/GetLoggedInUser', {});
//            //promise.then(
//            //    function (payload) {
//            //        var c = payload.data;

//            //        $scope.user = {
//            //            UserName: c.UserName,
//            //            Id: c.Id,
//            //            FirstName: c.FirstName,
//            //            LastName: c.LastName,
//            //            UserRoles: c.UserRoles,
//            //            RoleName : c.RoleName,
//            //        };
//            //    }
//            //);

//            $http.get('/webapi/ClientApi/GetAllServices').success(function (data, status) {
//                $scope.services = data;
//            });

//            //if ($scope.user.RoleName == 'agent') {

//                var promise = $http.get('/webapi/CommissionApi/GetGraphData');
//                promise.then(
//                    function (payload) {
//                        $scope.graphData = payload.data;

//                        $scope.barData = {

                      
//                            labels: ["Web Hosting", "SMS", "KayeDex", "Web Development"],

//                            datasets: [
//                                {
//                                    label: "My Fourth dataset",
//                                    fillColor: "rgba(98,203,49,0.5)",
//                                    strokeColor: "rgba(98,203,49,0.8)",
//                                    highlightFill: "rgba(98,203,49,0.75)",
//                                    highlightStroke: "rgba(98,203,49,1)",
//                                    data: [$scope.graphData[0].TotalCommission, $scope.graphData[1].TotalCommission, $scope.graphData[2].TotalCommission,$scope.graphData[3].TotalCommission]
                                    
//                                },
                               

//                            ]

//                        };

//                    }
//                );

//            //}          

//        }]);





angular
    .module('homer').controller('DashboardController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/DashBoardApi/GetDashBoardData');
            promise.then(
                function (payload) {
                    $scope.dashBoardData = payload.data;
                    $scope.loadingSpinner = false;
                }
            );

            

        }]);
