angular
    .module('homer').controller('DateController', ['$scope',
        function ($scope) {
           
            $scope.date = new Date();

        }]);