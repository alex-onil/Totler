(function () {
    'use strict';

    angular
        .module('mainApp')
        .factory('bootstrapDialog', bootstrapDialog);

    bootstrapDialog.$inject = ['$uibModal'];

    function bootstrapDialog($uibModal) {
        var service = {
            showModalConfiramtion: showModalcfrm
        };

        // ---------------------------

        var modalTemplate = '<div class="modal-body">' +
            '<div class="panel panel-info" ng-show="true">' +
            '   <div class = "panel-body"> '+
            '      <p>{{Ctrl.info}}</p>' +
            '   </div>' +
            ' </div>' +
            ' <button type="button" class="btn btn-success" data-ng-click="Ctrl.close()">Ок</button> ' +
            '</div>';

        var opts = {
            backdrop: false,
            keyboard: true,
            backdropClick: true,
            size: 'lg',
            template: modalTemplate,
            controller: 'DialogController',
            controllerAs: 'Ctrl'
        };
        // --------------------------

        return service;

        function showModalcfrm(message) {
            opts.resolve = {
                messageData: {
                    info: message
                }
            }

            var modalInstance = $uibModal.open(opts);
        }
    }

})();