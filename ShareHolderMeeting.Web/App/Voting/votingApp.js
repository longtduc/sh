/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.min.js" />
/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular-route.min.js" />


var votingApp = angular.module('votingApp',
    ['ngRoute', 'shareHolderFactory', 'votingFactory', 'ngAnimate', 'ui.bootstrap']);


votingApp.config(function ($routeProvider) {
    $routeProvider.
      when('/Voting/:type', {
          templateUrl: '/App/Voting/Partials/Voting.html',
          controller: 'voteCtrl'
      }).
      when('/VotingResult/:type', {
          templateUrl: '/App/Voting/Partials/VotingResult.html',
          controller: 'voteResultCtrl'
      }).
      otherwise({
          redirectTo: '/Voting/1'
      });
});
