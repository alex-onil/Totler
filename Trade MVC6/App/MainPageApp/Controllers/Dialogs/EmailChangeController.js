(function () {
    'use strict';

    angular
        .module('mainApp')
        .controller('EmailChangeController', emailChangeController);


    emailChangeController.$inject = ['$uibModalInstance', 'dataFactory', 'bootstrapFactory'];

    function emailChangeController($uibModalInstance, dF, bF) {

        var vm = this;

        vm.data = {}

        vm.IsSending = false;

        // ------------------------

        vm.close = function () {
            $uibModalInstance.close();
        }

        vm.ok = function () {
            vm.IsSending = true;
            var result = dF.sendEmailChangeRequest(vm.data.Email);
            result.then(function () {
                $uibModalInstance.close();
                bF.showModalConfirmation("Для изменения Email следуйте дальнейшим инструкциям " +
                    "в письме отправленном на новый электронный адрес( " + vm.data.Email + " ). ");
            }, function() {
                bF.showModalErrors("Ошибка изменения Email.", "При изменении электронного адреса произошла ошибка.");
                vm.IsSending = false;
            });
        }

    }

})();