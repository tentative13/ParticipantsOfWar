(function () {
    var app = angular.module('pow_app');
    app.directive('autofocus', ['$timeout', function ($timeout) {
        function link($scope, $element, $attrs) {
            var dom = $element[0];

            if ($attrs.autofocus) {
                $scope.$watch($attrs.autofocus, focusIf);
            } else {
                focusIf(true);
            }

            function focusIf(condition) {
                if (condition) {
                    $timeout(function () {
                        dom.focus();
                    }, $scope.$eval($attrs.autofocusDelay) || 0);
                }
            }
        }
        return {
            restrict: 'A',
            link: link
        };
    }]);


    app.directive('adjuster', ["$window", function ($window) {
        return {
            restrict: 'EA',
            link: function postLink(scope, element, attrs) {
                scope.onResizeFunction = function () {
                    var width = $('#photoGrid').width();
                    if (typeof width === "undefined") width = 250;
                    
                    $('#mainCarusel').width(width);
                    $('#mainCarusel').height(width);
                    

                };
                angular.element($window).bind('resize', function () {
                    scope.onResizeFunction();
                    scope.$apply();
                });

            }
        }
    }]);

    app.directive('adjustertext', ["$window", function ($window) {
        return {
            restrict: 'EA',
            link: function postLink(scope, element, attrs) {
                scope.onResizeFunction = function () {
                    var width = $('#photoGrid').width();
                    if (typeof width === "undefined") width = 250;

                    $('#mainCarusel').width(width);
                    $('#mainCarusel').height(width);


                };
                angular.element($window).bind('resize', function () {
                    scope.onResizeFunction();
                    scope.$apply();
                });

            }
        }
    }]);


})();

