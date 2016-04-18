(function () {
    'use strict';

    angular
        .module('mainApp')
        .directive('compareTo', compareToDirective);

    compareToDirective.$inject = ['$window'];

    function compareToDirective($window) {

        // Creates:
        // 

        var directive = {
            require: "ngModel",
            scope: {
                otherModelValue: "=compareTo"
            },
            link: link,
            restrict: 'A'
        };

        return directive;

        function link(scope, element, attrs, ngModel) {
            ngModel.$validators.compareTo = function (modelValue) {
                console.log(scope);
                return modelValue === scope.otherModelValue;
            };

            scope.$watch("otherModelValue", function () {
                ngModel.$validate();
            });
        }
    }

})();