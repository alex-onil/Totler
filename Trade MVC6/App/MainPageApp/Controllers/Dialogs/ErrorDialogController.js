(function () {
    'use strict';

    angular
        .module('mainApp')
        .controller('ErrorDialogController', errorDialogController);

    errorDialogController.$inject = ['$uibModalInstance', 'messageData'];

    function errorDialogController($uibModalInstance, mD) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'ErrorDialogController';
        vm.ErrorMessages = mD.errorMessages;
        vm.header = mD.headerText;

        // --------------------------------

        vm.close = function () {
            $uibModalInstance.close();
        }

        activate();

        function activate() { }
    }
})();
