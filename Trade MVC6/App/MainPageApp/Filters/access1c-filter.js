(function () {
    'use strict';

    angular
        .module('mainApp')
        .filter('access1c', access1CFilter);

    access1CFilter.$inject = [];

    function access1CFilter() {
        return function (input, filterValue) {
            //console.log(input, filterValue);

            if (angular.isArray(input)) {
                var result = [];
                input.forEach(function(value, index) {
                    if (value.Access1C === filterValue || filterValue === null) {
                        result.push(value);
                    }
                });
                //console.log(result);
                return result;
            }
            return input;
        }
    }
})();