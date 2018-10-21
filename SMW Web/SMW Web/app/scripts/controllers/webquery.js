angular
    .module('homer')
    .controller('WebQueryEditController', ['$scope', '$stateParams', 'ngTableParams', '$http', '$filter', '$location', '$log', 
    function ($scope, $stateParams, ngTableParams, $http, $filter, $location, $log) {

        $scope.loadingSpinner = false;

        var webQueryId = $stateParams.webQueryId;
        var action = $stateParams.action;


        if (action == 'create') {
            webQueryId = 0;
        }

        $scope.Save = function (webQuery) {
            $scope.loadingSpinner = true;
            $scope.showMessageSave = false;
            //if ($scope.form.$valid) {
                var promise = $http.post('/webapi/WebQueryApi/Save', {
                    WebQueryId: webQueryId,
                    Name: webQuery.Name,
                    PhoneNumber: webQuery.PhoneNumber,
                     EmailAddress: webQuery.EmailAddress,
                    Body: webQuery.Body,
                    CreatedOn: webQuery.CreatedOn,
               
                });

                promise.then(
                    function (payload) {
                        $scope.loadingSpinner = false;
                        $scope.showMessageSave = true;
                        //$scope.form.$setPristine();
                        setTimeout(function () {
                            $scope.$apply(function () {
                                $scope.webQuery.Name = "";
                                $scope.webQuery.WebQueryId = "";
                                $scope.webQuery.PhoneNumber = "";
                                $scope.webQuery.EmailAddress = "";
                                $scope.webQuery.Body = "";

                                $scope.showMessageSave = false;
                            });
                        }, 1500);
                    });
            }
        }

    //}
    ]);