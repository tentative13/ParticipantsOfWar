(function () {

   var app = angular.module('pow_app',
       [
            'ui.router',
            'ngMaterial'
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
                });

                $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|unsafe|ftp|mailto|file):/);
                $compileProvider.aHrefSanitizationWhitelist(/^\s*(|unsafe|http|blob|):/);
            }
       ])
       .run(['$log', function ($log) {

            $log.log('starting angularjs app...');

        }
       ]);
})();
