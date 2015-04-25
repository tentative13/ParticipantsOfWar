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
            };




         //   $scope.handlers.LoadPage();
        }]);
})();
