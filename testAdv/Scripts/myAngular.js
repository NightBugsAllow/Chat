var mainApp = angular.module("mainApp", []);

mainApp.controller("mainController", function ($http, $scope, $interval) {
    var ctrl = this;

    $interval(function() {
        $http({
            method: 'Post',
            url: '/Home/GetItems/'
        }).then(function(data, status, headers, config) {
            if (data.data == null)
                window.location.href = "~/";
            if ($scope.items != data.data) {
                $scope.items = data.data;
            }
        });
    }, 1000, 0);
    
    $scope.sendMessage = function (sender) {
        $http.post(
            '/Home/SendMessage/',
            sender
        ).then(function (data, status, headers, config) {
            if (data.data == null)
                window.location.href = "~/";
            $scope.sender.Text = "";
        });
    }

    $scope.keyUp = function(event, sender) {
        if (event.keyCode == 13)
            $scope.sendMessage(sender);
    }

    ctrl.$inject = [];
});