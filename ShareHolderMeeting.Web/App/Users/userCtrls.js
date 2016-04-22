/// <reference path="D:\00-OnlineInsurance\OnlineInsurance\OnlineInsurance.Web.Bhv\Scripts/angular.min.js" />

var userCtrls = angular.module('userCtrls', []);

userCtrls.controller('userAppCtrl', ['$scope', '$http', 'userFactory',
    function ($scope, $http, userFactory) {
        //For sorting

        $scope.loading = true;

        $scope.reverse = false;

        $scope.model = {};

        //Get all users from factory
        userFactory.getUsers(function (data) {
            $scope.model.users = data;
            $scope.loading = false;
        });

        $scope.deleteUser = function (user) {
            var dataSent = { userId: user.Id };
            $http({ method: 'POST', url: '/User/DeleteUser', params: dataSent }).
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


userCtrls.controller('assignRoleCtrl',
                ['$scope', '$http', '$routeParams', 'userFactory',
        function ($scope, $http, $routeParams, userFactory) {

            //Get params          
            var index = parseInt($routeParams.index);

            $scope.model.user = $scope.model.users[index];
            $scope.userId = $scope.model.user.Id;

            $scope.states = {
                showForm: false
            };

            $scope.showForm = function (showForm) {
                $scope.states.showForm = showForm;
            };

            //Get addable roles for a user
            $scope.rolesAddable = {};

            userFactory.getAddableRoles($scope.userId, function (data) {
                $scope.rolesAddable = data;
            });

            $scope.addRole = function (roleId) {

                var dataSent = { userId: $scope.userId, roleId: roleId };
                $http.post('/User/AddRole', dataSent).
                    then(function (response) {
                        if (response.data.Status == true) {
                            //Add to the roles belonging to user
                            var addedRole = response.data.ReturnObject
                            $scope.model.user.Roles.push(addedRole);

                            //remove the added role from $scope.model.rolesAddable;
                            var roleToRemove = $scope.rolesAddable.filter(function (role) {
                                return role.Id == roleId;
                            });
                            var index = $scope.rolesAddable.indexOf(roleToRemove[0]);
                            if (index != -1)
                                $scope.rolesAddable.splice(index, 1);
                        }
                    });
                $scope.showForm(false);
            };

            $scope.deleteRole = function (role) {
                var dataSent = { userId: $scope.userId, roleName: role.Name };
                $http.post('/User/DeleteRole', dataSent).
                    then(function (response) {
                        if (response.data.Status == true) {
                            //Update model
                            var index = $scope.model.user.Roles.indexOf(role);
                            if (index != -1) {
                                $scope.model.user.Roles.splice(index, 1);
                            }
                            $scope.rolesAddable.push(role);
                        }
                        else {
                            //Show error
                            alert(data.message);
                        }
                    })
            };

        }]);

