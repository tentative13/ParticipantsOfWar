(function () {
    var app = angular.module('pow_app');

    app.controller('powCtrl', ['$rootScope', '$log', '$scope', 'ParticipantsService', 'participantsVM', '$state', 'photoSlider',
        function ($rootScope, $log, $scope, participantsService, participantsVM, $state, photoSlider) {
            $scope.showFirstPage = true;
            $scope.participantsVM = participantsVM;
            $scope.Participants = participantsVM.Participants;
            $scope.idSelectedRow = null;
            $scope.filter = {};
            $scope.alphabet = ['А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ы', 'Э', 'Ю', 'Я'];
            $scope.predicate = '-type.value';

            if ($rootScope.predicate_holder) $scope.predicate = $rootScope.predicate_holder;

            $scope.photoSlider = photoSlider;
            $scope.photoSlider.slides.push({ image: 'Content/images/1.jpg', description: '1' });
            $scope.photoSlider.slides.push({ image: 'Content/images/2.jpg', description: '2' });
            $scope.photoSlider.slides.push({ image: 'Content/images/3.jpg', description: '3' });
            $scope.photoSlider.slides.push({ image: 'Content/images/4.jpg', description: '4' });
            $scope.photoSlider.slides.push({ image: 'Content/images/5.jpg', description: '5' });
            $scope.photoSlider.slides.push({ image: 'Content/images/6.jpg', description: '6' });
            $scope.photoSlider.slides.push({ image: 'Content/images/7.jpg', description: '7' });
            $scope.photoSlider.slides.push({ image: 'Content/images/8.jpg', description: '8' });
            $scope.photoSlider.slides.push({ image: 'Content/images/9.jpg', description: '9' });
            $scope.photoSlider.slides.push({ image: 'Content/images/10.jpg', description: '10' });
            $scope.photoSlider.slides.push({ image: 'Content/images/11.jpg', description: '11' });
            var timerId = setInterval(function () {
                $rootScope.$apply(photoSlider.prevSlide());
            }, 5000);

            $scope.handlers = {
                LoadPage: function () { },
                onDocumentClick: function (item) {
                    $rootScope.pow_details = item;
                  //  $scope.grid.openRows = [];
                    $rootScope.predicate_holder = $scope.predicate;
                    $rootScope.editMode = false;
                    $state.go("participants.details");
                },
                onCreateClick: function () {
                  //  $scope.grid.openRows = [];
                    $rootScope.predicate_holder = $scope.predicate;
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
                    console.log($scope.filter);
                    if (typeof $scope.filter.birthday === "undefined") {
                        delete $scope.filter.birthday;
                        return;
                    }
                    if ($scope.filter.birthday === "") {
                        delete $scope.filter.birthday;
                        return;
                    }
                    if ($scope.filter.birthday === null) {
                        delete $scope.filter.birthday;
                        return;
                    }


                    participantsService.TimeZoneFixer($scope.filter);
                },
                shortedCheck: function (item) {

                    if (item.length > 1000) return true;

                    var crlfCount = item.match(/[\n\r]|[\r\n]/g);

                    if (crlfCount && crlfCount.length > 5) return true;

                    return false;
                },
                onCancelClick: function () {
                    $scope.photoSlider.clearSlides();
                    clearInterval(timerId);
                    $scope.showFirstPage = false;
                }
            };

            $scope.grid = {
                openRows: [],
                rowindex: -1,
                closeAllRows: function(){
                    this.openRows = [];
                },
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
