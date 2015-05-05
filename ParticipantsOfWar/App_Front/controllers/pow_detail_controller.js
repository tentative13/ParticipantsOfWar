(function () {
    var app = angular.module('pow_app');

    app.controller('powDetailsCtrl', ['$rootScope', '$log', '$scope', 'ParticipantsService', 'participantsVM', '$state', '$timeout',
    function ($rootScope, $log, $scope, participantsService, participantsVM, $state, $timeout) {

        $scope.participant = $rootScope.pow_details;
        $scope.types = participantsVM.ParticipantsTypes;
        $scope.delayshow = false;
        $scope.isAuthorized = false;
        $scope.editMode = false;


        $timeout(function () { $scope.delayshow = true; }, 1000);

            $scope.handlers = {
                onGetBackClick: function () {
                    $state.go("participants");
                },
                GetDocument: function (documentId) {
                    if (typeof documentId === "undefined") return;
                    participantsService.getDocument(documentId);
                },
                onEditClick: function () {
                    $scope.editMode = true;
                },
                onSaveClick: function () {
                    $scope.editMode = false;
                },
                onCancelClick: function () {
                    $scope.editMode = false;
                },
                onDateInputChange: function () {
                    if (typeof $scope.participant.birthday != "undefined" && ($scope.participant.birthday instanceof Date)) {
                        var offset = new Date().getTimezoneOffset();
                        offset = (offset / 60) * (-1);
                        $scope.participant.birthday.setHours($scope.participant.birthday.getHours() + offset);
                    }
                }
            };
        }]);
})();


