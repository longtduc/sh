var roleApp = angular.module('roleApp', ['ngRoute', 'roleCtrls', 'roleFactory']);

roleApp.config(function ($routeProvider) {
    $routeProvider.
      when('/', {
          templateUrl: '/App/Roles/Partials/RoleList.html'          
      }).
      otherwise({
          redirectTo: '/'
      });
});