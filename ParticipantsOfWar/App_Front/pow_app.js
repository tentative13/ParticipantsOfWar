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
                    url: "/details/",
                    templateUrl: "/App_Front/views/Participants_details.html",
                    controller: 'powDetailsCtrl'
                })
                .state('participants.create', {
                    url: "/create/",
                    templateUrl: "/App_Front/views/Participants_details.html",
                    controller: 'powDetailsCtrl'
                });

                $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|unsafe|ftp|mailto|file):/);
                $compileProvider.aHrefSanitizationWhitelist(/^\s*(|unsafe|http|blob|):/);
            }
       ])
       .run(['$log', '$rootScope', 'participantsVM', '$mdToast', '$mdDialog', 'ParticipantsService', '$state',
           function ($log, $rootScope, participantsVM, $mdToast, $mdDialog, participantsService, $state) {

            $log.log('starting angularjs app...');

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
            });
        }
       ]);
})();
