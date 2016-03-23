/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.min.js" />
angular.module('votingCardApp', [])
    .controller('votingCardCtrl', function ($scope, $http) {
        $scope.statusLine = 'You should generate All Voting Cards only if there is a change of BOD/BOS candidates after registering shareholders at meeting';
        $scope.generateVotingCards = function () {
            if (!confirm('Are you sure to generate All Voting Cards?'))
                return;

            $http({ method: 'POST', url: '/VotingCard/GenerateVotingCards' })
                .then(
                    function (response) {

                        if (response.data.Status)
                            $scope.statusLine = response.data.Message
                        else {
                            $scope.statusLine = response.data.Message
                            alert('Something wrong when generating All Voting Card!')
                        }

                    },
                    function (error) {
                        $scope.statusLine = 'Something wrong happened!';
                        alert('Something wrong when generating All Voting Card!')
                    }
                    );
        };


    });


