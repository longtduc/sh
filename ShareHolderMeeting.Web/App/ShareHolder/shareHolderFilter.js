/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.min.js" />


//calculate total of shares
shareHolderApp.filter('sharesTotal', function () {
    return function (shareHolders) {
        var total = 0;
        angular.forEach(shareHolders, function (shareHolder) {
            total += parseInt(shareHolder.NumberOfShares);
        });
        return total;
    }
});