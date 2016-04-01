(function () {
    'use strict';

    angular
        .module('mainApp')
        .controller('accountCtrl', account);

    account.$inject = [ "$scope", "$http"]; 

    function account($scope, $http) {
        /* jshint validthis:true */

        $scope.logOff = function () {
            console.log("Click LogOff");

            var req = {
                method: 'POST',
                url: '/Account/Home/LogOff',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8'//"application/json; charset=utf-8"
                }
            }
            req.data = $("form").serialize();
            console.log(req);

            $http(req).then(function (response) {
                console.log("Result OK");
                console.log(response);
            }, function (response) {
                console.log("Result Error");
                console.log(response);
            });
        }
    }
})();
