(function () {

   var app = angular.module('pow_app',
       [
            'ui.router',
            'ngMaterial',
            'ui.bootstrap',
            'ui.date',
            'ngFileUpload'
       ])
       .config(
       [
            '$stateProvider',
            '$urlRouterProvider',
            '$compileProvider',
            function ($stateProvider, $urlRouterProvider, $compileProvider) {

                $urlRouterProvider.otherwise("/participants");

                $stateProvider.state('participants', {
                    url: "/participants",
                    templateUrl: "/App_Front/views/Participants_table.html",
                    controller: 'powCtrl'
                })
                .state('participants.details', {
                    url: "/details",
                    templateUrl: "/App_Front/views/Participants_details.html",
                    controller: 'powDetailsCtrl'
                })
                .state('participants.create', {
                    url: "/create",
                    templateUrl: "/App_Front/views/Participants_details.html",
                    controller: 'powDetailsCtrl'
                });

                $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|unsafe|ftp|mailto|file):/);
                $compileProvider.aHrefSanitizationWhitelist(/^\s*(|unsafe|http|blob|):/);
            }
       ])
       .run(['$log', '$rootScope', 'participantsVM', function ($log, $rootScope, participantsVM) {

            $log.log('starting angularjs app...');


            $rootScope.powHub = $.connection.powHub;
            $rootScope.powHub.client.ErrorSendMessage = function () {
                $log.error('Ошибка сервиса SignalR. Обратитесь к администратору!');
            }

            $.connection.hub.error(function (error) { $log.warn('SignalR error: ', error) });

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

                if ($.connection.hub.lastError)
                { $log.error("Disconnected. Reason: ", $.connection.hub.lastError.message); }

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
        }
       ]);
})();
