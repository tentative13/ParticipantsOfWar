(function () {
    var app = angular.module('pow_app');

    app.controller('powCtrl', ['$rootScope', '$log', '$scope', 'ParticipantsService', 'participantsVM',
        function ($rootScope, $log, $scope, participantsService, participantsVM) {

            $scope.participantsVM = participantsVM;
            $scope.predicate = 'birthday';
            $scope.idSelectedRow = null;

            $scope.handlers = {
                LoadPage: function () { }
            };

            $scope.grid = {
                openMessages: [],
                rowindex: -1,
                showData: function (value) {
                    if (this.openMessages.length > 0 && this.openMessages.indexOf(value) > -1)
                        return true;
                    return false;
                },
                expandRow: function (item, index) {
                    $log.log(item, index);
                    $scope.idSelectedRow = item.guid;

                    if (this.openMessages.indexOf(index) === -1)
                        this.openMessages.push(index);
                    else
                        this.openMessages.splice(this.openMessages.indexOf(index), 1);
                },
                newmsgrow_shift: function () {

                    if (this.openMessages.length > 0)
                    {
                        for (var i = 0; i < this.openMessages.length; i++)
                        {
                            this.openMessages[i] = this.openMessages[i] + 1;
                        }

                    }
                },
            }




         //   $scope.handlers.LoadPage();
        }]);
})();
