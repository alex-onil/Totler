(function () {
    'use strict';

    angular
        .module('mainApp')
        .controller('ProfileController', ProfileController);

    ProfileController.$inject = ['$location', 'dataService', 'bootstrapDialog'];

    function ProfileController($location, dataService, bootstrapDialog) {
        /* jshint validthis:true */

        // variable

        var vm = this;
        vm.data = {}
        vm.allowChangeCompany = false;

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

        vm.save = function () {
            
            dataService.sendForm($location.path(), vm.data, vm.antiforgery).then(function () {
                console.log("Form sended");
            }, function() {
                console.log("Form send Error");
            });
        }

        vm.changeCompany = function() {
            vm.allowChangeCompany = true;
        }

        vm.changeEmail = function() {
            // Необходимо выслать изменение email
            bootstrapDialog.showModalConfiramtion('Письмо с инструкциями по изменению электронного адреса выслано на email:' + 
                                                 vm.data.Email);
        }

        vm.sendConfirmation = function () {
            // необходимо выслать подтверждение email
            bootstrapDialog.showModalConfiramtion('Письмо для подверждения электронного адреса отправлено на email:' +
                                                vm.data.Email);
        }
    }
})();
