/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.min.js" />
/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular-route.min.js" />


var votingByHandApp = angular.module('votingByHandApp', ['ngRoute', 'votingByHandCtrl', 'votingByHandFactory']);
votingByHandApp.config(function ($routeProvider) {
    $routeProvider.
     when('/',
     {
         templateUrl: '/App/VotingByHand/Partials/VotingByHand.html',
         controller: 'votingByHandCtrl'
     }).
     when('/Result',
     {
         templateUrl: '/App/VotingByHand/Partials/VotingByHandResult.html',
         controller: 'votingByHandResultCtrl'

     }).
     otherwise({
         redirectTo: '/'
     }
     );

});



