﻿(function () {
    'use strict';

    angular
        .module('mainApp')
        .directive('emailDuplicateCheck', usersDirective);

    usersDirective.$inject = ['$q', 'dataFactory'];

    function usersDirective($q, dataFactory) {

        var directive = {
            require: 'ngModel',
            link: link,
            restrict: 'A',
            scope: {
                antiforgery: "=antiforgery"
            }
        };
        return directive;

        function link(scope, elm, attrs, ctrl) {

            ctrl.$asyncValidators.emailDuplicateCheck = function (modelValue, viewValue) {

                if (ctrl.$isEmpty(modelValue)) {
                    // consider empty model valid
                    return $q.when();
                }

                var def = $q.defer();

                dataFactory.emailDuplicateCheck(modelValue, scope.antiforgery).then(function (reciveBuf) {
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