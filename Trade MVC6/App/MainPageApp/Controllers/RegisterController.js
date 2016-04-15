(function () {
    'use strict';

    angular
        .module('mainApp')
        .controller('RegisterController', registerController);

    registerController.$inject = ['$location', '$rootScope', 'dataFactory'];

    function registerController($location, $rootScope, dF) {
        /* jshint validthis:true */
        var vm = this;

        // -------------------------

        vm.cancel = function () {
            if ($rootScope.previousPage) {
                $location.url($rootScope.previousPage);
                return;
            }
            $location.url('/');
        }

        vm.submit = function(antiforgery) {
            console.log("submit click");
            dF.sendUserRegistration(vm.data, antiforgery);
        }

        activate();

        function activate() {
            vm.submited = false;
        }
    }
})();
