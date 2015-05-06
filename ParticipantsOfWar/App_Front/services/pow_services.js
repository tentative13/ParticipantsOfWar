﻿(function () {
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

    }]);
})();
