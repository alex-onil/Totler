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
            create: $$Create,
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

        function $$Create(value) {
            //opts.data = value;
            //opts.method = "POST";
            var request = angular.extend({
                method: "POST",
                data: value
            }, opts);

            return $http(request);
        }

        function $$Save(value) {
            //opts.url += "/" + value.id;
            //opts.data = value;
            //opts.method = "PUT";

            var request = angular.extend({
                method: "PUT",
                data: value
            }, opts);

            request.url += "/" + value.id;

            return $http(request);
        }

        function $$Query() {
            //opts.method = "GET";
            var request = angular.extend({
                method: "GET"
            }, opts);

            return $http(request);
        }

        function $$Remove(id) {
            //opts.url += "/" + id;
            //opts.method = "DELETE";

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