/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.js" />

var shareHolderApp = angular.module('shareHolderApp', ['ngRoute', 'shareHolderFactory', 'shareHoldersCtrls']);

shareHolderApp.config(function ($routeProvider) {
    $routeProvider.
      when('/', {
          templateUrl: '/App/ShareHolder/Partials/ShareHolder.html',
          controller: 'shareHoldersCtrl'
      }).     
      otherwise({
          redirectTo: '/'
      });
});


