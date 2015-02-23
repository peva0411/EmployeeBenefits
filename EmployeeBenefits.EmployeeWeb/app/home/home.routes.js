(function() {
    'use strict';
    angular.module('app.home').config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $urlRouterProvider.otherwise('/');

        $stateProvider.state('home', {
            url: '/',
            templateUrl: 'app/home/home.html',
            controller: function () { },
            controllerAs: 'vm'
        }).state('login', {
            url: '/login',
            templateUrl: 'app/home/login.html',
            controller: 'Login',
            controllerAs: 'vm'
        });
    }
})();