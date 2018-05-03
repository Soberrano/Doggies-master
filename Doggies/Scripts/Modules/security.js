webApp.factory("Security", ["$rootScope", "$http", "$q", "$location", "$route", "RequestPromise", "PageLoader", "Tools", "RequestPromise", function ($rootScope, $http, $q, $location, $route, requestPromise, pageLoader, tools, requestPromise) {
    var userPromise;
    var module = {
        Login: function (login, password) {
            if ($rootScope.user != null) return;
            pageLoader.Regist(function (callback) {
                $http({
                    method: 'POST',
                    url: '/api/account/Login',
                    data: {
                        Login: login,
                        Password: password
                    }
                }).then(
                tools.ResponseUnwrapper(function (data) {
                    userPromise = null;
                    if (data != null) {
                        console.log(data);
                        $rootScope.user = data;
                    }
                }));
            });
        },
        Register: function (login, password, email) {
            if ($rootScope.user != null) return;
            return requestPromise({
                method: "POST",
                url: "/api/account/register",
                data: {
                    Password: password,
                    Login: login,
                    Email: email
                }
            });
        },
        Logout: function () {
            //console.log("Logout");
            if ($rootScope.user == null) return;
            pageLoader.Regist(function (callback) {
                $http({
                    method: 'POST',
                    url: '/api/account/Logout'
                }).then(
                tools.ResponseUnwrapper(function (data) {
                    userPromise = null;
                    $rootScope.user = null;
                    callback();
                    //location.reload(false);
                }),
                function (res) {
                    console.log(res);
                    callback();
                });
            });
        },
        // сохраняет последнее обещание загрузки
        getCurrentUser: function () {
            if (!userPromise) {
                userPromise = requestPromise({
                    method: "GET",
                    url: "/api/account/GetCurrentUser"
                });

                userPromise.then(function (user) {
                    console.log(user);
                    $rootScope.user = user;
                });
            }

            return userPromise;
        },
        checkUserIsAuthorized: function () {
            var deferred = $q.defer();

            module.getCurrentUser().then(function (user) {
                if (!user) {

                    $location.path("/login");
                    $rootScope.$on('$locationChangeSuccess', function (event, newUrl, oldUrl) {
                        deferred.resolve();
                    });
                }
                else {
                    deferred.resolve();
                }
            });

            return deferred.promise;
        },
        IsAuthorized: function () {
            return $rootScope.user ? true : false;
        }
    };
    $rootScope.UserInRoles = module.UserInRoles;

    $rootScope.IsAuthorized = module.IsAuthorized;

    $rootScope.user = undefined;

    module.getCurrentUser();

    //pageLoader.Regist(function (callback) {
    //    $http({
    //        method: "GET",
    //        url: "/api/Security/GetCurrentUser"
    //    }).then(
    //        tools.ResponseUnwrapper(function (data) {
    //            $rootScope.user = data;
    //            callback();
    //        }),
    //        function (res) {
    //            callback();
    //        });
    //});

    return module;
}]);