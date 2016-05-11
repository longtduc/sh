/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.min.js" />
/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular-route.min.js" />

var registrationApp = angular.module('registrationApp',
                        ['ngRoute', 'shareHolderFactory', 'commonValues', 'registrationCtrls']);

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
