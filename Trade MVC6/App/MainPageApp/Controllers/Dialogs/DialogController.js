(function () {
    'use strict';

    angular
        .module('mainApp')
        .controller('DialogController', dialogController);
        //.controller('EmailChangeDialogController', emailChangeController);

    // Base controller
    dialogController.$inject = ['$uibModalInstance', 'messageData'];

    function dialogController($uibModalInstance, messageData) {
        /* jshint validthis:true */
        var vm = this;
        vm.info = messageData.info;

        vm.close = function () {
            $uibModalInstance.close();
        }

    }

    //// Childs -------------

    //// Email Change child
    //emailChangeController.$inject = ['$uibModalInstance', 'messageData'];

    //function emailChangeController($uibModalInstance, messageData) {

    //    dialogController.call(this);

    //    var vm = this;

    //    vm.data = {}

    //    vm.ok = function() {
    //        console.log("Send Email Change Request");
    //    }

    //}

    //emailChangeController.prototype = Object.create(dialogController.prototype);
})();