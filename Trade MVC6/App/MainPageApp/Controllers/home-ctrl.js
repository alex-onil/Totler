(function () {
    'use strict';

    angular
        .module('mainApp')
        .controller('homeCtrl', controller);

    controller.$inject = ['$scope', '$routeParams', 'products'];

    function controller($scope, $routeParams, products) {
        $scope.title = 'homeCtrl';
        $scope.params = $routeParams;
        $scope.products = products.query();
        //console.log(products);
        $scope.products.$promise.then(function(result) {
            //console.log(result);
        });
        // console.log($scope.products);
        //activate();

        //function activate() { }
    }
})();
