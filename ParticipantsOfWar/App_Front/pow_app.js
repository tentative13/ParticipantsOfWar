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

                $urlRouterProvider.otherwise("/Participants");

                $stateProvider.state('Participants', {
                    url: "/Participants",
                    templateUrl: "/App_Front/views/Participants_table.html",
                    controller: 'powCtrl'
                })
                .state('Participants', {
                    url: "/Participants",
                    templateUrl: "/App_Front/views/Participants_table.html",
                    controller: 'powCtrl'
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
