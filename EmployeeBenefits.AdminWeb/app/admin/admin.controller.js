(function() {
    'use strict';
    angular.module('app.admin').controller('Admin', Admin);

    Admin.$inject = ['paycheckService'];

    function Admin(paycheckService) {
        var vm = this;
        vm.payPeriod = 1;
        vm.payChecks = [];
        vm.getPayChecks = getPayChecks;

        activate();


        function activate() {
            getPayChecks();
        }

        function getPayChecks() {
            if (vm.payPeriod == '')
                return;

            if (vm.payPeriod > 26) {
                alert("Pay period must be 26 or lower");
                return;
            }

            paycheckService.getEmployeePaychecksForPeriod(vm.payPeriod)
               .then(function (data) {
                   vm.payChecks = data;
               }).catch(function (data) {
                   alert(data);
               });
        }

    }

})();
