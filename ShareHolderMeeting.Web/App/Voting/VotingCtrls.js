/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.min.js" />


var VotingCtrls = angular.module('VotingCtrls', ['ngAnimate', 'ui.bootstrap']);

VotingCtrls.controller('VoteCtrl', function ($scope, $http, $uibModal, $log, $routeParams, votingFactory) {

   
    $scope.reverse = false;
    $scope.showName = false;

    //Set title for view
    $scope.title = function () {
        if ($routeParams.type == 1)
            return 'BODs';
        return 'BOSs';
    };

    //Get votingCards from factory
    $scope.votingCards = [];
    votingFactory.getVotingCards($routeParams.type, function (data) {
        $scope.votingCards = data;        
    });

    //Voting Card
    $scope.votingCard = {};

    //Open modal
    $scope.open = function (cardArg) {

        var modalInstance = $uibModal.open({
            animation: $scope.animationsEnabled,
            templateUrl: '/App/Voting/Partials/VotingModal.html',
            controller: 'ModalInstanceCtrl',
            size: 'md',
            resolve: {
                votingCard: cardArg
            }
        });

        modalInstance.result.
            then(
            function (votingCard) {
                //Get result from modal
                $scope.votingCard = votingCard;
                //update 
                $scope.updateVoting();
            },
            function () {
                $log.info('Modal dismissed at: ' + new Date());
            });
    };

    //update voting card
    $scope.updateVoting = function () {
        $http.post('/VotingCard/UpdateVotingCard', $scope.votingCard).
            success(function (data) {              
                for (var i = 0; i < $scope.votingCards.length; i++) {
                    if ($scope.votingCards[i].Id === $scope.votingCard.Id) {
                        $scope.votingCards[i].IsVoted = true;
                        $scope.votingCards[i].IsInvalid = $scope.votingCard.IsInvalid;
                        $scope.votingCards[i].AmtAlreadyVoted = data.ReturnedObj.AmtAlreadyVoted;
                        break;
                    }
                }
            }).
            error(function () {
                alert('Error happend when updating VotingCard (/VotingCard/UpdateVotingCard)');
            });
    };
    //Revert the voting
    $scope.revert = function (id) {
        $http.post('/VotingCard/RevertVotingCard', { id: id }).
            success(function (data) {
                for (var i = 0; i < $scope.votingCards.length; i++) {
                    if ($scope.votingCards[i].Id === id) {
                        $scope.votingCards[i].IsInvalid = false;
                        $scope.votingCards[i].IsVoted = false;
                        $scope.votingCards[i].AmtAlreadyVoted = 0;
                        break;
                    }
                }
            }).
            error(function () {
                alert('Error happend when reverting VotingCard (/VotingCard/RevertVotingCard)');
            });
    };

});

// Please note that $modalInstance represents a modal window (instance) dependency.
// It is not the same as the $uibModal service used above.
VotingCtrls.controller('ModalInstanceCtrl', function ($scope, $http, $uibModalInstance, votingCard) {

    $scope.card = {};

    $scope.ShareHolderCode = votingCard.ShareHolderCode;
    //Get voting card from server
    getVotingCard();

    $scope.ok = function () {
        if (!validateInputData())
            return;

        $uibModalInstance.close($scope.card);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    //Vote for all candidates equally
    $scope.voteAll = function () {
        var total = $scope.card.NumberOfShares * $scope.card.NumberOfCandidates;
        var numberOfCandidates = $scope.card.VotingCardLines.length;
        var each = total / numberOfCandidates;
        for (var i = 0; i < $scope.card.VotingCardLines.length; i++) {
            $scope.card.VotingCardLines[i].VotingAmt = each;
        }       
    };

    //Vote the candidate the rest
    $scope.voteTheRest = function (id) {
      

        var total = $scope.card.NumberOfShares * $scope.card.NumberOfCandidates;
        var votedAmt = 0;
        //Calulate voted amt
        for (var i = 0; i < $scope.card.VotingCardLines.length; i++) {
            if ($scope.card.VotingCardLines[i].Id != id)
                votedAmt += $scope.card.VotingCardLines[i].VotingAmt;            
        }
        //Allocate the rest 
        if (total > votedAmt)
        {
            for (var i = 0; i < $scope.card.VotingCardLines.length; i++) {
                if ($scope.card.VotingCardLines[i].Id == id)
                    $scope.card.VotingCardLines[i].VotingAmt = total - votedAmt;
            }
        }

    };

    $scope.voteForAll = function () {
        var total = $scope.card.NumberOfShares * $scope.card.NumberOfCandidates;
        var numberOfCandidates = $scope.card.VotingCardLines.length;
        var each = total / numberOfCandidates;
        for (var i = 0; i < $scope.card.VotingCardLines.length; i++) {
            $scope.card.VotingCardLines[i].VotingAmt = each;
        }
    };

    //Get voting card from server
    function getVotingCard() {
        var dataSent = { id: votingCard.Id };
        $http({ method: 'GET', url: '/VotingCard/GetVotingCard', params: dataSent }).
            success(function (data) {
                $scope.card = data;
                //alert(JSON.stringify(data));
            }).
        error(function () {
            alert('Error when loading voting card (/VotingCard/GetVotingCard)' + JSON.stringify(dataSent));
        });;
    };
    //Validate input data
    function validateInputData() {
        //If it is invalid
        if ($scope.card.IsInvalid)
            return true;

        var maxVotingAmt = $scope.card.NumberOfShares * $scope.card.NumberOfCandidates;
        var totalVotingAmt = 0;

        for (var i = 0; i < $scope.card.VotingCardLines.length; i++) {
            var votingAmt = parseInt($scope.card.VotingCardLines[i].VotingAmt)
            if (votingAmt < 0) {
                alert('Voting Amount has to be greater than 0');
                return false;
            }
            else
                totalVotingAmt += votingAmt;
        }
        if (totalVotingAmt > maxVotingAmt) {
            alert('Total Voting Amount (' + totalVotingAmt + ') has to be <= ' + maxVotingAmt);
            return false;
        }
        return true;
    };
});

VotingCtrls.controller('VoteResultCtrl', function ($scope, $http, $routeParams) {
    $scope.votingResult = {};
    $scope.reverse = false;
    //Set title for view
    $scope.title = function () {
        if ($routeParams.type ==1)
            return 'BOD';
        return 'BOS';
    };
    var dataSent = { votingType: $routeParams.type };
    $http({ method: 'GET', url: '/VotingCard/GetVotingResult',params: dataSent }).
        success(function (data, status, headers, config) {
            $scope.votingResult = data;
        }).
        error(function () {
            alert('Error when loading Voting Result (/VotingCard/GetVotingResult)');
        });

});
