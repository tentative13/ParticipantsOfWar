(function () {
    var app = angular.module('pow_app');

    app.factory('DateToStr', function () {
        return function (date) {
            var datestr = date.split("T")[0];
            var date_components = datestr.split("-");
            return date_components[2] + '.' + date_components[1] + '.' + date_components[0];
        }
    });



})();

