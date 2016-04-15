(function () {
    'use strict';

    angular
        .module('mainApp')
        .controller('GlobalController', globalController);

    globalController.$inject = ['$location']; 

    function globalController($location) {
        /* jshint validthis:true */
        var vm = this;


        activate();

        function activate() { }
    }
})();
