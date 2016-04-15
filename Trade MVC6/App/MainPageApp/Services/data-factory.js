(function () {
    'use strict';

    angular
        .module('mainApp')
        .factory('dataFactory', dataService);

    dataService.$inject = ['$http', 'AppConfig'];

    function dataService($http, appConfig) {
        var service = {
            sendForm: sendFormData,
            sendRequest: sendRequestByUrl,
            //getUsers: sendUsersRequest,
            checkUser: sendCheckUserRequest,
            emailDuplicateCheck: sendCheckEmailDuplicate,
            sendUserRegistration: sendUserRegistration

        };

        return service;

        // Declaration

        function sendFormData(url, data, antiForgeryToken) {

            if (url === "" || angular.isUndefined(url)) return $q.reject("No Url in Request");

            var req = {
                method: 'POST',
                url: url,
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }

            req.data = angular.toJson(data);

            if (angular.isDefined(antiForgeryToken)) {
                req.headers["__RequestVerificationToken"] = antiForgeryToken;
            }

            console.log(req);

            return $http(req);
        }

        function sendRequestByUrl(url, data, antiForgeryToken) {
            return $http.post(url, data, {
                 headers: { "__RequestVerificationToken": antiForgeryToken }
            });

        }

        //function sendUsersRequest(antiForgeryToken) {
        //    return $http.get(AppConfig.requestUsers, {
        //        headers: { "__RequestVerificationToken": antiForgeryToken }
        //    });
        //}

        function sendCheckUserRequest(name, antiForgeryToken) {
            return $http.get(appConfig.requestCheckUser + "?userName=" + name, {
                headers: { "__RequestVerificationToken": antiForgeryToken }
            });
        }

        function sendCheckEmailDuplicate(email, antiForgeryToken) {
            return $http.get(appConfig.requestChekEmail + "?email=" + email, {
                headers: { "__RequestVerificationToken": antiForgeryToken }
            });
        }

        function sendUserRegistration(data, antiForgeryToken) {
            console.log(data);
            console.log(antiForgeryToken);
            return $http.post(appConfig.sendRegister, {
                headers: { "__RequestVerificationToken": antiForgeryToken }, data: data });
        }
    }
})();