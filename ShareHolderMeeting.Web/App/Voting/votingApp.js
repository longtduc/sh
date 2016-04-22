﻿/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.js" />

var votingApp = angular.module('votingApp',
    ['ngRoute', 'shareHolderFactory', 'votingFactory',
                                                'VotingCtrls']);


votingApp.config(function ($routeProvider) {
    $routeProvider.      
      when('/Voting/:type', {
          templateUrl: '/App/Voting/Partials/Voting.html',
          controller: 'VoteCtrl'
      }).
      when('/VotingResult/:type', {
          templateUrl: '/App/Voting/Partials/VotingResult.html',
          controller: 'VoteResultCtrl'
      }).    
      otherwise({
          redirectTo: '/'
      });
});
