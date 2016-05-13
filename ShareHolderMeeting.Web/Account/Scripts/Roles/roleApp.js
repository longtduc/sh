/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.min.js" />
/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular-route.min.js" />
var roleApp = angular.module('roleApp', ['ngRoute', 'roleFactory']);

roleApp.config(function ($routeProvider) {
    $routeProvider.
      when('/', {
          templateUrl: '/Account/Scripts/Roles/Partials/_roleList.html'
      }).
      otherwise({
          redirectTo: '/'
      });
});