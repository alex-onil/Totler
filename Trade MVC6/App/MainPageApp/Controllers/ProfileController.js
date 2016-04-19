(function () {
    'use strict';

    angular
        .module('mainApp')
        .controller('ProfileController', profileController);

    profileController.$inject = ['$location', '$templateCache', '$rootScope', '$route', 'AppConfig', 'dataFactory', 'bootstrapFactory'];

    function profileController($location, $templateCache, $rootScope, $route, appConfig, dataFactory, bootstrapFactory) {
        /* jshint validthis:true */

        $templateCache.remove(appConfig.accountProfileUrl);
        // or
        //$templateCache.removeAll();

        // variable

        var vm = this;
        vm.data = {}
        vm.allowChangeCompany = false;
        vm.sendingData = false;

        // properties

        Object.defineProperty(vm, "access1C", {
            get: function () {
                if (vm.data.Access1C) return "B2B доступ разрешен";
                return "B2B доступ ЗАПРЕЩЁН";
            }
        });

        // activate

        activate();

        function activate() { }

        // function

        vm.debug = function () {
            console.log(vm);
        }

        vm.cancel = function () {
            $rootScope.toPreviousPage();
        }

        vm.save = function () {
            vm.sendingData = true;
            dataFactory.sendForm(appConfig.accountProfileUrl, vm.data).then(function () {
                bootstrapFactory.showModalConfirmation("Аккаунт успешно обновлен.").then(function () {
                    $rootScope.toPreviousPage();
                });
            }, function (evt) {
                bootstrapFactory.showModalErrors("Ошибка обновления данных", evt.data);
                vm.sendingData = false;
            });
        }

        vm.changeCompany = function () {
            vm.allowChangeCompany = true;
        }

        vm.changeEmail = function () {
            var result = bootstrapFactory.showEmailChangeRequest();
            console.log(result);
            result.closed.then(function() {
                $route.reload();
            });
        }

        vm.sendConfirmation = function () {
            // необходимо выслать подтверждение email
            var result = dataFactory.sendRequest(appConfig.requestEmailConfirmationUrl, {});
            result.then(function () {
                bootstrapFactory.showModalConfirmation('Письмо для подверждения электронного адреса отправлено на email:' +
                                                    vm.data.Email);
            }, function () {
                bootstrapFactory.showModalConfirmation('В процессе отправки письма произошла ошибка.');
            });

        }
    }
})();
