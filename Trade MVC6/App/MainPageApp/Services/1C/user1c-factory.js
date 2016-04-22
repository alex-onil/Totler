(function () {
    'use strict';

    angular
        .module('mainApp')
        .factory('user1CFactory', user1CFactory);

    user1CFactory.$inject = ['$http', '$rootScope', 'AppConfig'];

    function user1CFactory($http, $rootScope, config) {
        var service = {
            //get: $get,
            query: $query
        };

        var req = {
            url: config.api1C.apiUsers,
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                "__RequestVerificationToken": $rootScope.antiforgery
            }
        }

        return service;

        function $query() {
            req.method = 'GET';
            return $http(req);
        }

        //function $get(id) { }
    }
})();