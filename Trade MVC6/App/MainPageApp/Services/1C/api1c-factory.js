(function () {
    'use strict';

    angular
        .module('mainApp')
        .factory('api1CFactory', _1CFactory);

    _1CFactory.$inject = ['user1CFactory'];

    function _1CFactory(user1CFactory) {
        var service = {};

        Object.defineProperty(service, 'users', {
            get: function() { return user1CFactory; }
        });

        return service;
    }
})();