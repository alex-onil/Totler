(function () {
    'use strict';

    angular
        .module('mainApp')
        .controller('UserManagmentController', userManagmentController);

    userManagmentController.$inject = ['$location', '$filter', 'userFactory', 'bootstrapFactory'];

    function userManagmentController($location, $filter, userFactory, bootstrapFactory) {
        /* jshint validthis:true */


        var vm = this;
        vm.title = 'UserManagmentController';

        // ---- pagination -----
        vm.maxSize = 5;
        vm.itemsPerPage = "5";
        vm.currentPage = 1;

        // Editor & Switch
        vm.editor = {}
        vm.mode = '';

        vm.activate1c = bootstrapFactory.showAccount1CActivationDialog;

        vm.deactivate1c = $$Deactivate1C;

        vm.reCalculate = $$ReCalculate;

        vm.reLoad = $$LoadDataFromServer;

        vm.editUser = $$EditUser;

        vm.deleteUser = $$Deleteuser;

        //Editor operation
        vm.editor.save = $$Save;

        // ------------

        activate();

        function activate() {
            vm.viewMode = null;
            vm.btnDisabled = false;
            $$LoadDataFromServer();
            
        }

        function $$LoadDataFromServer() {
            vm.btnDisabled = true;
            userFactory.query.then(
                 function (response) {
                     console.log("Success Response");

                     if (angular.isArray(response.data)) {
                         vm.originalData = response.data;
                         vm.totalItems = response.data.length;

                         $$ReCalculate();
                     } else {
                         vm.originalData = [];
                         $$ReCalculate();
                     }
                 }, function (response) {
                     console.log("Failed Response");

                     vm.originalData = [];
                     $$ReCalculate();
                 });
            vm.btnDisabled = false;
        }

        function $$ReCalculate() {
            vm.currentPage = 1;
            vm.users = $filter('access1c')(vm.originalData, vm.viewMode);
            vm.totalItems = vm.users.length;
        }

        function $$Deactivate1C(user) {
            vm.btnDisabled = true;
            var result = bootstrapFactory.showYesNo("Отключить доступ к 1С для аккаунта " + user.Nickname + '(' +
            user.Email + ')');
            result.then(function(recive) {
                var updateResult = userFactory.deactivate1C(user.Id);
                updateResult.success(function(recive) {
                    console.log(recive);
                    angular.extend(user, recive);
                });
            });
            vm.btnDisabled = false;
        }

        function $$EditUser(user) {
            vm.btnDisabled = true;
            console.log("Editing mode");
            vm.editor.submited = false;
            vm.editor.sumbitting = false;
            vm.editor.model = {};
            angular.copy(user, vm.editor.model);
            vm.editor.user = user;
            vm.mode = 'edit';
            vm.btnDisabled = false;
        }

        function $$Save() {

            vm.btnDisabled = true;
            vm.editor.submited = true;

            if (!vm.editor.model.$valid) {
                console.log("Model Not Valid!");
                vm.btnDisabled = false;
                return;
            }

            vm.editor.sumbitting = true;

            var result = userFactory.save(vm.editor.model);
            result.then(function(recive) {
                console.log("Success Save", recive);
                angular.copy(recive.data, vm.editor.user);
                bootstrapFactory.showModalConfirmation("Данные аккаунта успешно обновлены.")
                                .then(function () { vm.mode = ''; });

            }, function(recive) {
                console.log("Failed Save");
                bootstrapFactory.showModalErrors(recive.data);
                vm.editor.sumbitting = false;
            });

            vm.btnDisabled = false;
        }

        function $$Deleteuser(user) {

            vm.btnDisabled = true;
            var result = bootstrapFactory.showYesNo("Удалить пользователя " + user.Nickname + ' (' +
             user.Email + ') ?');
            result.then(function() {
                userFactory.remove(user.Id)
                    .then(function() {
                        vm.users.splice(vm.users.indexOf(user), 1);
                        bootstrapFactory.showModalConfirmation("Пользователь удалён.");
                    }, function (recive) {
                        console.log(recive);
                        bootstrapFactory.showModalErrors("Ошибка удаления пользователя:",recive.data);
                    });
            });
            vm.btnDisabled = false;
        }
    }
})();
