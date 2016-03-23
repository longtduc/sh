/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.js" />

var shareHolderApp = angular.module('shareHolderApp', ['ngRoute', 'shareHolderFactory', 'shareHoldersCtrls']);

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

shareHolderApp.config(function ($routeProvider) {
    $routeProvider.
      when('/', {
          templateUrl: '/App/ShareHolder/Partials/ShareHolder.html',
          controller: 'shareHoldersCtrl'
      }).     
      otherwise({
          redirectTo: '/'
      });
});


