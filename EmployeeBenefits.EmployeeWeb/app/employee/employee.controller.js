(function() {
    'use strict';

    angular.module('app.employee').controller('Employee', Employee);

    Employee.$inject = ['$scope', 'employeeService', '$state', '$stateParams'];

    function Employee($scope, employeeService, $state, $stateParams) {
        var vm = this;
        vm.test = 'hello World',
            vm.employee = {
                firstName: '',
                lastName: '',
                dependents: []
            };
        vm.updating = false;
        vm.cost = 1000;
        vm.removedDependents = [];
        vm.getPreviewCost = getPreviewCost;
        vm.save = save;
        vm.addDependent = addDependent;
        vm.removeDependent = removeDependent;


        activate();

        function activate() {
            if ($stateParams.firstName && $stateParams.lastName) {
                employeeService.getEmployee($stateParams)
                    .then(function(data) {
                        vm.employee = data;
                        vm.updating = true;
                        getPreviewCost();
                }).catch(function(message) {
                    alert('Could not find employee');
                    vm.updating = false;
                });
            }
        }

        function getPreviewCost() {
            employeeService.getCost(vm.employee)
                .then(function(cost) {
                vm.cost = cost;
            }).catch(function(message) {
                    alert(message);
            });
        }

        function save() {

            if (vm.updating) {
                employeeService.putEmployee(vm.employee, vm.removedDependents)
                    .then(function (data) {
                        alert("Updated employee");//redirect to home
                    $state.go('home');
                }).catch(function (message) {
                        alert(message);
                    });

            } else {
                employeeService.postEmployee(vm.employee)
                    .then(function(data) {
                        alert("Added employee");//redirect to home
                        $state.go('home');
                }).catch(function(message) {
                    alert(message);
                });    
            }

        }

        function addDependent() {
            vm.employee.dependents.push({ firstName: '', lastName: '' });
            getPreviewCost();
        }

        function removeDependent(dependent) {
            if (dependent.dependentId !== 0) {
                 vm.removedDependents.push(dependent.dependentId);
            }

            vm.employee.dependents = vm.employee.dependents.filter(function(item) {
                return item.$$hashKey !== dependent.$$hashKey;
            });

            getPreviewCost();
        }
    };
})();