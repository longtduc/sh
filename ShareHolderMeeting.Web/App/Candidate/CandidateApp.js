/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.min.js" />
/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular-route.min.js" />
var candidateApp = angular.module('candidateApp', ['ngRoute']);
candidateApp.config(function ($routeProvider) {
    $routeProvider.
     when('/', {
         templateUrl: '/App/Candidate/Partials/Candidate.html',
         controller: 'candidateCtrl'
     }).
     otherwise({
         redirectTo: '/'
     });

});
