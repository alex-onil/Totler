(function () {
    'use strict';

    angular
        .module('mainApp')
        .controller('UserManagmentController', userManagmentController);

    userManagmentController.$inject = ['$location', 'userFactory'];

    function userManagmentController($location, userFactory) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'UserManagmentController';

        activate();

        function activate() {
            console.log(userFactory);
            userFactory.query.then(
            function(response) {
                console.log("Success Response");
                console.log(response);
            }, function (response) {
                console.log("Failed Response");
                console.log(response);
            });
        }
    }
})();
