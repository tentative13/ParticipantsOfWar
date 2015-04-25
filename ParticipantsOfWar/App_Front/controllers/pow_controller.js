(function () {
    var app = angular.module('pow_app');

    app.controller('powCtrl', ['$rootScope', '$log', '$scope', 'ParticipantsService', 'participantsVM',
        function ($rootScope, $log, $scope, participantsService, participantsVM) {

            $scope.participantsVM = participantsVM;
            $scope.predicate = 'birthday';
            $scope.idSelectedRow = null;

            $scope.handlers = {
                LoadPage: function () { },
                expandRow: function (item, index) {
                    $log.log(item, index);
                    $scope.idSelectedRow = item.guid;
                },
                showData: function (index) { },
                loader: function () {
                    return participantsVM.Participants.lenght > 0;
                }
            };




         //   $scope.handlers.LoadPage();
        }]);
})();
