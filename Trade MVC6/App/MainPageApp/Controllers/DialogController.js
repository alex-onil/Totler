(function () {
    'use strict';

    angular
        .module('mainApp')
        .controller('DialogController', dialogController);

    dialogController.$inject = ['$uibModalInstance', 'messageData'];

    function dialogController($uibModalInstance, messageData) {
        /* jshint validthis:true */
        var vm = this;
        vm.info = messageData.info;

        vm.close = function () {
            $uibModalInstance.close();
        }

        activate();

        function activate() {
            console.log(messageData);
        }


    }
})();
