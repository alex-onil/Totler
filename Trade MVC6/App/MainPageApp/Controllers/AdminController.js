(function () {
    'use strict';

    angular
        .module('mainApp')
        .controller('AdminController', adminController);

    adminController.$inject = ['$location']; 

    function adminController($location) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AdminController';

        activate();

        function activate() { }
    }
})();
