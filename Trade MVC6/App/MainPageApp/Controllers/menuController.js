(function () {
    'use strict';

    angular
        .module('mainApp')
        .controller('MenuController', menuController);

    menuController.$inject = ['$scope','$location']; 

    function menuController($scope, $location) {
        var vm = this;

        vm.login = function () {
            var currentUrl = $location.url();
            $location.path('/Account/Login').search('ReturnUrl', currentUrl);
        }

        vm.logOff = function () {
            var currentUrl = $location.url();
            $location.path('/Account/LogOut').search('ReturnUrl', currentUrl);
        }

        vm.profile = function () {
            var currentUrl = $location.url();
            $location.path('/Account/Profile').search('ReturnUrl', currentUrl);
        }

        vm.b2b = function () {
            var currentUrl = $location.url();
            $location.path('/b2b');
        }
    }
})();
