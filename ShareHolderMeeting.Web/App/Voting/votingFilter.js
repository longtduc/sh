/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.min.js" />
    
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
