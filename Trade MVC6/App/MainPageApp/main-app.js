(function () {
    'use strict';

    angular.module('mainApp', [
        // Angular modules 
        'ngRoute', 

        // Custom modules 
        'main-app.service'
        // 3rd Party Modules
        
    ]).constant("validExternalLink", ["/b2b", "/Login"])
       .config(["$routeProvider", "$locationProvider", "validExternalLink",
         function ($routeProvider, $locationProvider, validExternalLink) {
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

        //$routeProvider.when("/b2b", {
        //    redirectTo: function (routeParams, path, search) {
        //        window.location.replace(path);
        //    }
        //});

        $routeProvider.when("/", {
            templateUrl: "/views/base.html"
        });

        $routeProvider.when("/Home/Error", {
            templateUrl: "/views/base.html"
        });

        $routeProvider.otherwise({
            redirectTo: function (routeParams, path) {
                if (validExternalLink.indexOf(path) > -1) {
                    window.location.replace(path);
                }
                return "/Home/Error";
            }
        });

    }]);
})();