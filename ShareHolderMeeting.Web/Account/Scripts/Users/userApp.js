/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.min.js" />
/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular-route.min.js" />
/// <reference path="../../Views/User/_usersList.html" />

var userApp = angular.module('userApp', ['ngRoute', 'userFactory'])
    .config(function ($routeProvider, $locationProvider) {
        //$locationProvider.html5Mode(true);
        $routeProvider.
          when('/', {
              templateUrl: '/Account/Scripts/Users/Partials/_usersList.html',
              controller: 'userCtrl'
          }).
          when('/AssignRole/:user', {
              templateUrl: '/Account/Scripts/Users/Partials/_assignRole.html',
              controller: 'assignRoleCtrl'
          }).
        otherwise({
            redirectTo: '/'
        });
    });