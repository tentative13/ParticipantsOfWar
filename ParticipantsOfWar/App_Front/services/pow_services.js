(function () {
    var app = angular.module('pow_app');

    app.service('ParticipantsService', ['$http', '$rootScope', '$log', function ($http, $rootScope, $log) {

        var url = 'api/Participants';

        this.getTypes = function (callback) {
            $http({
                method: 'GET',
                url: url + '/GetTypes'
            }).
            success(function (data, status, headers, config) {
                callback(data);
            }).
            error(function (data, status, headers, config) {
                $log.error('GetTypes', status, data);
            });
        };

        this.getAllParticipants = function (callback) {
            $http({
                method: 'GET',
                url: url + '/All'
            }).
            success(function (data, status, headers, config) {
                callback(data);
            }).
            error(function (data, status, headers, config) {
                $log.error('GetAllParticipants', status, data);
            });
        };

        this.getDocument = function (documentid) {
            window.location.href = 'api/Documents/GetDocument/' + documentid;
        };
    }]);
})();
