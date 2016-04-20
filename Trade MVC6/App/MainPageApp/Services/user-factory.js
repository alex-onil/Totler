(function () {
    'use strict';

    angular
        .module('mainApp')
        .factory('userFactory', userFactory);

    userFactory.$inject = ['$http', '$rootScope', 'AppConfig'];

    function userFactory($http, $rootScope, config) {

        var opts = {
            url: config.api.userCrudUrl,
            headers: { "__RequestVerificationToken": $rootScope.antiforgery }
        }

        var service = {
            // get: $$get,
            save: $$save,
            // query: $$query,
            remove: $$remove,
            create: $$create
        };

        Object.defineProperty(service,'get', { get: $$get });

        Object.defineProperty(service, 'query', { get: $$query });

        return service;

        function $$get(id) {
            opts.url += "/" + id;
            opts.method = "GET";

            return $http(opts);
        }

        function $$create(value) {
            opts.data = value;
            opts.method = "POST";

            return $http(opts);
        }

        function $$save(value) {
            opts.url += "/" + value.id;
            opts.data = value;
            opts.method = "PUT";

            return $http(opts);
        }

        function $$query() {
            opts.method = "GET";

            return $http(opts);
        }

        function $$remove(id) {
            opts.url += "/" + id;
            opts.method = "DELETE";

            return $http(opts);
        }
    }
})();