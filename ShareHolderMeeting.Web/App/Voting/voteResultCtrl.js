votingApp.controller('voteResultCtrl', function ($scope, $http, $routeParams) {
    $scope.votingResult = {};
    $scope.reverse = false;
    //Set title for view
    $scope.title = function () {
        if ($routeParams.type == 1)
            return 'HĐQT'; //return 'BOD';
        return 'BKS';
    };
    var dataSent = { votingType: $routeParams.type };
    $http({ method: 'GET', url: '/VotingCard/GetVotingResult', params: dataSent }).
        success(function (data, status, headers, config) {
            $scope.votingResult = data;
        }).
        error(function () {
            alert('Error when loading Voting Result (/VotingCard/GetVotingResult)');
        });

});