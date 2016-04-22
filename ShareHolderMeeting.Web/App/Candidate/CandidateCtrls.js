/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.min.js" />
var CandidateCtrls = angular.module('CandidateCtrls', []);

CandidateCtrls.controller('CandidateCtrl', ['$scope', '$http', function ($scope, $http) {

    $scope.candidates = [];
    $scope.newCandidate = {};
    $scope.editCandidate = {};
    $scope.selectedRow = -1;
    $scope.title = ($scope.type === 1) ? 'BOD Candidates' : 'BOS Candidates';

    //Get all candidates from server
    var url = '/api/CandidateDS';
    $http({ method: 'GET', url: url, contentType: 'json', params: { type: $scope.type }, cache: false }).
        success(function (data, status, headers, config) {
            $scope.candidates = data;
        }).
        error(function (data, status, headers, config) {
            alert('error while loading canidates (/api/Candidate)');
        });

    $scope.postCandidate = function () {
        var dataSent = { name: $scope.newCandidate.Name, candidateType: $scope.type };
        $http.post('/api/CandidateDS', dataSent).
            success(function (data, status, headers, config) {
                //alert(JSON.stringify(data));
                if (status = 201) {
                    //update the model
                    $scope.candidates.push(data);
                    //Reset init values
                    $scope.newCandidate = {};
                }
            }).
            error(function (data, status, headers, config) {
                alert(data.Message);
            });
    };



    $scope.deleteCandidate = function (candidate) {
        var dataSent = { id: candidate.Id };
        $http.delete('/api/CandidateDS', { params: dataSent }).success(function (data, status, header, config) {
            if (status === 200) { //Delete is OK
                //update the model
                var i = $scope.candidates.indexOf(candidate);
                $scope.candidates.splice(i, 1);
            };
        }).error(function (data, status, headers, configs) {
            alert('error while deleting a candidate with id of ' + candidate.Id + ' (/api/Candidate)');
        });
    };

   
    $scope.setEditingMode = function (candidate) {
        $scope.selectedRow = candidate.Id;
        //DO NOT USE THIS CODE
        //$scope.editCandidate = candidate;
        $scope.editCandidate.Name = candidate.Name;
        $scope.editCandidate.Id = candidate.Id;
        $scope.newCandidate = {};
    };


    $scope.cancelEditing = function () {
        $scope.editCandidate = {};
        $scope.selectedRow = -1;
    };

    $scope.putCandidate = function () {
        var dataSent = { id: $scope.editCandidate.Id, name: $scope.editCandidate.Name, candidateType: $scope.type };
        $http.put('/Api/CandidateDS', dataSent)
         .success(function (data, status, headers, config) {
             //if (status == 200) {
             var length = $scope.candidates.length;
             for (var i = 0; i < length; i++) {
                 if ($scope.candidates[i].Id === $scope.editCandidate.Id) {
                     $scope.candidates[i].Name = $scope.editCandidate.Name;
                     break;
                 }
             }
             $scope.cancelEditing();
         })
         .error(function (data, status, headers, config) {
             alert(data.Message);
         });

    };


}]);