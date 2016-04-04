(function () {
    'use strict';

    angular
        .module('mainApp')
        .controller('errorCtrl', errorCtrl);

    errorCtrl.$inject = ['$scope', '$routeParams']; 

    function errorCtrl($scope, $routeParams) {
        $scope.Path = $routeParams.Path;
        console.log("errorCtrlActivated");
        console.log($routeParams.Path);
    }
})();
