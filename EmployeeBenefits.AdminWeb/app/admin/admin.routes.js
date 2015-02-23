(function() {
    'use strict';
    angular.module('app.admin').config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise('/');

        $stateProvider.state('admin', {
            url: '/',
            templateUrl: 'app/admin/admin.html',
            controller: 'Admin',
            controllerAs: 'vm'
        });
    }

})();
