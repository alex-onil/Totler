(function () {
    'use strict';

    angular
        .module('mainApp')
        .controller('Account1CActivateController', emailChangeController);


    emailChangeController.$inject = ['$uibModalInstance', 'account', 'api1CFactory', 'userFactory'];

    function emailChangeController($uibModalInstance, account, api1C, userFactory) {

        var vm = this;

        vm.data = {}

        vm.IsSending = false;

        activate();
        // ------------------------


        function activate() {
            
        }

        vm.close = function () {
            $uibModalInstance.dismiss();
        }

        vm.ok = function () {
            console.log($uibModalInstance, account, api1C, userFactory);
            //vm.IsSending = true;
            //var result = dF.sendEmailChangeRequest(vm.data.Email);
            //result.then(function () {
            //    $uibModalInstance.close();
            //    bF.showModalConfirmation("Для изменения Email следуйте дальнейшим инструкциям " +
            //        "в письме отправленном на новый электронный адрес( " + vm.data.Email + " ). ");
            //}, function() {
            //    bF.showModalErrors("Ошибка изменения Email.", "При изменении электронного адреса произошла ошибка.");
            //    vm.IsSending = false;
            //});
        }

    }

})();