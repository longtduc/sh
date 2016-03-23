/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.min.js" />
angular.module('votingGenApp', ['votingByHandFactory'])
    .controller('votingGenCtrl', function ($scope, $http, votingByHandFactory) {
    $scope.votingByHands = [];

    $scope.votingByHand = {};

    getVotinByHand();
   
    $scope.generateVotingByHands = function () {
        if (!confirm('Are you sure to generate Initial VotingByHand Data?'))
            return;
        $http({ method: 'GET', url: '/VotingByHand/GenerateVotingByHands' })
            .success(function (resp) {
                //console.log(resp);
                if (resp.Status == true)
                    getVotinByHand();
                else
                    alert('Error while generating VotingByHand data!');
            });
    };

    function getVotinByHand() {
        votingByHandFactory.getVotingByHands(function (data) {
            $scope.votingByHands = data;

        });
    }   
});


