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

                //   $urlRouterProvider.otherwise("/realizationAIP/titles");

                //$stateProvider.state('message', {
                //    url: "/message",
                //    templateUrl: "/App_Front/view/message.html",
                //    controller: 'messageCtrl'
                //});

                $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|unsafe|ftp|mailto|file):/);
                $compileProvider.aHrefSanitizationWhitelist(/^\s*(|unsafe|http|blob|):/);
            }
        ])
        .run(['$log', function ($log) {

            $log.log('starting angularjs app...');
            }
        ]);
})();
