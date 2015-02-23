var token = "";

lightApp.controller('HomeController', function ($scope, lightService) {
    $scope.showButton = false;

    var GetRoomInfo = function () {

        var roomInfo = lightService.getRoomInfo(token)
        .then(function (roomInfo) {
            $scope.rooms = roomInfo;
        });
    };

    var getToken = function () {
        var resToken = lightService.getToken()
        .then(function (data) {
            if (data.Success) {
                token = data.Value;
                $scope.message = "found Previous Token: " + data.Value;
                GetRoomInfo();
            }
            else {
                resToken = lightService.generateToken().then(function (data) {
                    if (data.Success) {
                        token = data.Value;
                        lightService.saveToken(token);
                        $scope.message = "new token created: " + data.Value;
                        GetRoomInfo();
                    }
                    else {

                        $scope.message = "Couldn't find token or your previous token expired.  Press sync on your device and try again";
                        $scope.showButton = true;
                    }
                });

            }
        });

    };

   

    getToken();



    $scope.tryAgain = getToken;
});
