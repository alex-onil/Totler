(function () {
    'use strict';

    angular
        .module('mainApp')
        .controller('ErrorController', errorCtrl);

    errorCtrl.$inject = ['$scope', '$routeParams']; 

    function errorCtrl($scope, $routeParams) {
        $scope.Path = $routeParams.Path;
    }
})();
