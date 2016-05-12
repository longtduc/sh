votingByHandApp.controller('votingByHandResultCtrl', function ($scope, $http) {
    $scope.model = [];

    $http({ method: 'GET', url: '/VotingByHand/GetVotingByHandResult' })
        .success(function (data) {

            $scope.model = data;
            console.log(data);
        });

});
