(function() {
    'use strict';

    angular.module('app.services').factory('employeeService', employeeService);

    employeeService.$inject = ['$http', '$q'];

    function employeeService($http, $q) {

        var service = {
            getEmployee: getEmployee,
            getCost: getCost,
            postEmployee: postEmployee,
            putEmployee: putEmployee
        };

        return service;

        function getCost(employee) {
            var defered = $q.defer();

            $http.post("/api/employee/costPreview", employee)
                .success(function(data) {
                    defered.resolve(data);
                }).catch(function(reason) {
                    defered.reject(reason.message);
                });
            
            return defered.promise;
        }

        function getEmployee(name) {
            var defered = $q.defer();

            $http.get("/api/employee/" + name.firstName + "/" + name.lastName)
                .success(function(data) {
                    defered.resolve(data);
                }).catch(function(reason) {
                defered.reject(reason.message);
                });

            return defered.promise;
        }

        function postEmployee(employee) {
            var defered = $q.defer();

            $http.post("/api/employee", employee)
                .success(function(data) {
                    defered.resolve(data);
                }).catch(function(reason) {
                    defered.reject(reason.statusText);
                });

            return defered.promise;
        }

        function putEmployee(employee, idsToDelete) {
            var defered = $q.defer();

            $http.put("/api/employee", { 'employee': employee, 'dependentIdsToDelete': idsToDelete })
                .success(function(data) {
                    defered.resolve(data);
                }).catch(function(reason) {
                    defered.reject(reason.statusText);
                });

            return defered.promise;
        }

    };

})();