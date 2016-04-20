(function () {
    'use strict';

    angular
        .module('mainApp')
        .controller('RegisterController', registerController);

    registerController.$inject = ['$location', '$rootScope', 'dataFactory', 'bootstrapFactory'];

    function registerController($location, $rootScope, dF, bD) {
        /* jshint validthis:true */
        var vm = this;
        vm.submiting = false;
        // -------------------------

        vm.cancel = function () {
            if ($rootScope.previousPage) {
                $location.url($rootScope.previousPage);
                return;
            }
            $location.url('/');
        }

        vm.submit = function () {
            vm.submiting = true;
            var result = dF.sendUserRegistration(vm.data);
            result.then(function () {

                //console.log("Send Success");

                var result = bD.showModalConfirmation("Регистрация прошла успешно. Письмо с инструкциями по подтверждению электронного адреса " +
                    "Вашей учетной записи отправлено на указанный адрес Email." +
                    "Для работы в системе B2B допускаются пользователи допущенные администратором" +
                    "и успешно подтвердившие адрес электронной почты.");

                result.then(function () {
                    if ($rootScope.previousPage) {
                        $location.url($rootScope.previousPage);
                        return;
                    }
                    $location.url('/');
                });

            }, function (evt) {
                //console.log("Send Error");
                //console.log(evt);
                if (angular.isArray(evt.data)) {
                    var result = bD.showModalErrors("Ошибка регистрации на сервере:", evt.data);
                    result.then(function () {
                        vm.submiting = false;
                    });
                } else {
                    vm.submiting = false;
                }

            });
        }

        activate();

        function activate() {
            vm.submited = false;

            // if Authorized then redirect to root
            if ($rootScope.isAuth) {
                $location.url('/');
            }
        }
    }
})();
