(function () {
    var app = angular.module('pow_app');

    app.service('ParticipantsService', ['$http', '$rootScope', '$log', 'Upload', function ($http, $rootScope, $log, Upload) {

        var url = 'api/Participants';

        this.LogIn = function (login, password) {
            return $http({
                method: 'POST',
                url: '/Token',
                data: "userName=" + login + "&password=" + password + "&grant_type=password"
            }).success(function (data, status) {
                $log.info('LogIn', status);
                sessionStorage.setItem('accessToken', data.access_token);
                sessionStorage.setItem('userName', data.userName);
                $rootScope.authentication.isAuthorized = true;
                $rootScope.authentication.userName = data.userName;
            }).error(function (data, status) {
                $log.error('LogIn', status, data);
                $rootScope.showSimpleToast('Ошибка авторизации!');
                $rootScope.authentication.isAuthorized = false;
            });
        };
        this.getTypes = function (callback) {
            $rootScope.loader_text = 'Получаем данные...';
            $http({
                method: 'GET',
                url: url + '/GetTypes'
            }).
            success(function (data, status, headers, config) {
                callback(data);
            }).
            error(function (data, status, headers, config) {
                $log.error('GetTypes', status, data);
                $rootScope.showSimpleToast('Ошибка получения данных!');
            });
        };
        this.updateParticipant = function (id, item, callback) {
            $rootScope.loader_text = 'Обновляем запись...';
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
                $rootScope.showSimpleToast('Ошибка получения данных!');
            });
        };
        this.createParticipant = function (item, callback) {
            $rootScope.loader_text = 'Сохраняем запись...';
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
                $rootScope.showSimpleToast('Ошибка получения данных!');
            });
        };
        this.getDocument = function (documentid) {
            window.location.href = 'api/Documents/GetDocument/' + documentid;
        };
        this.downloadPhoto = function (photoId) {
            window.location.href = 'api/Documents/DownloadPhoto/' + photoId;
        };

        this.getParticipants = function (filter, number) {
            return $rootScope.powHub.server.getParticipants(filter, number);
        };
        this.getAllParticipants = function (filter) {
            return $rootScope.powHub.server.getParticipants(filter, -1);
        };
        this.getTotalFilteredParticipants = function (filter) {
            return $rootScope.powHub.server.getTotalFilteredParticipants(filter);
        };
        this.sendGuidsCache = function (guids) {
            $rootScope.powHub.server.guidscache(guids);
        };

        this.UploadDocument = function (file, guid, callback) {
            $rootScope.loader_text = 'Загружаем документы...';
            this.UploadFile('api/Documents/UploadDocument', file, guid, callback);
        };
        this.UploadPhoto = function (file, guid, callback) {
            $rootScope.loader_text = 'Загружаем фотографии...';
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
                $rootScope.showSimpleToast('Ошибка получения данных!');
            });

        };
        this.TimeZoneFixer = function (item) {
            if (typeof item.birthday != "undefined" && (item.birthday instanceof Date)) {
                var offset = new Date().getTimezoneOffset();
                offset = (offset / 60) * (-1);
                item.birthday.setHours(item.birthday.getHours() + offset);
            }
        };
        this.deleteDocument = function (id, callback) {
            $rootScope.loader_text = 'Удаляем документы...';
            return $http({
                url: 'api/Documents/DeleteDocument/' + id,
                method: 'delete'
            })
            .success(function (data, status, headers, config) {
                $log.info('deleteDocument', status);
                callback(id,data);
            })
            .error(function (data, status) {
                $log.error('deleteDocument', status, data);
                $rootScope.showSimpleToast('Ошибка удаления данных!');
            });
        };
        this.deletePhoto = function (id, callback) {
            $rootScope.loader_text = 'Удаляем фотографии...';
            return $http({
                url: 'api/Documents/DeletePhoto/' + id,
                method: 'delete'
            })
            .success(function (data, status, headers, config) {
                $log.info('deletePhoto', status);
                callback(id, data);
            })
            .error(function (data, status) {
                $log.error('deletePhoto', status, data);
                $rootScope.showSimpleToast('Ошибка удаления данных!');
            });
        };

    }]);
})();
