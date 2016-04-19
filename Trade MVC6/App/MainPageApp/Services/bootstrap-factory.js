(function () {
    'use strict';

    angular
        .module('mainApp')
        .factory('bootstrapFactory', bootstrapDialog);

    bootstrapDialog.$inject = ['$uibModal', '$q'];

    function bootstrapDialog($uibModal, $q) {
        var service = {
            showModalConfirmation: showModalcfrm,
            showModalErrors:showModalErr,
            showEmailChangeRequest: showEmailChange
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

        // --------------------------


        // --------------------------

        return service;

        function showModalcfrm(message) {

            var opts = {
                backdrop: false,
                keyboard: true,
                backdropClick: true,
                size: 'lg',
                template: modalTemplate,
                controller: 'DialogController',
                controllerAs: 'Ctrl'
            };

            opts.resolve = {
                messageData: {
                    info: message
                }
            }

            return $uibModal.open(opts);
        }

        function showModalErr(header, messages) {

            if (!angular.isArray(messages)) {
                return $q.when();
            }

            var opts = {
                backdrop: false,
                keyboard: true,
                backdropClick: true,
                size: 'lg',
                templateUrl: "/views/dialogs/ErrorMessages.html",
                controller: 'ErrorDialogController',
                controllerAs: 'Ctrl'
            };

            opts.resolve = {
                messageData: {
                    headerText: header,
                    errorMessages: messages
                }
            }

            return $uibModal.open(opts);
        }

        function showEmailChange() {

            console.log("show change email");
            
            var opt = {
                backdrop: false,
                keyboard: true,
                backdropClick: true,
                size: 'lg',
                templateUrl: '/views/dialogs/EmailChange.html',
                controller: 'EmailChangeController',
                controllerAs: 'Ctrl'
            }

            return $uibModal.open(opt);
        }

    }

})();