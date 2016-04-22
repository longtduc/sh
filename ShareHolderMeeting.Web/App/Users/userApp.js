/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.min.js" />
/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular-route.js" />

angular.module('userApp', ['ngRoute', 'userCtrls', 'userFactory'])
    .config(function ($routeProvider, $locationProvider) {
    //$locationProvider.html5Mode(true);
    $routeProvider.
      when('/', {
          templateUrl: '/App/Users/Partials/UsersList.html'
      }).
      when('/AssignRole/:index', {
          templateUrl: '/App/Users/Partials/AssignRoles.html',
          controller: 'assignRoleCtrl'
      }).    
    otherwise({
        redirectTo: '/'
    });
});
