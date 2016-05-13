/// <reference path="D:\00-OnlineInsurance\OnlineInsurance\OnlineInsurance.Web.Bhv\Scripts/angular.min.js" />


userApp.controller('userCtrl', ['$scope', '$http', 'userFactory',
    function ($scope, $http, userFactory) {
        //For sorting

        $scope.loading = true;

        $scope.reverse = false;

        $scope.model = {};
        $scope.model.users = [];

        //Get all users from factory
        userFactory.getUsers(function (data) {
            $scope.model.users = data;
            $scope.loading = false;
        });

        $scope.deleteUser = function (user) {
            var dataSent = { userId: user.Id };
            $http({ method: 'post', url: '/User/DeleteUser', params: dataSent }).
                then(function (response) {
                    if (response.data.Status) {
                        var index = $scope.model.users.indexOf(user);
                        if (index != -1) {
                            $scope.model.users.splice(index, 1);
                        }
                    }
                    else {
                        alert(response.data.message);
                    }
                });
        }

    }]);

