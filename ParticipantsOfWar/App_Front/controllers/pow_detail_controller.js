(function () {
    var app = angular.module('pow_app');

    app.controller('powDetailsCtrl', ['$rootScope', '$log', '$scope', 'ParticipantsService', 'participantsVM', '$state', '$timeout', 'DateToStr',
    function ($rootScope, $log, $scope, participantsService, participantsVM, $state, $timeout, DateToStr) {

        $scope.participant = $rootScope.pow_details;
        $scope.types = participantsVM.ParticipantsTypes;
      //  $scope.delayshow = false;
        $scope.isAuthorized = false;
        $scope.new_record = {};
        $scope.new_record.type = $scope.types[0];
        $scope.dateOptions = {
            changeYear: true,
            changeMonth: true,
            yearRange: '1900:-0',
            dateFormat: 'dd.mm.yy'
        };
        $scope.docFile = [];
        $scope.photoFile = [];
        $scope.birthday_str = '';
        $scope.death_str = '';
        $scope.slideInterval = 5000;
        $scope.slides = [];

        if ($scope.participant && $scope.participant.birthday) {
             $scope.birthday_str = DateToStr($scope.participant.birthday);
        }
        if ($scope.participant && $scope.participant.deathday) {
            $scope.death_str = DateToStr($scope.participant.deathday);
        }

    //    $timeout(function () { $scope.addSlides(); }, 1000);

        $scope.uploadFiles = function (participant) {
            if ($scope.photoFile && $scope.photoFile.length && participant && participant.guid) {
                //todo start loader
                for (var i = 0; i < $scope.photoFile.length; i++) {
                    var file = $scope.photoFile[i];
                    participantsService.UploadPhoto(file, participant.guid, function (data) {
                        //todo disable loader
                        $log.info('participantsService.UploadPhoto success', data);
                        angular.forEach(data, function (item) {
                            participant.photos.push(item);
                        });
                        participantsVM.UpdateCacheParticipants(participant);
                    });
                }
                $scope.photoFile = [];
            }


            if ($scope.docFile && $scope.docFile.length && participant && participant.guid) {
                //todo start loader
                for (var i = 0; i < $scope.docFile.length; i++) {
                    var file = $scope.docFile[i];
                    participantsService.UploadDocument(file, participant.guid, function (data) {
                        //todo disable loader
                        $log.info('participantsService.UploadDocument success', data);
                        angular.forEach(data, function (item) {
                            participant.documents.push(item);
                        });
                        participantsVM.UpdateCacheParticipants(participant);
                    });
                }
                $scope.docFile = [];
            }
        };

        $scope.addSlides = function () {
            $scope.slides = [];
            if ($scope.participant && $scope.participant.photos) {
                angular.forEach($scope.participant.photos, function (item) {
                    $scope.slides.push({
                        image: 'api/Documents/GetPhoto/' + item.photoId,
                        text: item.description
                    });
                });
            }
        };

        $scope.addSlides();

        $scope.handlers = {
            onGetBackClick: function () {
                this.onCancelClick();
                $rootScope.createMode = false;
                $state.go("participants");
            },
            GetDocument: function (documentId) {
                if (typeof documentId === "undefined") return;
                participantsService.getDocument(documentId);
            },
            onEditClick: function () {
                $rootScope.editMode = true;
                $scope.docFile = [];
                $scope.photoFile = [];
                $scope.new_record = angular.copy($scope.participant);

                for (var i = 0; i < $scope.types.length; i++) {
                    if ($scope.new_record.type.value == $scope.types[i].value) {
                        $scope.new_record.type = $scope.types[i];
                        break;
                    }
                }

            },
            onSaveClick: function () {
                //todo start loader
                $rootScope.editMode = false;//todo move to success, add loader

                if ($scope.new_record.birthday) {
                    $scope.new_record.birthday = (new Date($scope.new_record.birthday)).toJSON();
                    $scope.birthday_str = DateToStr($scope.new_record.birthday);
                }

                if ($scope.new_record.deathday) {
                    $scope.new_record.deathday = (new Date($scope.new_record.deathday)).toJSON();
                    $scope.death_str = DateToStr($scope.new_record.deathday);
                }

                if ($rootScope.createMode === true) {
                    participantsService.createParticipant($scope.new_record, function (data) {
                        //todo disable loader
                        $log.info('success createParticipant');
                        $scope.participant = angular.copy(data);
                        $scope.new_record = {};
                        $scope.participant.photos = [];
                        $scope.participant.documents = [];
                        $rootScope.createMode = false;
                        participantsVM.AddToCacheParticipants($scope.participant);
                        $scope.uploadFiles($scope.participant);
                    });
                }
                else{
                    participantsService.updateParticipant($scope.new_record.guid, $scope.new_record, function () {
                        //todo disable loader
                        $log.info('success update participant');
                        $scope.participant = angular.copy($scope.new_record);
                        $scope.new_record = {};
                        participantsVM.UpdateCacheParticipants($scope.participant);
                        $scope.uploadFiles($scope.participant);
                    });
                }

            },
            onCancelClick: function () {
                $rootScope.editMode = false;
                $scope.new_record = {};
                $scope.docFile = [];
                $scope.photoFile = [];
            },
            PhotoFileSelected: function (files) {
                if (files && files.length) {
                    angular.forEach(files, function (item) {
                        $scope.photoFile.push(item);
                    });
                }
            },
            DocumentFileSelected: function (files) {
                if (files && files.length) {
                    angular.forEach(files, function (item) {
                        $scope.docFile.push(item);
                    });
                }
            },
            RemoveFromDocSelected: function (index) {
                $scope.docFile.splice(index, 1);
            },
            RemoveFromPhotoSelected: function (index) {
                $scope.photoFile.splice(index, 1);
            }
        };
        }]);
})();


