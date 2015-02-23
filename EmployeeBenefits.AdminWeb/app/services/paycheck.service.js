(function() {
    'use strict';

    angular.module('app.services').factory('paycheckService', payCheckService);

    payCheckService.$inject = ['$http', '$q'];

    function payCheckService($http, $q) {
        var servcie = {
            getEmployeePaychecksForPeriod: getEmployeePaychecksForPeriod
        };

        return servcie;

        function getEmployeePaychecksForPeriod(period) {
            var deferred = $q.defer();

            $http.get('/api/employee/' + period)
                .success(function(data) {
                    deferred.resolve(data.employeePaychecks);
                })
                .catch(function(message) {
                    deferred.reject(message.statusText);
                });

            return deferred.promise;
        };

    }

})();