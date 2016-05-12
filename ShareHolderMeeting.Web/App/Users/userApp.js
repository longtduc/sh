/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.min.js" />
/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular-route.min.js" />

var userApp = angular.module('userApp', ['ngRoute', 'userFactory'])
    .config(function ($routeProvider, $locationProvider) {
        //$locationProvider.html5Mode(true);
        $routeProvider.
          when('/', {
              templateUrl: '/App/Users/Partials/UsersList.html',
              controller: 'userCtrl'
          }).
          when('/AssignRole/:user', {
              templateUrl: '/App/Users/Partials/AssignRoles.html',
              controller: 'assignRoleCtrl'
          }).
        otherwise({
            redirectTo: '/'
        });
    });
