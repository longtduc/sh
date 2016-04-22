/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.min.js" />
/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular-route.js" />
var CandidateApp = angular.module('CandidateApp', ['ngRoute', 'CandidateCtrls']);
CandidateApp.config(function ($routeProvider) {
    $routeProvider.
     when('/', {
         templateUrl: '/App/Candidate/Partials/Candidate.html',
         controller: 'CandidateCtrl'
     }).
     otherwise({
         redirectTo: '/'
     });

});
