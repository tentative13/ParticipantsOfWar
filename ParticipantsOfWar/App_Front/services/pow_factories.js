(function () {
    var app = angular.module('pow_app');

    app.factory('DateToStr', function () {
        return function (date) {
            var datestr = date.split("T")[0];
            var date_components = datestr.split("-");
            return date_components[2] + '.' + date_components[1] + '.' + date_components[0];
        }
    });

    app.factory('photoSlider', ['$rootScope', function ($rootScope) {
        var self = this;
        self.slides = [];
        self.direction = 'left';
        self.currentIndex = 0;

        self.clearSlides = function () {
            self.slides = [];
            self.direction = 'left';
            self.currentIndex = 0;
        }

        self.addSlides = function (photos) {
            angular.forEach(photos, function (item) {
                self.slides.push({
                    image: 'api/Documents/GetPhoto/' + item.photoId,
                    description: item.description
                });
            });
        };
                
        self.setCurrentSlideIndex = function (index) {
            self.direction = (index > self.currentIndex) ? 'left' : 'right';
            self.currentIndex = index;
        };

        self.isCurrentSlideIndex = function (index) {
            return self.currentIndex === index;
        };

        self.prevSlide = function () {
            self.direction = 'left';
            self.currentIndex = (self.currentIndex < self.slides.length - 1) ? ++self.currentIndex : 0;
        };

        self.nextSlide = function () {
            self.direction = 'right';
            self.currentIndex = (self.currentIndex > 0) ? --self.currentIndex : self.slides.length - 1;
        };

        return self;
    }]);

})();

