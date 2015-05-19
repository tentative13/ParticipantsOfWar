(function () {
    var app = angular.module('pow_app');

    app.controller('powCtrl', ['$rootScope', '$log', '$scope', 'ParticipantsService', 'participantsVM', '$state', '$mdDialog',
        function ($rootScope, $log, $scope, participantsService, participantsVM, $state, $mdDialog) {
            $scope.participantsVM = participantsVM;
            $scope.Participants = participantsVM.Participants;
            $scope.predicate = '-type.value';
            $scope.idSelectedRow = null;
            $scope.filter = {};
            $scope.alphabet = ['А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ы', 'Э', 'Ю', 'Я'];

            $scope.handlers = {
                LoadPage: function () { },
                onDocumentClick: function (item) {
                    $rootScope.pow_details = item;
                    $rootScope.editMode = false;
                    $state.go("participants.details");
                },
                onCreateClick: function () {
                    $rootScope.pow_details = {};
                    $rootScope.editMode = true;
                    $rootScope.createMode = true;
                    $state.go("participants.create");
                },
                letterFilter: function (letter)
                {
                    $scope.filter.surname = letter;
                },
                onDateInputChange: function () {
                    participantsService.TimeZoneFixer($scope.filter);
                },
                onLoginClick: function (event) {
                    $mdDialog.show({
                        templateUrl: '/App_Front/views/dialog1_tmpl.html',
                        targetEvent: event,
                        locals: { participantsService: participantsService },
                        controller: function DialogController($scope, $mdDialog, participantsService) {
                            $scope.valid_error = false;
                            $scope.login_loader = false;
                            $scope.onloginClick = function () {
                                $scope.login_loader = true;
                                participantsService.LogIn($scope.login, $scope.password)
                                .then(
                                    function () {
                                        //auth ok
                                        $mdDialog.hide();
                                        $scope.login_loader = false;
                                    },
                                    function () {
                                        //auth not ok
                                        $scope.valid_error = true;
                                        $scope.login_loader = false;
                                    });
                            };
                            $scope.oncancelClick = function () {
                                $mdDialog.cancel();
                            };
                        }
                    })
                }

            };

            $scope.grid = {
                openRows: [],
                rowindex: -1,
                showData: function (value) {
                    if (this.openRows.length > 0 && this.openRows.indexOf(value) > -1)
                        return true;
                    return false;
                },
                expandRow: function (item, index) {
                    $scope.idSelectedRow = item.guid;
                    if (this.openRows.indexOf(index) === -1)
                        this.openRows.push(index);
                    else
                        this.openRows.splice(this.openRows.indexOf(index), 1);
                },
                row_shift: function () {
                    if (this.openRows.length > 0){
                        for (var i = 0; i < this.openRows.length; i++){
                            this.openRows[i] = this.openRows[i] + 1;
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
