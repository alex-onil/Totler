(function () {
    'use strict';

    angular
        .module('mainApp')
        .controller('MenuController', menuController);

    menuController.$inject = ['$scope', '$location'];

    function menuController($scope, $location) {
        var vm = this;

        vm.register = function () {
            $location.path('/Account/Register');
        }

        vm.login = function () {
            moveToUrlWithReturn('/Account/Login');
        }

        vm.logOff = function () {
            moveToUrlWithReturn('/Account/LogOut');
        }

        vm.profile = function () {
            moveToUrlWithReturn('/Account/Profile');
        }

        vm.b2b = function () {
            $location.path('/b2b');
        }

        function moveToUrlWithReturn(url) {
            if ($location.path() === url) return;
            var currentUrl = $location.url();
            $location.path(url).search('ReturnUrl', currentUrl);
        }

    }
})();
