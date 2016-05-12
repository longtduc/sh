/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.min.js" />

shareHolderApp.controller('shareHoldersCtrl', ['$scope', '$http', 'shareHolderFactory', function ($scope, $http, shareHolderFactory) {
    $scope.loading = true;

    $scope.getShareHolders = function () {
        ////Get server data from factory using callback
        //shareHolderFactory.getShareHolders(function (data) {
        //    $scope.shareHolders = data;      
        //    $scope.loading = false;
        //});    

        //Get server data from factory using promise
        shareHolderFactory.getShareHoldersUsingPromise().
            then(
                function (res) {
                    $scope.shareHolders = res;
                    $scope.loading = false;
                },
                function (err) {
                    alert(JSON.stringify(err));
                    $scope.loading = false;
                });
    }

    $scope.init = function () {

        $scope.shareHolders = [];

        //Used for adding/editing shareHolder
        $scope.newShareHolder = {};
        $scope.addingMode = true;

        //Used for sorting
        $scope.sortField = 'shareHolderCode';
        $scope.reverse = true;

        //Get ShareHolders
        $scope.getShareHolders();
    };

    $scope.init();



    //Add a new shareholder
    $scope.putShareHolder = function () {
        var dataSent = {
            shareHolderCode: $scope.newShareHolder.ShareHolderCode,
            name: $scope.newShareHolder.Name,
            numberOfShares: $scope.newShareHolder.NumberOfShares
        };
        //alert(JSON.stringify(dataSent));
        $http.put('/Api/ShareHolderDS', dataSent)
            .success(function (data, status, headers, config) {
                if (status === 201) { //Post is OK
                    //update the model                  
                    $scope.shareHolders.push(data);
                    //hide the form and clear data of new
                    $scope.newShareHolder = {};
                }
            })
            .error(function (data, status, headers, config) {
                alert(data.Message);
            });

    };

    //delete a shareholder
    $scope.deleteShareHolder = function (shareHolder) {
        var dataSent = { id: shareHolder.ShareHolderId };
        $http.delete('/Api/ShareHolderDS', { params: dataSent })
            .success(function (data, status, headers, config) {
                if (status === 200) { //Delete is OK
                    //update the model
                    var i = $scope.shareHolders.indexOf(shareHolder);
                    $scope.shareHolders.splice(i, 1);
                };
            })
            .error(function (data, status, header, config) {
                alert(data.Message);
            });
    };

    //switch to editing mode for updating 
    $scope.setEditingMode = function (shareHolder) {

        $scope.newShareHolder.ShareHolderId = shareHolder.ShareHolderId;
        $scope.newShareHolder.ShareHolderCode = shareHolder.ShareHolderCode,
        $scope.newShareHolder.Name = shareHolder.Name,
        $scope.newShareHolder.NumberOfShares = shareHolder.NumberOfShares

        $scope.btnLabel = "Save";
        $scope.addingMode = false;
    };

    //cancel the editing mode
    $scope.setAddingMode = function () {
        $scope.addingMode = true;
        $scope.newShareHolder = {};
    };

    //update while in editing mode
    $scope.postShareHolder = function () {
        var dataSent = {
            shareHolderCode: $scope.newShareHolder.ShareHolderCode,
            name: $scope.newShareHolder.Name,
            numberOfShares: $scope.newShareHolder.NumberOfShares,
            shareHolderId: $scope.newShareHolder.ShareHolderId,
            statusAtMeeting: $scope.newShareHolder.StatusAtMeeting
        };

        //alert(JSON.stringify(dataSent));
        $http.post('/Api/ShareHolderDS', dataSent)
            .success(function (data, status, headers, config) {
                //if (status === 200) {
                var length = $scope.shareHolders.length;
                for (var i = 0; i < length; i++) {
                    if ($scope.shareHolders[i].ShareHolderId === $scope.newShareHolder.ShareHolderId) {
                        $scope.shareHolders[i].ShareHolderCode = $scope.newShareHolder.ShareHolderCode,
                        $scope.shareHolders[i].Name = $scope.newShareHolder.Name,
                        $scope.shareHolders[i].NumberOfShares = $scope.newShareHolder.NumberOfShares
                        break;
                    }
                }

                $scope.newShareHolder = {};
                $scope.addingMode = true;

            })
            .error(function (data, status, headers, config) {
                alert(data.Message);
            });

    };

}]);


shareHolderApp.controller('shareHolderPagingCtrl', function ($scope, $http) {

    $scope.shareHolders = [];

    //Get shareHolder paging using querystring
    $scope.getPagingUsingQueryString = function () {

        var pageSize = $('#pageSize').val();
        var pageIndex = $('#pageIndex').val();
        var url = "/Api/ShareHolderDS?pageSize=" + pageSize + '&pageIndex=' + pageIndex;

        $http({ method: 'get', url: url }).success(function (data, status, headers, config) {
            //alert(JSON.stringify(data));
            $scope.shareHolders = data;
        });
    };

    //Get shareHolders paging using OData
    $scope.getPagingUsingQueryable = function () {
        var pageSize = $('#pageSize').val();
        var pageIndex = $('#pageIndex').val();
        var url = "/Api/ShareHolderDS?$top=" + pageSize + '&$skip=' + (pageIndex * pageSize);

        $http({ method: 'get', url: url }).success(function (data, status, headers, config) {
            //alert(JSON.stringify(data));
            $scope.shareHolders = data;
        });
    };

});

