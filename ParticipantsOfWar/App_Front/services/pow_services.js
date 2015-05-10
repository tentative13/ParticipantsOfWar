(function () {
    var app = angular.module('pow_app');

    app.service('ParticipantsService', ['$http', '$rootScope', '$log', 'Upload', function ($http, $rootScope, $log, Upload) {

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
        //move to $resource
        this.updateParticipant = function (id, item, callback) {
            $http({
                method: 'PUT',
                url: url + '/' + id,
                data: item
            }).
            success(function (data, status, headers, config) {
                $log.info('updateParticipant', status);
                callback();
            }).
            error(function (data, status, headers, config) {
                $log.error('updateParticipant', status, data);
            });
        };
        //move to $resource
        this.createParticipant = function (item, callback) {
            $http({
                method: 'POST',
                url: url,
                data: item
            }).
            success(function (data, status, headers, config) {
                $log.info('createParticipant', status);
                callback(data);
            }).
            error(function (data, status, headers, config) {
                $log.error('createParticipant', status, data);
            });
        };


        this.getDocument = function (documentid) {
            window.location.href = 'api/Documents/GetDocument/' + documentid;
        };

        this.getParticipants = function (filter, number) {
            return $rootScope.powHub.server.getParticipants(filter, number);
        };
        this.getAllParticipants = function (filter) {
            return $rootScope.powHub.server.getAllParticipants(filter);
        };
        this.getTotalFilteredParticipants = function (filter) {
            return $rootScope.powHub.server.getTotalFilteredParticipants(filter);
        };
        this.sendGuidsCache = function (guids) {
            $rootScope.powHub.server.guidscache(guids);
        };

        this.UploadDocument = function (file, guid, callback) {
            this.UploadFile('api/Documents/UploadDocument', file, guid, callback);
        };

        this.UploadPhoto = function (file, guid, callback) {
            this.UploadFile('api/Documents/UploadPhoto', file, guid, callback);
        };

        this.UploadFile = function (url, file, guid, callback) {
            Upload.upload({
                url: url,
                file: file,
                method: 'POST',
                fields: {
                    ParticipantGuid: guid
                }
            }).progress(function (evt) {
                $log.log('progress: ' + parseInt(100.0 * evt.loaded / evt.total) + '% file :' + evt.config.file.name);
            }).success(function (data, status, headers, config) {
                $log.log('file ' + config.file.name + ' is uploaded successfully. Response: ' + data);
                callback(data);
            }).error(function (data, status, headers, config) {
                $log.error('Upload error', status, data, headers, config);
            });

        };

        this.TimeZoneFixer = function (item) {

            if (typeof item.birthday != "undefined" && (item.birthday instanceof Date)) {
                var offset = new Date().getTimezoneOffset();
                offset = (offset / 60) * (-1);
                item.birthday.setHours(item.birthday.getHours() + offset);
            }
        };

    }]);
})();
