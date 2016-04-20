(function() {
    'use strict';

    angular
        .module('mainApp')
        .component('userManagment', {
            templateUrl: "/views/Admin/UserManagment.html",
            controller: "UserManagmentController",
            controllerAs: "$ctrl"
        });

})();