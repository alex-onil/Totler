(function () {
    'use strict';

    angular
        .module('mainApp')
        .factory('bootstrapFactory', bootstrapDialog);

    bootstrapDialog.$inject = ['$uibModal', '$q'];

    function bootstrapDialog($uibModal, $q) {
        var service = {
            showModalConfirmation: $$ShowModalcfrm,
            showModalErrors: $$ShowModalErr,
            showEmailChangeRequest: $$ShowEmailChange,
            showAccount1CActivationDialog: $$ShowAccount1CActivation,
            showYesNo: $$ShowYesNo
        };

        // ---------------------------

        var modalTemplate = '<div class="modal-body">' +
            '<div class="panel panel-info" ng-show="true">' +
            '   <div class = "panel-body"> ' +
            '      <p>{{Ctrl.info}}</p>' +
            '   </div>' +
            ' </div>' +
            ' <button type="button" class="btn btn-success" data-ng-click="Ctrl.close()">Ок</button> ' +
            '</div>';

        // --------------------------


        // --------------------------

        return service;

        function $$ShowModalcfrm(message) {

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

        function $$ShowModalErr(header, messages) {

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

        function $$ShowEmailChange() {

            //console.log("show change email");

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

        function $$ShowAccount1CActivation(account) {
            var opts = {
                backdrop: false,
                keyboard: true,
                backdropClick: true,
                size: 'lg',
                templateUrl: '/views/dialogs/Account1CActivate.html',
                controller: 'Account1CActivateController',
                controllerAs: 'Ctrl'
            };

            opts.resolve = {
                account: account
            }

            return $uibModal.open(opts);
        }

        function $$ShowYesNo(message) {

            var template = '<div class="modal-body" ng-cloak>' +
                '<p>{{Ctrl.message}}</p>' +
                ' <button type="button" class="btn btn-warning" data-ng-click="Ctrl.yes()">Да</button> ' +
                ' <button type="button" class="btn btn-success" data-ng-click="Ctrl.no()">Нет</button> ' +
                '</div>';

            var opts = {
                backdrop: true,
                keyboard: true,
                backdropClick: true,
                size: 'lg',
                template: template,
                controller: ['$uibModalInstance', function ($uibModalInstance) {
                    var vm = this;
                    vm.message = message;
                    vm.yes = $uibModalInstance.close;
                    vm.no = $uibModalInstance.dismiss;
                }],
                controllerAs: 'Ctrl'
            };

            return $uibModal.open(opts).result;
        }
    }

})();