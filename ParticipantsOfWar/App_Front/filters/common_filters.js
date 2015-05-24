(function () {
    var app = angular.module('pow_app');

    app.filter('shortTextToLines', function () {
        return function (items, number) {
            var arr = items.split("\n");
            var shortstr = '';
            if (arr.length < number) {
                shortstr = items;
            }
            else {
                for (var i = 0; i < number; i++) {
                    shortstr = shortstr + arr[i] + '\n';
                }
            }

            if (shortstr.length > 1000) shortstr = shortstr.substring(0,1000);

            return shortstr;
        }
    });

    app.filter('shortPhotoDesc', ['$rootScope', function ($rootScope) {
        return function (item) {
            var width = $('#photoGrid').width();
            var result = '';
            if (width) {
                width = width / 10;
                while (item.length > 0) {
                    result += item.substring(0, width) + '\n';
                    item = item.substring(width);
                }

            }
            else {
                result = item;
            }
            return result;
        }
    }]);

}());
