/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.js" />

var registrationApp = angular.module('registrationApp',
                        ['ngRoute', 'shareHolderFactory', 'registrationCtrls']);

registrationApp.config(function ($routeProvider) {
    $routeProvider.
      when('/', {
          templateUrl: '/App/Registration/Partials/Registration.html',
          controller: 'registrationCtrl'
      }).
      otherwise({
          redirectTo: '/'
      });
});
