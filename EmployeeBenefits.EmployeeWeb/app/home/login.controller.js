(function() {
    'use strict';
    angular.module('app.home').controller('Login', Login);

    Login.$inject = ['$state'];

    function Login($state) {

        var vm = this;
        vm.firstName ='';
        vm.lastName = '';
        vm.submit = submit;

        function submit() {
            $state.go('employee', { firstName: vm.firstName, lastName: vm.lastName });
        }
    }

})();
