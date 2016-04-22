/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.min.js" />

angular.module('shareHolderFactory', [])
    .factory('shareHolderFactory', ['$http', '$q', function ($http, $q) {
        return {
            getShareHoldersUsingPromise: function () {
                var deferred = $q.defer();
                $http({ method: 'GET', url: '/Api/ShareHolderDS', cache: true }).
                    success(function (res) {
                        deferred.resolve(res);
                    }).
                    error(function (err) {
                        deferred.reject(err);
                    });
                return deferred.promise;
            },
            getShareHolders: function (callback) {
                var url = '/Api/ShareHolderDS';
                $http({ method: 'GET', url: url, contentType: 'json', cache: false }).
                    success(function (data) {
                        callback(data);
                    }).
                    error(function (data, status, headers, config) {                     
                        alert('Error when loading ShareHolders (/Api/ShareHolderDS)');
                    });

            }
        };
    }]);