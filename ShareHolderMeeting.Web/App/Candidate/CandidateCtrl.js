/// <reference path="D:\00-OnlineInsurance\ShareHolderMeeting\ShareHolderMeeting.Web\Scripts/angular.min.js" />

candidateApp.controller('candidateCtrl', ['$scope', '$http', function ($scope, $http) {

    $scope.candidates = [];
    $scope.newCandidate = {};
    $scope.editCandidate = {};
    $scope.selectedRow = -1;
    $scope.title = ($scope.type === 1) ? 'BOD Candidates' : 'BOS Candidates';

    $scope.loading = true;

    //Get all candidates from server
    var url = '/api/CandidateDS';

    $http({ method: 'get', url: url, contentType: 'json', params: { type: $scope.type }, cache: false }).
        success(function (data, status, headers, config) {
            $scope.candidates = data;
            $scope.loading = false;
        }).
        error(function (data, status, headers, config) {
            alert('error while loading canidates (/api/Candidate)');
        });

    $scope.update = function () {
        var dataSent = { id: $scope.editCandidate.Id, name: $scope.editCandidate.Name, candidateType: $scope.type };
        $http.put('/api/CandidateDS', dataSent).
            success(function (data, status, headers, config) {
                //if (status == 200) {
                var length = $scope.candidates.length;
                for (var i = 0; i < length; i++) {
                    if ($scope.candidates[i].Id === $scope.editCandidate.Id) {
                        $scope.candidates[i].Name = $scope.editCandidate.Name;
                        $scope.cancelEditing();
                        break;
                    }
                }
            }).
            error(function (data, status, headers, config) {
                alert(data.Message);
            });
    };



    $scope.delete = function (candidate) {
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
        $scope.editCandidate =angular.copy(candidate);       
        $scope.newCandidate = {};
    };


    $scope.cancelEditing = function () {
        $scope.editCandidate = {};
        $scope.selectedRow = -1;
    };

    $scope.create = function () {
        var dataSent = { name: $scope.newCandidate.Name, candidateType: $scope.type };
        $http.post('/Api/CandidateDS', dataSent)
            .success(function (data, status, headers, config) {
                //alert(JSON.stringify(data));
                if (status = 201) {
                    //update the model
                    $scope.candidates.push(data);
                    //Reset init values
                    $scope.newCandidate = {};
                }
             
             $scope.cancelEditing();
         })
         .error(function (data, status, headers, config) {
             alert(data.Message);
         });

    };


}]);