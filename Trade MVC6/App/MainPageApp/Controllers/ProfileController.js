(function () {
    'use strict';

    angular
        .module('mainApp')
        .controller('ProfileController', profileController);

    profileController.$inject = ['$location', 'AppConfig', 'dataFactory', 'bootstrapFactory', '$templateCache'];

    function profileController($location, appConfig, dataFactory, bootstrapFactory, $templateCache) {
        /* jshint validthis:true */

        $templateCache.remove('/Account/Profile');
        // or
        //$templateCache.removeAll();

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
            
            dataFactory.sendForm($location.path(), vm.data, vm.antiforgery).then(function () {
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
            var result = bootstrapFactory.showEmailChangeRequest();
            //result.then(function () {
            //    bootstrapFactory.showModalConfirmation('Письмо с инструкциями по изменению электронного адреса выслано на email:' +
            //        vm.data.Email);
            //}, function () {
            //    bootstrapFactory.showModalConfirmation('В процессе отправки письма произошла ошибка.');
            //});

        }

        vm.sendConfirmation = function () {
            // необходимо выслать подтверждение email
            var result = dataFactory.sendRequest(appConfig.requestEmailConfirmationUrl, {}, vm.antiforgery);
            result.then(function () {
                bootstrapFactory.showModalConfirmation('Письмо для подверждения электронного адреса отправлено на email:' +
                                                    vm.data.Email);
            }, function () {
                bootstrapFactory.showModalConfirmation('В процессе отправки письма произошла ошибка.');
            });

        }
    }
})();
