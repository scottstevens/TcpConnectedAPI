var lightApp = angular.module('lightApp', ['ui.bootstrap']);
var lightApi = 'http://localhost:8080/api/';

lightApp.service('lightService', function ($http) {
    this.getToken = function () {
        return $http.get(lightApi + 'token').then(
              function (response) {
                  return response.data;
              });
    };
    this.saveToken = function (newToken) {
        return $http.post(lightApi + 'token', { value: newToken }).then(
              function (response) {
                  return response.data;
              });
    };

    this.generateToken = function (newToken) {
        return $http.put(lightApi + 'token/GenerateToken/').then(
              function (response) {
                  return response.data;
              });
    };

    this.getRoomInfo = function (newToken) {
        return $http.get(lightApi + 'Room/?token=' + newToken).then(
              function (response) {
                  return response.data;
              });
    };

});