(function () {
    'use strict';

    angular
        .module('main-app.service', ['ngResource'])
        .factory('products', moduleMy);
        
        moduleMy.$inject = ['$resource'];

        function moduleMy($resource) {
            return $resource('/api/Products');
    }
})();