﻿(function () {
    'use strict';

    angular.module('mainApp', [
        // Angular modules 
        'ngRoute', 

        // Custom modules 
        'main-app.service'
        // 3rd Party Modules
        
    ]).constant("validExternalLink", ["/b2b", "/Account/LogOut", "/Account/Login"]) //
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

        // Base View

        $routeProvider.when("/", {
            templateUrl: "/views/base.html"
        });

        $routeProvider.when("/Home", {
            templateUrl: "/views/base.html"
        });

        $routeProvider.when("/Home/Index", {
            templateUrl: "/views/base.html"
        });

        // Static view

        $routeProvider.when("/Home/Contacts", {
            templateUrl: "/views/MainPageApp/Contacts.html"
        });
        
        $routeProvider.when("/Home/Server", {
            templateUrl: "/views/MainPageApp/Servers.html"
        });

        $routeProvider.when("/Home/Storage", {
            templateUrl: "/views/MainPageApp/Storage.html"
        });

        $routeProvider.when("/Home/Certificates", {
            templateUrl: "/views/MainPageApp/Certificates.html"
        });

        $routeProvider.when("/Home/Partners", {
            templateUrl: "/views/MainPageApp/Partners.html"
        });

        $routeProvider.when("/Home/Vacancies", {
            templateUrl: "/views/MainPageApp/Vacancies.html"
        });
        
        // Error page

        $routeProvider.when("/Home/Error", {
            templateUrl: "/views/Shared/Error.html",
            controller: "errorCtrl"
        });

        $routeProvider.otherwise({
            redirectTo: function (routeParams, path, search) {
                if (validExternalLink.indexOf(path) > -1) {
                    var srch = '';
                    for(var index in search) {
                        srch += index + "=" + search[index];
                    }

                    if (srch === '') window.location.replace(path);
                    window.location.replace(path + "?" + srch);
                    return path + "?" + srch;
                }

                return "/Home/Error" + "?Path=" + path;
            }
        });

    }]);
})();