/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.min.js" />

userApp.controller('assignRoleCtrl', ['$scope', '$http', '$routeParams', 'userFactory',
       function ($scope, $http, $routeParams, userFactory) {

           //Get params 
           //$scope.userId = $routeParams.userId;

           $scope.user = JSON.parse($routeParams.user);

           //$scope.user = {};

           //$http({ method: 'get', url: '/User/GetUser', params: { id: $scope.userId } })
           //    .then(
           //        function (response) {
           //            //alert(JSON.stringify(response.data));
           //            $scope.user = response.data;
           //        },
           //        function (error) {

           //            alert('err');
           //        }
           //    );

           $scope.states = {
               showForm: false
           };

           $scope.showForm = function (showForm) {
               $scope.states.showForm = showForm;
           };

           //Get addable roles for a user
           $scope.rolesAddable = {};

           userFactory.getAddableRoles($scope.user.Id, function (data) {
               $scope.rolesAddable = data;
           });

           $scope.addRole = function (roleId) {

               var dataSent = { userId: $scope.user.Id, roleId: roleId };
               $http.post('/User/AddRole', dataSent).
                   then(function (response) {
                       if (response.data.Status == true) {
                           //Add to the roles belonging to user
                           var addedRole = response.data.ReturnObject
                           $scope.user.Roles.push(addedRole);

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
               var dataSent = { userId: $scope.user.Id, roleName: role.Name };
               $http.post('/User/DeleteRole', dataSent).
                   then(function (response) {
                       if (response.data.Status == true) {
                           //Update model
                           var index = $scope.user.Roles.indexOf(role);
                           if (index != -1) {
                               $scope.user.Roles.splice(index, 1);
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

