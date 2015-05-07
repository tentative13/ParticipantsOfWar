(function () {
    var app = angular.module('pow_app');

    app.controller('powDetailsCtrl', ['$rootScope', '$log', '$scope', 'ParticipantsService', 'participantsVM', '$state', '$timeout', 'Upload',
    function ($rootScope, $log, $scope, participantsService, participantsVM, $state, $timeout, Upload) {

        $scope.participant = $rootScope.pow_details;
        $scope.types = participantsVM.ParticipantsTypes;
        $scope.delayshow = false;
        $scope.isAuthorized = false;
        $scope.new_record = {};
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

        
        if (typeof $scope.participant != "undefined") {
            if (typeof $scope.participant.birthday != "undefined") {
                var datestr = $scope.participant.birthday.split("T")[0];
                var date_components = datestr.split("-");
                $scope.birthday_str = date_components[2] + '.' + date_components[1] + '.' + date_components[0];
            }
        }

        if (typeof $scope.participant != "undefined") {
            if (typeof $scope.participant.deathday != "undefined") {
                var datestr = $scope.participant.deathday.split("T")[0];
                var date_components = datestr.split("-");
                $scope.death_str = date_components[2] + '.' + date_components[1] + '.' + date_components[0];
            }
        }

        $timeout(function () { $scope.delayshow = true; }, 1000);

            $scope.handlers = {
                onGetBackClick: function () {
                    $rootScope.createMode = false;
                    $state.go("participants");
                },
                GetDocument: function (documentId) {
                    if (typeof documentId === "undefined") return;
                    participantsService.getDocument(documentId);
                },
                onEditClick: function () {
                    $rootScope.editMode = true;
                    $scope.new_record = angular.copy($scope.participant);
                },
                onSaveClick: function () {
                    $rootScope.editMode = false;//todo move to success, add loader

                    //todo move to factory
                    if (typeof $scope.new_record.birthday != "undefined") {
                        var date_components = $scope.new_record.birthday.split("-");
                        $scope.birthday_str = date_components[2] + '.' + date_components[1] + '.' + date_components[0];
                    }
                    //todo move to factory
                    if (typeof $scope.new_record.deathday != "undefined") {
                        date_components = $scope.new_record.deathday.split("-");
                        $scope.death_str = date_components[2] + '.' + date_components[1] + '.' + date_components[0];
                    }


                    if ($rootScope.createMode === true) {

                        participantsService.createParticipant($scope.new_record, function (data) {
                            //todo disable loader
                            $log.info('success create');
                            $scope.participant = angular.copy(data);
                            $scope.participant.photos = [];
                            $scope.participant.documents = [];

                            if ($scope.photoFile && $scope.photoFile.length && data && data.guid) {
                                for (var i = 0; i < $scope.photoFile.length; i++) {
                                    var file = $scope.photoFile[i];
                                    participantsService.UploadPhoto(file, data.guid, function (data) {
                                        $log.log('participantsService.UploadPhoto success', data);

                                        angular.forEach(data, function (item) {
                                            $scope.participant.photos.push(item);
                                        });

                                        
                                        participantsVM.AddToCacheParticipants($scope.participant);
                                        $scope.photoFile = [];
                                    });
                                }
                            }
                            
                            if ($scope.docFile && $scope.docFile.length && data && data.guid) {
                                for (var i = 0; i < $scope.docFile.length; i++) {
                                    var file = $scope.docFile[i];

                                    participantsService.UploadDocument(file, data.guid, function (data) {
                                        $log.log('participantsService.UploadDocument success', data);

                                        angular.forEach(data, function (item) {
                                            $scope.participant.documents.push(item);
                                        });

                                        participantsVM.AddToCacheParticipants($scope.participant);
                                        $scope.docFile = [];
                                    });
                                }
                            }

                            participantsVM.AddToCacheParticipants($scope.participant);
                        });
                        $rootScope.createMode = false;
                    }
                    else {

                        $scope.participant = angular.copy($scope.new_record);

                        participantsService.updateParticipant($scope.participant.guid, $scope.participant, function () {
                            //todo disable loader
                            $log.info('success update participant');
                            $scope.new_record = {};

                            if ($scope.photoFile && $scope.photoFile.length && $scope.participant && $scope.participant.guid) {
                                for (var i = 0; i < $scope.photoFile.length; i++) {
                                    var file = $scope.photoFile[i];
                                    participantsService.UploadPhoto(file, $scope.participant.guid, function (data) {
                                        $log.log('participantsService.UploadPhoto success', data);

                                        angular.forEach(data, function (item) {
                                            $scope.participant.photos.push(item);
                                        });

                                        participantsVM.AddToCacheParticipants($scope.participant);
                                        $scope.photoFile = [];
                                    });
                                }
                            }

                            if ($scope.docFile && $scope.docFile.length && $scope.participant && $scope.participant.guid) {
                                for (var i = 0; i < $scope.docFile.length; i++) {
                                    var file = $scope.docFile[i];

                                    participantsService.UploadDocument(file, $scope.participant.guid, function (data) {
                                        $log.log('participantsService.UploadDocument success', data);

                                        angular.forEach(data, function (item) {
                                            $scope.participant.documents.push(item);
                                        });

                                        participantsVM.AddToCacheParticipants($scope.participant);
                                        $scope.docFile = [];
                                    });
                                }
                            }


                            participantsVM.UpdateCacheParticipants($scope.participant);
                        });
                    }

                },
                onCancelClick: function () {
                    $rootScope.editMode = false;
                    $rootScope.createMode = false;
                    $scope.new_record = {};
                },
                onDateInputChange: function () {
                    //todo move to factory
                    if (typeof $scope.participant.birthday != "undefined" && ($scope.participant.birthday instanceof Date)) {
                        var offset = new Date().getTimezoneOffset();
                        offset = (offset / 60) * (-1);
                        $scope.participant.birthday.setHours($scope.participant.birthday.getHours() + offset);
                    }
                },
                PhotoFileSelected: function (files) {
                    if (files && files.length) {
                        $scope.photoFile.push(files);
                        //for (var i = 0; i < files.length; i++) {
                        //    var file = files[i];

                        //    participantsService.UploadPhoto(file, $scope.new_record.guid, function (data) {
                        //        $log.log('participantsService.UploadPhoto success', data);
                        //    });
                        //}
                    }
                },
                DocumentFileSelected: function (files) {
                    if (files && files.length) {
                        $scope.docFile.push(files);

                        //for (var i = 0; i < files.length; i++) {
                        //    var file = files[i];
                        //    participantsService.UploadDocument(file, $scope.new_record.guid, function (data) {
                        //        $log.log('participantsService.UploadDocument success', data);
                        //    });
                        //}
                    }
                }

            };
        }]);
})();


