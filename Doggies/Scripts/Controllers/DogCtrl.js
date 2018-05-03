webApp.controller("DogCtrl", ["$rootScope", "$scope", "RequestPromise", function ($rootScope, $scope, requestPromise) {
    $scope.getDogs = function () {
        return requestPromise(
            {
                method: "POST",
                url: "/api/dog/getDogs"
            }
        );
    }
    $scope.Dogs;
    $scope.getDogs().then(function (data) {
        $scope.Dogs = data;
    })

    $scope.addDog = function () {

        $scope.DogName;
        $scope.VpkosOrLicenceNumber
        $scope.DateOfBirth;
        $scope.UserId;
        $scope.MotherId;
        $scope.FatherId;
        return requestPromise(
            {
                method: "POST",
                url: "/api/dog/addDogs"
            }
        );

    }
}]);