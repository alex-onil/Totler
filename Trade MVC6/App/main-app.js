(function () {
    'use strict';

    angular.module('mainApp', [
        // Angular modules 
        'ngRoute', 

        // Custom modules 
        'main-app.service'
        // 3rd Party Modules
        
    ]).config(["$routeProvider", "$locationProvider", function ($routeProvider, $locationProvider) {
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: true
        });

        $routeProvider.when("/Home/option1/:message", {
            templateUrl: "/views/option1.html",
            controller: "homeCtrl"
        });

        $routeProvider.when("/Home/option2/:message", {
            templateUrl: "/views/option2.html",
            controller: "homeCtrl"
        });

        $routeProvider.when("/Home/option3/:message", {
            templateUrl: "/views/option3.html",
            controller: "homeCtrl"
        });

        $routeProvider.otherwise({
            templateUrl: "/views/base.html"
        });
    }]);
})();