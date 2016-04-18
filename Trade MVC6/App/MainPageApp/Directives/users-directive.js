(function () {
    'use strict';

    angular
        .module('mainApp')
        .directive('users', usersDirective);

    usersDirective.$inject = ['$q', 'dataFactory'];

    function usersDirective($q, dataFactory) {

        var directive = {
            require: 'ngModel',
            link: link,
            restrict: 'A',
            scope: {}
        };
        return directive;

        function link(scope, elm, attrs, ctrl) {

            ctrl.$asyncValidators.users = function (modelValue, viewValue) {

                if (ctrl.$isEmpty(modelValue)) {
                    // consider empty model valid
                    return $q.when();
                }

                var def = $q.defer();

                dataFactory.checkUser(modelValue).then(function (reciveBuf) {
                    if (reciveBuf.data) {
                        // The username is available
                        def.resolve();
                    } else {
                        def.reject();
                    }
                }, function () {
                    def.reject();
                });

                return def.promise;
            };

        }
    }

})();