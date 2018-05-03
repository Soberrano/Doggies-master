webApp.factory("Teacher", [
    "$rootScope",
    "HttpRequest",
    "RequestPromise",
    "Tools",
function (
    $rootScope,
    httpRequest,
    requestPromise,
    tools
) {

    var service = {

        getTeachers: function () {
            return requestPromise(
                {
                    method: "POST",
                    url: "/api/teacher/getTeachers"
                }
            );
        }
    };

    return service;
}]);