(function () {
    'use strict';

    angular
        .module('mainApp')
        .controller('Account1CActivateController', emailChangeController);


    emailChangeController.$inject = ['$uibModalInstance', 'account', 'api1CFactory', 'userFactory', 'bootstrapFactory'];

    function emailChangeController($uibModalInstance, account, api1C, userFactory, bF) {

        var vm = this;

        //vm.data = {}

        vm.IsSending = false;

        vm.reLoadUsers = $$LoadUsers;

        activate();
        // ------------------------


        function activate() {
            $$LoadUsers();
        }

        vm.close = function () {
            $uibModalInstance.dismiss();
        }

        vm.ok = function () {
            console.log($uibModalInstance, account, api1C, userFactory);
            var result = userFactory.activate1c(account.Id, vm.selected.Id);
            result.then(function (recive) {
                console.log("Activate Success ");
                //console.log(recive);
                angular.extend(account, recive.data);
                //console.log(account);
                //account = recive.data;
                $uibModalInstance.close();
            }, function (recive) {
                console.log("Failed activate: " + recive);
                bF.showModalErrors("Ошибка", recive.data);
            });
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

        function $$LoadUsers() {
            vm.accounts1C = [];
            api1C.users.query().then(function(recive) {
                console.log("Load Success " + recive);
                if (angular.isArray(recive.data)) {
                    vm.accounts1C = recive.data;
                }
            }, function(recive) {
                console.log("Failed load: " + recive);
                bF.showModalErrors("Ошибка", recive.data);
            });
        }

    }

})();