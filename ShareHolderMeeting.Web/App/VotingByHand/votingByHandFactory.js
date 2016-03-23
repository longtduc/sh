/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.min.js" />

angular.module('votingByHandFactory', []).factory('votingByHandFactory', function ($http) {
    return {
        getVotingByHands: function (callback) {
            $http({ method: 'GET', url: '/VotingByHand/GetVotingByHands' }).
                success(function (data) {
                    callback(data);
                }).
                error(function (data, status, headers, config) {
                    alert('Error when loading data!');
                });
        },
        getVotingByHand: function (id, callback) {
            var dataSent = { id: id };
            $http({ method: 'GET', url: '/VotingByHand/GetVotingByHand', params: dataSent }).
                success(function (data) {
                    callback(data);
                })
        },
        getVotingOptions: function () {
            var options = [
                { id: 0, text: 'Yes' },
                { id: 1, text: 'No' },
                { id: 2, text: 'Other' }];
            return options;
        }
    }

});


