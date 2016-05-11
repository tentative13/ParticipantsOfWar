(function () {

   var app = angular.module('pow_app',
       [
            'ui.router',
            'ngMaterial',
            'ui.bootstrap',
            'ui.date',
            'ngFileUpload',
            'ngAnimate',
            'ngMessages',
            'ngMdIcons'
       ])
       .config(
       [
            '$stateProvider',
            '$urlRouterProvider',
            '$compileProvider',
            '$httpProvider',
            '$mdThemingProvider',
            function ($stateProvider, $urlRouterProvider, $compileProvider, $httpProvider, $mdThemingProvider) {

                $mdThemingProvider.theme('default').primaryPalette('brown');

                $httpProvider.interceptors.push('authInterceptorService');

                $urlRouterProvider.otherwise("/participants/");

                $stateProvider.state('participants', {
                    url: "/participants/",
                    templateUrl: "/App_Front/views/Participants_table.html",
                    controller: 'powCtrl'
                })
                .state('participants.details', {
                    url: "details/",
                    templateUrl: "/App_Front/views/Participants_details.html",
                    controller: 'powDetailsCtrl'
                })
                .state('participants.create', {
                    url: "create/",
                    templateUrl: "/App_Front/views/Participants_details.html",
                    controller: 'powDetailsCtrl'
                });

                $compileProvider.debugInfoEnabled(false);
                $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|unsafe|ftp|mailto|file):/);
                $compileProvider.aHrefSanitizationWhitelist(/^\s*(|unsafe|http|blob|):/);
            }
       ])
       .run(['$log', '$rootScope', 'participantsVM', '$mdToast', '$mdDialog', 'ParticipantsService', '$state',
           function ($log, $rootScope, participantsVM, $mdToast, $mdDialog, participantsService, $state) {

            function FirstPageDialogController($scope, $mdDialog, photoSlider) {
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

                   setInterval(function () {
                       $rootScope.$apply(photoSlider.prevSlide());
                   }, 5000);
                   $scope.oncancelClick = function () {
                       $scope.photoSlider.clearSlides();
                       $mdDialog.cancel();
                   };
               }

            //FirstPageDialogController.$inject = ['$scope', '$mdDialog', 'photoSlider'];
            //$mdDialog.show({
            //    templateUrl: '/App_Front/views/first_page.html',
            //    clickOutsideToClose: true,
            //    escapeToClose: true,
            //    controller: FirstPageDialogController
            //});

            $rootScope.loadingClass = '';
            $rootScope.loader_text = '';

            $rootScope.authentication = {
                isAuthorized: false,
                userName: ''
            };

            var token = sessionStorage.getItem('accessToken');
            if (token) {
                $rootScope.authentication.isAuthorized = true;
            }
            var userName = sessionStorage.getItem('userName');
            if (userName) {
                $rootScope.authentication.userName = userName;
            }

            $rootScope.powHub = $.connection.powHub;
            $rootScope.powHub.client.ErrorSendMessage = function () {
                $log.error('Ошибка сервиса SignalR. Обратитесь к администратору!');
            }

            $.connection.hub.error(function (error) { $log.warn('SignalR error: ', error) });
            $.connection.hub.logging = true;
            $.connection.hub.start()
                .done(function () {
                    $rootScope.signalrConnectionId = $.connection.hub.id;
                    $log.info('Now connected, connection ID=' + $.connection.hub.id);
                    participantsVM.GetTotalFilteredParticipants(Object.create(null));
                    participantsVM.GetPageParticipants(Object.create(null), 10);
                })
                .fail(function () { $log.warn('Could not Connect to Hub!'); });

            $.connection.hub.stateChanged(function (change) {
                if (change.newState === $.signalR.connectionState.reconnecting) {
                    $log.log('SignalR Re-connecting');
                }
                else if (change.newState === $.signalR.connectionState.connected) {
                    $log.info('SignalR The server is online');
                }
            });
            $.connection.hub.reconnected(function () {
                $rootScope.signalrConnectionId = $.connection.hub.id;
                $log.info('Now Reconnected, connection ID=' + $.connection.hub.id);
                if (participantsVM.Participants.length > 0) {
                    participantsVM.SendGuidsCache();
                }
            });
            $.connection.hub.disconnected(function () {

            if ($.connection.hub.lastError){ $log.error("Disconnected. Reason: ", $.connection.hub.lastError.message); }

            setTimeout(function () {
                    $.connection.hub.start()
                        .done(function () {
                            $rootScope.signalrConnectionId = $.connection.hub.id;
                            $log.info('Now connected, connection ID=' + $.connection.hub.id);
                            if (participantsVM.Participants.length > 0) {
                                participantsVM.SendGuidsCache();
                            }
                            else {
                                participantsVM.GetTotalFilteredParticipants(Object.create(null));
                                participantsVM.GetPageParticipants(Object.create(null), 10);
                            }
                        })
                        .fail(function () { $log.warn('Could not Connect to Hub!'); });

                }, 20000); // Restart connection after 20 seconds.
            });

            $rootScope.showSimpleToast = function (text) {
                $mdToast.show(
                  $mdToast.simple()
                    .content(text)
                    .position('top right')
                    .hideDelay(3000)
                );
            };

            $rootScope.dateOptions = {
                changeYear: true,
                changeMonth: true,
                yearRange: '1900:-0',
                dateFormat: 'dd.mm.yy',
                firstDay: 1,
                dayNames: ["Воскресенье", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота"],
                dayNamesMin: ["Вс", "Пн", "Вт", "Ср", "Чт", "Пт", "Сб"],
                monthNamesShort: ["Янв", "Фев", "Март", "Апр", "Май", "Июнь", "Июль", "Авг", "Сеп", "Окт", "Нояб", "Дек"]
            };

            $rootScope.onLoginClickedEvent = function () {
                function DialogController($scope, $mdDialog, participantsService) {
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
                };
                DialogController.$inject = ['$scope', '$mdDialog', 'participantsService'];
                $mdDialog.show({
                    templateUrl: '/App_Front/views/dialog1_tmpl.html',
                    targetEvent: event,
                    locals: { participantsService: participantsService },
                    controller: DialogController
                });
            };

            $rootScope.$on('$stateChangeSuccess', function (event, toState) {

                if (toState.name === 'participants.details')
                {
                    if (typeof $rootScope.pow_details === "undefined") {
                        $state.go('participants');
                    }
                }
                if (toState.name === 'participants.create') {
                    if (typeof $rootScope.createMode === "undefined") {
                        $state.go('participants');
                    }
                    if (typeof $rootScope.createMode === false) {
                        $state.go('participants');
                    }
                }
            });

            $rootScope.CaruselStyle = function () {
                var width = $('#photoGrid').width();
                if (typeof width === "undefined") width = 250;
                return {
                    'height': width + 'px',
                    'width': width +'px',
                    'overflow': 'hidden',
                    'position': 'relative',
                    'margin-top': '20px'
                };
            };
        }
       ]);
})();
