(function () {
    'use strict';

    angular
       .module('mainApp')
       .constant('AppConfig', appConfig());

    appConfig.$inject = [];

    function appConfig() {
        return {
            requestChangeEmailUrl: "/Account/EmailChangeRequest",
            requestEmailConfirmationUrl: "/Account/ReSendEmailConfirmation",
            //requestUsers: "/Account/UserNames",
            requestCheckUser: "/Account/CheckUser",
            requestChekEmail: "/Account/CheckEmailDuplicate",
            sendRegister: "/Account/Register",
            accountProfileUrl: "/Account/Profile",
            api : {
                userCrudUrl: '/Api/Users'
            },
            api1C : {
                apiUsers: '/api/1C/Users'
            }
        }
    }

})();