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
        //vm.users = [];

        vm.activate1c = bootstrapFactory.showAccount1CActivationDialog;

        vm.reCalculate = calculateItems;

        vm.reLoad = loadDataFromServer;

        // ------------

        activate();

        function activate() {
            vm.viewMode = null;
            loadDataFromServer();
            //console.log(api1C);
            //api1C.users.query().then(
            // function(recive) {
            //     console.log("Success", recive);
            // },
            // function(recive) {
            //     console.log('Failed', recive);
            // }
            //);

        }

        function loadDataFromServer() {
            userFactory.query.then(
                 function (response) {
                     console.log("Success Response");

                     if (angular.isArray(response.data)) {
                         vm.originalData = response.data;
                         vm.totalItems = response.data.length;

                         calculateItems();
                     } else {
                         vm.originalData = [];
                         calculateItems();
                     }
                 }, function (response) {
                     console.log("Failed Response");

                     vm.originalData = [];
                     calculateItems();
                 });
        }

        function calculateItems() {
            vm.currentPage = 1;
            vm.users = $filter('access1c')(vm.originalData, vm.viewMode);
            //console.log(vm.users);
            vm.totalItems = vm.users.length;
        }
    }
})();
