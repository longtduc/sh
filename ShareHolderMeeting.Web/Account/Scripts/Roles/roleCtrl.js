/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.min.js" />


roleApp.controller('roleCtrl',
    ['$scope', '$http', 'roleFactory', function ($scope, $http, roleFactory) {

        $scope.model = {};

        $scope.states = {
            showRoleForm: false
        };

        $scope.loading = true;

        //used for sorting Name

        $scope.reverse = false
        
        // 2-Calling services with Callback technique
        roleFactory.getRoles(function (roles) {
            $scope.model.roles = roles;
            $scope.loading = false;
        });

        //// 3-Calling services with Promise technique     
        //$scope.promise = roleFactory.getRolesPromise();
        //$scope.promise.then(function (promise) {
        //    $scope.model.roles = promise;
        //});

        //Add a new role
        $scope.createRole = function (newRole) {
            var dataSent = { roleName: newRole.Name };
            //alert(JSON.stringify(dataSent));
            $http.post('/Role/Create', dataSent)
                .then(function (response) {
                    if (response.data.Status == true) {
                        //update the model                  
                        $scope.model.roles.push(response.data.ReturnObject);
                        //hide the form and clear data of new
                        $scope.states.showRoleForm = false;
                        $scope.new = {};
                    }
                    else
                        alert(response.Message);
                })
        }

        //delete a role
        $scope.deleteRole = function (role) {
            $http.post('/Role/Delete', { roleId: role.Id })
                .then(function (response) {
                    if (response.data.Status == true) {
                        //Update model                    
                        var index = $scope.model.roles.indexOf(role);
                        if (index != -1) {
                            $scope.model.roles.splice(index, 1);
                        }
                    }
                    else {
                        //Show error
                        alert(response.Message);
                    }
                })

        }

    }]);