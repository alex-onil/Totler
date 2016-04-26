(function () {
    'use strict';

    angular
        .module('mainApp')
        .factory('userFactory', userFactory);

    userFactory.$inject = ['$http', '$rootScope', 'AppConfig'];

    function userFactory($http, $rootScope, config) {

        var opts = {
            url: config.api.userCrudUrl,
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                "__RequestVerificationToken": $rootScope.antiforgery
            }
        }

        var service = {
            // get: $$get,
            save: $$Save,
            // query: $$query,
            remove: $$Remove,
            activate1c: $$Activate,
            deactivate1C: $$Deactivate
        };

        Object.defineProperty(service,'get', { get: $$Get });

        Object.defineProperty(service, 'query', { get: $$Query });

        return service;

        function $$Get(id) {
            var request = angular.extend({
                method: "POST"
            }, opts);

            request.url += "/" + id;

            return $http(request);
        }

        function $$Save(value) {
            var request = angular.extend({
                method: "PUT",
                data: value
            }, opts);

            request.url += "/" + value.Id;

            return $http(request);
        }

        function $$Query() {
            var request = angular.extend({
                method: "GET"
            }, opts);

            return $http(request);
        }

        function $$Remove(id) {

            var request = angular.extend({
                method: "DELETE"
            }, opts);

            request.url += "/" + id;

            return $http(request);
        }

        function $$Activate(userId, id1C) {
            var request = angular.extend({
                method: 'POST',
                params: { id1C: id1C}
            }, opts);

            request.url += "/" + userId;

            return $http(request);

        }

        function $$Deactivate(userId) {

            var request = angular.extend({
                method: 'POST'
            }, opts);

            request.url += "/" + userId + "/Deactivate"; 

            return $http(request);
        }
    }
})();