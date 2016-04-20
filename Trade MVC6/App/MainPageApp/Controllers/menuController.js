(function () {
    'use strict';

    angular
        .module('mainApp')
        .controller('MenuController', menuController);

    menuController.$inject = ['$scope', '$location'];

    function menuController($scope, $location) {
        var vm = this;

        vm.register = function () {
            $location.path('/Home/RegisterAccount');
        }

        vm.profile = function () {
            $location.path('/Home/AccountProfile');
        }

        vm.login = function () {
            moveToUrlWithReturn('/Account/Login');
        }

        vm.logOff = function () {
            moveToUrlWithReturn('/Account/LogOut');
        }

        vm.profile = function () {
            $location.path('/Home/AccountProfile');
        }

        vm.b2b = function () {
            $location.path('/b2b');
        }

        vm.admin = function () {
            $location.path('/Admin');
        }

        function moveToUrlWithReturn(url) {
            if ($location.path() === url) return;
            var currentUrl = $location.url();
            $location.path(url).search('ReturnUrl', currentUrl);
        }

    }
})();
