/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.min.js" />

angular.module('roleFactory', [])
    .factory('roleFactory', ['$http', '$q', function ($http, $q) {
        return {
            //3-Using promise technique
            getRolesPromise: function () {
                var deferred = $q.defer();
                $http({ method: 'GET', url: '/Role/GetRoles' }).
                         success(function (data, status, headers, config) {
                             deferred.resolve(data);
                         }).
                         error(function () {
                             deferred.reject(status);
                         });
                return deferred.promise;
            },
            // 2-Using callback technique
            getRoles: function (successcb) {
                $http({ method: 'GET', url: '/Role/GetRoles' }).
                         success(function (data, status, headers, config) {
                             successcb(data);
                         }).
                         error(function () {

                         });
            }
        };
    }]);

