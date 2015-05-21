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

}());
