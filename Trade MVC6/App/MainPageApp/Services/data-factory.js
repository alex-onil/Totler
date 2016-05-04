(function () {
    'use strict';

    angular
        .module('mainApp')
        .factory('dataFactory', dataService);

    dataService.$inject = ['$http', '$rootScope', 'AppConfig'];

    function dataService($http, $rootScope, appConfig) {
        var service = {
            sendForm: sendFormData,
            sendRequest: sendRequestByUrl,
            //getUsers: sendUsersRequest,
            checkUser: sendCheckUserRequest,
            emailDuplicateCheck: sendCheckEmailDuplicate,
            sendUserRegistration: sendUserRegistration,
            sendEmailChangeRequest: sendEmailChangeRequest

        };

        return service;

        // Declaration

        function sendFormData(url, data) {

            if (url === "" || angular.isUndefined(url)) return $q.reject("No Url in Request");

            var req = {
                method: 'POST',
                url: url,
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    "__RequestVerificationToken": $rootScope.antiforgery
                }
            }

            req.data = angular.toJson(data);

            //if (angular.isDefined($rootScope.antiforgery)) {
            //    req.headers["__RequestVerificationToken"] = $rootScope.antiforgery;
            //}

            console.log(req);

            return $http(req);
        }

        function sendRequestByUrl(url, data) {
            return $http.post(url, data, {
                headers: { "__RequestVerificationToken": $rootScope.antiforgery }
            });

        }

        //function sendUsersRequest(antiForgeryToken) {
        //    return $http.get(AppConfig.requestUsers, {
        //        headers: { "__RequestVerificationToken": antiForgeryToken }
        //    });
        //}

        function sendCheckUserRequest(name) {
            return $http.get(appConfig.requestCheckUser, {
                headers: { "__RequestVerificationToken": $rootScope.antiforgery },
                params: {"userName" : name}
            });
        }

        function sendCheckEmailDuplicate(email) {
            return $http.get(appConfig.requestChekEmail, {
                headers: { "__RequestVerificationToken": $rootScope.antiforgery },
                params: { "email" : email }
            });
        }

        function sendUserRegistration(data) {
            var config = {
                headers: { "__RequestVerificationToken": $rootScope.antiforgery }
            }
            return $http.post(appConfig.sendRegister, data, config );
        }

        function sendEmailChangeRequest(newEmail) {
            
            var config = {
                headers: { "__RequestVerificationToken": $rootScope.antiforgery },
                params: { "newEmail": newEmail }
            }
            return $http.post(appConfig.requestChangeEmailUrl, newEmail, config);
        }
    }
})();