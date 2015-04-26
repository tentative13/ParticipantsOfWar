(function () {
    var app = angular.module('pow_app');

    app.controller('powCtrl', ['$rootScope', '$log', '$scope', 'ParticipantsService', 'participantsVM', '$state',
        function ($rootScope, $log, $scope, participantsService, participantsVM, $state) {

            $scope.participantsVM = participantsVM;
            $scope.predicate = '-type_value';
            $scope.idSelectedRow = null;
            $scope.Participants = [];
            $scope.filter = {};

            $scope.alphabet = ['А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ы', 'Э', 'Ю', 'Я'];

            $scope.handlers = {
                LoadPage: function () { },
                onDocumentClick: function (item) {
                    $rootScope.pow_details = item;
                    $state.go("participants.details");
                },
                letterFilter: function (letter)
                {
                    angular.forEach(participantsVM.Participants, function (item) {

                        if (item.surname[0].toLowerCase() === letter.toLowerCase()) {
                            var check = $.grep($scope.Participants, function (f) { return f.guid == item.guid; });
                            if (check.length === 0) {
                                $scope.Participants.push(item);
                            }
                        }
                    });

                    $scope.$apply(
                        $scope.filter.surname = letter
                    );
                    

                }
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
