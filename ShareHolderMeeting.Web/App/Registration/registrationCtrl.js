/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.js" />

registrationApp.controller('registrationCtrl', function ($scope, $http, $route, shareHolderFactory, statusOptions) {

    //Used for sorting
    $scope.reverse = false;

    $scope.loading = true;

    //Get server data from factory   
    $scope.shareHolders = [];

    shareHolderFactory.getShareHolders(function (data) {
        $scope.shareHolders = data;
        $scope.loading = false;
        calculateStatistic();
    });
    
    //get statusOptions from module.value
    $scope.statusOptions = statusOptions;
    $scope.statusSelected = 0;

    $scope.showName = false;

    //Transform enum values into text 

    $scope.statusText = function (status) {

        //Using filter method of js array 
        var result = $scope.statusOptions.filter(function (value, index, array) {
            return (value.id === status);
        });
        if (result != undefined) {
            return result[0].name;
        }
        else
            return 'Invalid Status';
    };

    //Choose shareHolder to register
    $scope.registeringMode = false;
    $scope.toBeRegisted = {};

    $scope.setRegisteringMode = function (shareHolder) {
        $scope.toBeRegisted = angular.copy(shareHolder);       
        $scope.statusSelected = $scope.statusOptions[shareHolder.StatusAtMeeting];
        $scope.registeringMode = true;
    };

    //Update ShareHolder Status at Meeting
    $scope.updateStatus = function () {

        var newStatus = $scope.statusSelected.id;
        var dataSent = {
            shareHolderId: $scope.toBeRegisted.ShareHolderId,
            newStatus: newStatus
        };

        //alert(JSON.stringify(dataSent));
        $http.put('/Registration/updateStatus', dataSent)
            .success(function (data) {
                if (data.status === true) {
                    var length = $scope.shareHolders.length;
                    for (var i = 0; i < length; i++) {
                        if ($scope.shareHolders[i].ShareHolderId === $scope.toBeRegisted.ShareHolderId) {
                            $scope.shareHolders[i].StatusAtMeeting = newStatus;
                            break;
                        }
                    }

                    $scope.toBeRegisted = {};
                    $scope.registeringMode = false;
                    calculateStatistic();
                }
                else
                    alert(data.message);
            })

    };

    //Cancel updating status
    $scope.cancelUpdate = function () {
        $scope.toBeRegisted = {};
        $scope.registeringMode = false;
    };

    //Calculate statistic data for opening the meeting
    $scope.statisticData = {};

    //Reload data
    $scope.reloadData = function () {
        $route.reload();
    };

    function calculateStatistic() {
        var totalShareHolders = 0,
            totalNumberOfShares = 0,
            currentShareHolders = 0,
            currentNumberOfShares = 0;
        for (var i = 0; i < $scope.shareHolders.length; i++) {
            var sh = $scope.shareHolders[i];
            totalShareHolders += 1;
            totalNumberOfShares += sh.NumberOfShares;
            if (sh.StatusAtMeeting != 0) {
                currentShareHolders += 1;
                currentNumberOfShares += sh.NumberOfShares;
            }
        }

        $scope.statisticData = {
            totalShareHolders: totalShareHolders,
            currentShareHolders: currentShareHolders,
            shareHolderRate: Math.round(currentShareHolders / totalShareHolders * 10000) / 100,
            totalNumberOfShares: totalNumberOfShares,
            currentNumberOfShares: currentNumberOfShares,
            numberOfSharesRate: Math.round(currentNumberOfShares / totalNumberOfShares * 10000) / 100
        };

    };

});
