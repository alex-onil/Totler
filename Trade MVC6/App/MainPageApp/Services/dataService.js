(function () {
    'use strict';

    angular
        .module('mainApp')
        .factory('dataService', dataService);

    dataService.$inject = ['$http'];

    function dataService($http) {
        var service = {
            sendForm: sendFormData
        };

        return service;

        // Declaration

        function sendFormData(url, data, antiForgeryToken) {

            if (url === "" || angular.isUndefined(url)) return;

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
    }
})();