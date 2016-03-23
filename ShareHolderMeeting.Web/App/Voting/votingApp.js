/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.js" />

var votingApp = angular.module('votingApp',
    ['ngRoute', 'shareHolderFactory', 'votingFactory',
                                                'VotingCtrls']);

votingApp.filter('cardsVoted', function () {
    return function (votingCards) {
        var result = 0;
        angular.forEach(votingCards, function (card) {
            if (card.IsVoted)
                result += 1;
        });
        return result;
    };
});

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
