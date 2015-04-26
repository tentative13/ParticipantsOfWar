(function () {
    var app = angular.module('pow_app');

    app.controller('powDetailsCtrl', ['$rootScope', '$log', '$scope', 'ParticipantsService', 'participantsVM', '$state', '$timeout',
    function ($rootScope, $log, $scope, participantsService, participantsVM, $state, $timeout) {

        $scope.participant = $rootScope.pow_details;
        $scope.delayshow = false;

        $timeout(function () { $scope.delayshow = true; }, 1000);

            $scope.handlers = {
                onGetBackClick: function () {
                    $state.go("participants");
                },
                GetDocument: function (documentId) {
                    if (typeof documentId === "undefined") return;
                    participantsService.getDocument(documentId);
                }
            };
        }]);
})();