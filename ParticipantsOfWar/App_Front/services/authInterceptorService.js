(function () {
    var app = angular.module('pow_app');

    app.factory('authInterceptorService', [
        '$q', '$rootScope', '$log', function ($q, $rootScope, $log) {
            return {
                request: function (config) {
                    
                    if (config.url != '/Token') {
                        $rootScope.loadingClass = 'block-loader';
                        if (!$rootScope.loadingCount) $rootScope.loadingCount = 0;
                        $rootScope.loadingCount++;
                    }

                    config.headers = config.headers || {};
                    var token = sessionStorage.getItem('accessToken');
                    if (token) {
                        config.headers.Authorization = 'Bearer ' + token;
                    }
                    return config || $q.when(config);
                },
                response: function (response) {
                    if ($rootScope.loadingCount)$rootScope.loadingCount--;
                    if ($rootScope.loadingCount == 0) {
                        $rootScope.loadingClass = "";
                        $rootScope.loader_text = '';
                    }


                    if (response.status === 401) {
                        $log.warn(response.status, response.data);
                    }
                    else if (response.status === 500) {
                        $log.warn(response.status, response.data);
                    }
                    return response || $q.when(response);
                },
                responseError: function (rejection) {
                    if ($rootScope.loadingCount) $rootScope.loadingCount--;
                    if ($rootScope.loadingCount == 0) {
                        $rootScope.loadingClass = "";
                        $rootScope.loader_text = '';
                    }

                    if (rejection.status === 403 || rejection.status === 401) {
                        $log.warn(rejection.status, rejection.data);
                        $rootScope.showSimpleToast('У Вас недостаточно прав на выполение данной операции!');
                    }

                    return $q.reject(rejection);
                }
            }
        }]);



})();
