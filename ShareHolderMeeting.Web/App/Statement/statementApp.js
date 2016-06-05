/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.min.js" />
/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular-route.min.js" />
angular.module('statementApp', [])
    .controller('statementCtrl', function ($scope, $http) {

        $scope.statements = [];
        $scope.editedStatement = {};
        $scope.newStatement = {};

        $selectedRow = -1;
        $http({ method: 'GET', url: '/Statement/GetStatements' })
            .success(function (data) {
                $scope.statements = data;

            });


        $scope.setEditingMode = function (s) {
            $scope.selectedRow = s.Id;
            $scope.editedStatement = angular.copy(s);         
        };

        $scope.cancelEditing = function () {
            $scope.editedStatement = {};
            $scope.newStatement = {};
            $scope.selectedRow = -1;
        };

        $scope.put = function () {
            var dataSent = { Id: 0, Description: $scope.newStatement.Description };

            $http({ method: 'put', url: '/Statement/Put', params: dataSent })
                .success(
                    function (data) {
                        if (data.Status) {
                            $scope.newStatement.Id = data.Id;
                            $scope.statements.push($scope.newStatement);
                            $scope.newStatement = {};
                        }
                        else {
                            alert(data.Message);
                        }
                    })
            };

        $scope.delete = function (statement) {
            var dataSent = { id: statement.Id };
            $http({ method: 'delete', url: '/Statement/Delete', params: dataSent })
                .success(function (data) {
                    if (data.Status) {
                        var i = $scope.statements.indexOf(statement);
                        $scope.statements.splice(i, 1);
                    } else {
                        alert(data.Message);
                    }
                })
                .error(function (data) {
                    alert(data.Message);
                });

        };

        $scope.post = function () {
            var dataSent = { id: $scope.editedStatement.Id, description: $scope.editedStatement.Description };
            //console.log(dataSent);
            $http({ method: 'post', url: '/Statement/post', params: dataSent })
                .success(function (data) {
                    if (data.Status == true) {
                        var length = $scope.statements.length;
                        for (var i = 0; i < length; i++) {
                            if ($scope.statements[i].Id == $scope.editedStatement.Id) {
                                $scope.statements[i].Description = $scope.editedStatement.Description;
                                break;
                            }
                        }
                        $scope.cancelEditing();
                    } else {
                        alert(data.Message);
                    }

                })
                .error(function (data) {
                    alert(data);
                });



        };

    });

