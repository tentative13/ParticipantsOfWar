(function () {
    var app = angular.module('pow_app');

    app.controller('powCtrl', ['$rootScope', '$log', '$scope', 'ParticipantsService', 'participantsVM', '$state',
        function ($rootScope, $log, $scope, participantsService, participantsVM, $state) {

            $scope.participantsVM = participantsVM;
            $scope.Participants = participantsVM.Participants;
            $scope.predicate = '-type_value';
            $scope.idSelectedRow = null;
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
                    $scope.filter.surname = letter;
                },
                onDateInputChange: function () {
                    if (typeof $scope.filter.birthday != "undefined" && ($scope.filter.birthday instanceof Date)) {
                        var offset = new Date().getTimezoneOffset();
                        offset = (offset / 60) * (-1);
                        $scope.filter.birthday.setHours($scope.filter.birthday.getHours() + offset);
                    }
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
                filterbase : { firstname: '', middlename: '', surname: '', ParticipantsTypes: 0, birthday: '' },
                getLastCount: function (filter, tablelength) {
                    if (!(JSON.stringify(filter) === JSON.stringify(this.filterbase))) {
                        if ($.connection.hub.id != null) {
                            $scope.participantsVM.GetTotalFilteredParticipants(filter);
                        }
                        this.filterbase = angular.copy(filter);
                    }

                    var lastLength = $scope.participantsVM.TotalParticipants - tablelength;
                    return lastLength < 0 ? null : lastLength >= this.pageSize ? this.pageSize : lastLength;
                },
                showMore: function (filter) {
                    $scope.participantsVM.GetPageParticipants(filter, this.pageSize);
                },
                showAll: function (filter) {
                    $scope.participantsVM.GetAllParticipants(filter);
                }
            }

         //   $scope.handlers.LoadPage();
        }]);
})();
