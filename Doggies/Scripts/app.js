
var webApp = angular.module("webApp", ["ngRoute", "ngMessages"]);
webApp.config(["$routeProvider", "$locationProvider",
    function ($routeProvider, $locationProvider) {
        $routeProvider.
            when('/', {
                templateUrl: 'template/mainpage',
                controller: 'RedirectCtrl'//,
                //resolve: {
                //    auth: ["Security", function (security) {
                //        return security.checkUserIsAuthorized();
                //    }],
                //    access: ["Security", function (security) {
                //        return security.checkUserInRole(['developer', 'admin']);
                //    }]
                //}
            }).
             when('/login', {
                 templateUrl: 'template/account/login',
                 controller: 'SecurityCtrl'
             }).
             when('/hall', {
                 templateUrl: 'template/app/hall',
                 controller: 'HallCtrl'
             }).
            when('/register', {
                templateUrl: 'template/account/register',
                controller: 'SecurityCtrl'
            }).
            when('/teachers', {
                templateUrl: 'template/app/Teachers',
                controller: 'TeachersCtrl'
            }).
            when('/filter', {
                templateUrl: 'template/app/filter',
                controller: 'FilterCtrl'
            }).
            when('/dog', {
                templateUrl: 'template/app/dog',
                controller: 'DogCtrl'
            }).
            when('/adddog', {
                templateUrl: 'template/app/adddog',
                controller: 'DogCtrl'
            }).
            otherwise({
                redirectTo: '/'
            });

        $locationProvider.html5Mode(true);
    }]);