(function () {
    var app = angular.module('pow_app');
    app.animation('.slide-animation', ['photoSlider', function (photoSlider) {
        return {
            beforeAddClass: function (element, className, done) {
                var scope = element.scope();


                if (className == 'ng-hide') {
                    var finishPoint = element.parent().width();
                    if (photoSlider.direction !== 'right') {
                        finishPoint = -finishPoint;
                    }
                    TweenMax.to(element, 0.5, { left: finishPoint, onComplete: done });
                }
                else {
                    done();
                }
            },
            removeClass: function (element, className, done) {
                var scope = element.scope();

                if (className == 'ng-hide') {
                    element.removeClass('ng-hide');

                    var startPoint = element.parent().width();
                    if (photoSlider.direction === 'right') {
                        startPoint = -startPoint;
                    }
                    TweenMax.fromTo(element, 0.5, { left: startPoint }, { left: 0, onComplete: done });
                }
                else {
                    done();
                }
            }
        };
    }]);
}());
