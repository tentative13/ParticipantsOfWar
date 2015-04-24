(function () {
    var app = angular.module('pow_app');

    app.controller('powCtrl', ['$rootScope', '$log', '$scope', 'ParticipantsService', 'participantsVM',
        function ($rootScope, $log, $scope, participantsService, participantsVM) {

            $scope.participantsVM = participantsVM;


            $scope.handlers = {
                LoadPage: function () {}
            };




         //   $scope.handlers.LoadPage();
        }]);
})();
