(function () {
    var app = angular.module('pow_app');

    app.controller('powDetailsCtrl', ['$rootScope', '$log', '$scope', 'ParticipantsService', 'participantsVM', '$state',
        function ($rootScope, $log, $scope, participantsService, participantsVM, $state) {

            $scope.participant = $rootScope.pow_details;

            $scope.handlers = {
                onGetBackClick: function () {
                    $state.go("participants");
                }
            };


        }]);
})();