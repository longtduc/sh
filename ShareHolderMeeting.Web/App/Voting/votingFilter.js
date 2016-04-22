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
