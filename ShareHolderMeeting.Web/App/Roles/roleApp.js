﻿/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.min.js" />
/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular-route.js" />
var roleApp = angular.module('roleApp', ['ngRoute', 'roleCtrls', 'roleFactory']);

roleApp.config(function ($routeProvider) {
    $routeProvider.
      when('/', {
          templateUrl: '/App/Roles/Partials/RoleList.html'          
      }).
      otherwise({
          redirectTo: '/'
      });
});