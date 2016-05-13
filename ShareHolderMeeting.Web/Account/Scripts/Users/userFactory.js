/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.min.js" />

angular.module('userFactory', [])
        .factory('userFactory', function ($http) {
            return {

                getUsers: function (successCbFunc) {
                    $http({ method: 'GET', url: '/User/GetUsers' }).
                        success(function (data, status, headers, config) {
                            successCbFunc(data);
                        }).
                        error(function () {
                            alert('Error when loading all users (/User/GetUsers)');
                        });
                },

                getAddableRoles: function (userId, successCbFunc) {

                    $http({ method: 'GET', url: '/User/GetAddableRoles', params: { userId: userId } }).
                        success(function (data, status, headers, config) {
                            successCbFunc(data);
                        }).
                        error(function () {
                            alert('Error when loading addable roles (/User/GetAddableRoles)');
                        });
                }
            };
        });