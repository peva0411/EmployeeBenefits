(function() {
    'use strict';

    angular.module('app.employee').config(config);

    config.$inject = ["$stateProvider"];

    function config($stateProvider) {
        $stateProvider.state('employee', {
            url: '/employee/:firstName/:lastName',
            templateUrl: 'app/employee/employee.html',
            controller: 'Employee',
            controllerAs: 'vm'
        });
    }
})();