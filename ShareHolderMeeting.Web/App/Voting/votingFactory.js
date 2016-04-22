/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.min.js" />

var votingFactories = angular.module('votingFactory', []);

votingFactories.factory('votingFactory', function ($http) {
    return {
        getVotingCards: function (type, callback) {
            var dataSent = { votingType: type };
            $http({ method: 'GET', url: '/VotingCard/getVotingCards', cache: false, params: dataSent }).
                success(function (data) {
                    callback(data);
                }).
                error(function (data, status, headers, config) {
                    alert('Error when loading ShareHoldersAtMeeting (/VotingCard/getVotingCards)');
                });
        }
    }
});

