var CandidateApp = angular.module('CandidateApp', ['ngRoute', 'CandidateCtrls']);
CandidateApp.config(function ($routeProvider) {
    $routeProvider.
     when('/', {
         templateUrl: '/App/Candidate/Partials/Candidate.html',
         controller: 'CandidateCtrl'
     }).
     otherwise({
         redirectTo: '/'
     });

});
