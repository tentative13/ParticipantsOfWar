(function () {
    var app = angular.module('pow_app');

    app.controller('powCtrl', ['$rootScope', '$log', '$scope', 'ParticipantsService', 'participantsVM',
        function ($rootScope, $log, $scope, participantsService, participantsVM) {

            $scope.participantsVM = participantsVM;
            $scope.predicate = '-type_value';
            $scope.idSelectedRow = null;
            $scope.Participants = [];

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

            $scope.paging = {
                pageSize: 10,
                currpage: 1,
                getLastCount: function (filter, tablelength) {
                    var lastLength = participantsVM.TotalParticipants - $scope.Participants.length;// tablelength;
                    return lastLength < 0 ? null : lastLength >= this.pageSize ? this.pageSize : lastLength;
                },
                showMore: function (filter, remains) {
                    var j = 0;
                    for (var i = this.pageSize*this.currpage; i < participantsVM.TotalParticipants; i++) {

                        $scope.Participants.push(participantsVM.Participants[i]);
                        j++;
                        if (j == this.pageSize) break;
                    }
                    this.currpage++;

                },
                showAll: function (filter) {
                    for (var i = this.pageSize * this.currpage; i < participantsVM.TotalParticipants; i++) {
                        $scope.Participants.push(participantsVM.Participants[i]);
                    }
                }
            }

            $rootScope.$on('ParticipantsLoaded', function (e) {
                $scope.Participants = participantsVM.Participants.slice(0, $scope.paging.pageSize);
            });



         //   $scope.handlers.LoadPage();
        }]);
})();
