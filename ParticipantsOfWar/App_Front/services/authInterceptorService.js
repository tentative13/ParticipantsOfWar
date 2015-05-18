(function () {
    var app = angular.module('pow_app');

    app.factory('authInterceptorService', [
        '$q', '$rootScope', '$log', function ($q, $rootScope, $log) {
            return {
                request: function (config) {
                    //$rootScope.loadingClass = 'loader';
                    config.headers = config.headers || {};
                    var token = sessionStorage.getItem('accessToken');
                    if (token) {
                        config.headers.Authorization = 'Bearer ' + token;
                    }
                    return config || $q.when(config);
                },
                response: function (response) {
                    //$rootScope.loadingClass = "";
                    if (response.status === 401) {
                        $log.warn(response.status, response.data);
                    }
                    else if (response.status === 500) {
                        $log.warn(response.status, response.data);
                    }
                    return response || $q.when(response);
                },
                responseError: function (rejection) {
                    //$rootScope.loadingClass = "";

                    if (rejection.status === 403 || rejection.status === 401) {
                        $log.warn(rejection.status, rejection.data);
                        $rootScope.showSimpleToast('У Вас недостаточно прав на выполение данной операции!');
                    }

                    return $q.reject(rejection);
                }
            }
        }]);



})();
