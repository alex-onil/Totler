(function () {
    'use strict';

    angular
        .module('mainApp')
        .controller('ProfileController', ProfileController);

    ProfileController.$inject = ['$location']; 

    function ProfileController($location) {
        /* jshint validthis:true */

        // variable

        var vm = this;
        vm.data = {}

        // properties

        Object.defineProperty(vm, "access1C", {
            get: function() {
                if (vm.data.Access1C) return "B2B доступ разрешен";
                return "B2B доступ ЗАПРЕЩЁН";
            } 
        });

        // activate

        activate();

        function activate() { }

        // function

        vm.debug = function() {
            console.log(vm);
        }

        vm.cancel = function () {
            console.log(vm.returnUrl.length);
            if (vm.returnUrl && vm.returnUrl.length > 0) {
                $location.url(vm.returnUrl);
                return;
            }
            $location.url("/");
        }

    }
})();
