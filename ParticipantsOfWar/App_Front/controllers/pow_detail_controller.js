(function () {
    var app = angular.module('pow_app');

    app.controller('powDetailsCtrl', ['$rootScope', '$log', '$scope', 'ParticipantsService', 'participantsVM',
        '$state', '$timeout', 'DateToStr', 'photoSlider', '$mdToast',
    function ($rootScope, $log, $scope, participantsService, participantsVM, $state, $timeout, DateToStr, photoSlider, $mdToast) {

        $scope.participant = $rootScope.pow_details;
        $scope.types = participantsVM.ParticipantsTypes;
        $scope.isAuthorized = false;
        $scope.new_record = {};
        $scope.focus = true;
        $timeout(function () {
            $scope.new_record.type = $scope.types[1];
        }, 50);
        $scope.dateOptions = {
            changeYear: true,
            changeMonth: true,
            yearRange: '1900:-0',
            dateFormat: 'dd.mm.yy'
        };
        $scope.docFile = [];
        $scope.photoFile = [];
        $scope.docForDelete = [];
        $scope.photoForDelete = [];
        $scope.birthday_str = '';
        $scope.death_str = '';
        $scope.photoSlider = photoSlider;

        if ($scope.participant && $scope.participant.birthday) {$scope.birthday_str = DateToStr($scope.participant.birthday);}
        if ($scope.participant && $scope.participant.deathday) {$scope.death_str = DateToStr($scope.participant.deathday);}



        $scope.dropFiles = function (participant) {
            if ($scope.docForDelete && $scope.docForDelete.length && participant && participant.guid) {
                for (var i = 0; i < $scope.docForDelete.length; i++) {
                    
                    participantsService.deleteDocument($scope.docForDelete[i].documentId, function (documentId, data) {
                        $log.info('participantsService.deleteDocument success', documentId, data);
                        for (var j = 0; j < participant.documents.length; j++) {
                            if (participant.documents[j].documentId == documentId) {
                                participant.documents.splice(j, 1);
                                break;
                            }
                        }
                        participantsVM.UpdateCacheParticipants(participant);
                    });
                }
                $scope.docForDelete = [];
            }

            if ($scope.photoForDelete && $scope.photoForDelete.length && participant && participant.guid) {
                for (var i = 0; i < $scope.photoForDelete.length; i++) {

                    participantsService.deletePhoto($scope.photoForDelete[i].photoId, function (photoId, data) {
                        $log.info('participantsService.deletePhoto success', photoId, data);
                        for (var j = 0; j < participant.photos.length; j++) {
                            if (participant.photos[j].photoId == photoId) {
                                participant.photos.splice(j, 1);
                                break;
                            }
                        }
                        participantsVM.UpdateCacheParticipants(participant);
                    });
                }
                $scope.photoForDelete = [];
            }
        };
        $scope.uploadFiles = function (participant) {
            if ($scope.photoFile && $scope.photoFile.length && participant && participant.guid) {
                for (var i = 0; i < $scope.photoFile.length; i++) {
                    var file = $scope.photoFile[i];
                    participantsService.UploadPhoto(file, participant.guid, function (data) {
                        $log.info('participantsService.UploadPhoto success', data);
                        angular.forEach(data, function (item) {
                            participant.photos.push(item);
                        });
                        photoSlider.addSlides(data);
                        participantsVM.UpdateCacheParticipants(participant);
                    });
                }
                $scope.photoFile = [];
            }


            if ($scope.docFile && $scope.docFile.length && participant && participant.guid) {
                for (var i = 0; i < $scope.docFile.length; i++) {
                    var file = $scope.docFile[i];
                    participantsService.UploadDocument(file, participant.guid, function (data) {
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
            photoSlider.clearSlides();
            if ($scope.participant && $scope.participant.photos) {
                photoSlider.addSlides($scope.participant.photos);
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
                $scope.docForDelete = [];
                $scope.photoForDelete = [];

                $scope.new_record = angular.copy($scope.participant);

                for (var i = 0; i < $scope.types.length; i++) {
                    if ($scope.new_record.type.value == $scope.types[i].value) {
                        $scope.new_record.type = $scope.types[i];
                        break;
                    }
                }

            },
            onSaveClick: function () {

                //validation
                if (typeof $scope.new_record.surname === "undefined") {
                    $rootScope.showSimpleToast('Не задана фамилия!');
                    return;
                }

                if ($scope.new_record.surname && $scope.new_record.surname.length == 0) {
                    $rootScope.showSimpleToast('Не задана фамилия!');
                    return;
                }
                if ($scope.new_record.type.value <= 0) {
                    $rootScope.showSimpleToast('Не установлен статус!');
                    return;
                }

                

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
                        $log.info('success createParticipant');
                        $rootScope.editMode = false;
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
                        $rootScope.editMode = false;
                        $log.info('success update participant');
                        $scope.participant = angular.copy($scope.new_record);
                        $scope.new_record = {};
                        participantsVM.UpdateCacheParticipants($scope.participant);
                        $scope.dropFiles($scope.participant);
                        $scope.uploadFiles($scope.participant);
                    });
                }

            },
            onCancelClick: function () {
                $rootScope.editMode = false;
                $scope.new_record = {};
                $scope.docFile = [];
                $scope.photoFile = [];
                $scope.docForDelete = [];
                $scope.photoForDelete = [];
                if ($rootScope.createMode === true) {
                    $rootScope.createMode = false;
                    $state.go("participants");
                }
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
            },
            DeleteDocument: function (item) {
                if (item && item.documentId) {
                    var check = $.grep($scope.docForDelete, function (f) { return f.documentId == item.documentId; });
                    if (check.length === 0) $scope.docForDelete.push(item);
                }
            },
            DeletePhoto: function (item) {
                if (item && item.photoId) {
                    var check = $.grep($scope.photoForDelete, function (f) { return f.photoId == item.photoId; });
                    if (check.length === 0) $scope.photoForDelete.push(item);
                }
            },
            isDocDeleted: function(item){

                var check = $.grep($scope.docForDelete, function (f) { return f.documentId == item.documentId; });
                if (check.length === 0)
                    return false;
                else
                    return true;
            },
            unDeleteDocument: function(item){
                for (var j = 0; j < $scope.docForDelete.length; j++) {
                    if ($scope.docForDelete[j].documentId == item.documentId) {
                        $scope.docForDelete.splice(j, 1);
                        break;
                    }
                }
            },
            unDeletePhoto: function (item) {
                for (var j = 0; j < $scope.photoForDelete.length; j++) {
                    if ($scope.photoForDelete[j].photoId == item.photoId) {
                        $scope.photoForDelete.splice(j, 1);
                        break;
                    }
                }
            }
        };
        }]);
})();