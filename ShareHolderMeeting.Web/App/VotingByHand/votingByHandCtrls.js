/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.min.js" />
var votingByHandCtrl = angular.module('votingByHandCtrl', ['ngAnimate', 'ui.bootstrap'])


votingByHandCtrl.controller('votingByHandCtrl', function ($scope, $http, $uibModal, $log, votingByHandFactory) {
    $scope.votingByHands = [];

    $scope.votingByHand = {};

    $scope.reverse = false;

    getVotinByHands();


    $scope.updateVoting = function () {
        console.log($scope.votingByHand);
        $http.post('/VotingByHand/UpdateVoingByHand', $scope.votingByHand).
               success(function (data) {
                   for (var i = 0; i < $scope.votingByHands.length; i++) {
                       if ($scope.votingByHands[i].Id === $scope.votingByHand.Id) {
                           $scope.votingByHands[i].VotingByHandLines = $scope.votingByHand.VotingByHandLines;
                           break;
                       }
                   }
               }).
               error(function () {
                   alert('Error happend when updating!');
               });

    };
  
    function getVotinByHands() {
        votingByHandFactory.getVotingByHands(function (data) {
            $scope.votingByHands = data;

        });

    }

    $scope.open = function (votingByHand) {

        var modalInstance = $uibModal.open({
            animation: $scope.animationsEnabled,
            templateUrl: '/App/VotingByHand/Partials/VotingByHandModal.html',
            controller: 'ModalInstanceCtrl',
            size: 'md',
            resolve: {
                votingByHand: votingByHand
            }
        });

        modalInstance.result
            .then(
                function (votingCard) {
                    ////Get result from modal
                    $scope.votingByHand = votingCard;
                    ////update 
                    $scope.updateVoting();
                },
                function () {
                    $log.info('Modal dismissed at: ' + new Date());
                });
    };

    $scope.votingOptionText = function (value) {
        if (value == 0) {
            return 'Yes';
        } else if (value == 1) {
            return 'No';
        } else if (value == 2)
            return 'Other';
        return 'Invalid';
    }
});

votingByHandCtrl.controller('ModalInstanceCtrl', function ($scope, $http, $uibModalInstance, votingByHandFactory, votingByHand) {

    $scope.card = {};
    //console.log(votingCard);

    //Get voting card from server
    getVotingCard();

    //When click OK in dialog box
    $scope.ok = function () {

        //update VotingOption from 
        for (var i = 0; i < $scope.card.VotingByHandLines.length; i++) {

            var newValue = $scope.card.VotingByHandLines[i].selectedOption.id;
            $scope.card.VotingByHandLines[i].VotingOption = newValue;

        }
        console.log($scope.card);
        $uibModalInstance.close($scope.card);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    //Get VotingByHand
    function getVotingCard() {
        var dataSent = { id: votingByHand.Id };
        $http({ method: 'GET', url: '/VotingByHand/GetVotingByHand', params: dataSent }).
            success(function (data) {
                $scope.card = data;
                //alert(JSON.stringify(data));
            }).
        error(function () {
            alert('Error when loading VotingByHand');
        });;
    };

    $scope.votingOptions = votingByHandFactory.getVotingOptions();   

});

votingByHandCtrl.controller('votingByHandResultCtrl', function ($scope, $http)
{
    $scope.model = [];

    $http({ method: 'GET', url: '/VotingByHand/GetVotingByHandResult' })
        .success(function (data) {

            $scope.model = data;
            console.log(data);
        });

});
