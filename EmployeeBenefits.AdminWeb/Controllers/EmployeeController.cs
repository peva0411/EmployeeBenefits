using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeBenefits.Data;
using EmployeeBenefits.Domain;

namespace EmployeeBenefits.AdminWeb.Controllers
{
   
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // GET api/employee/5
        [Route("api/employee/{payPeriod}")]
        public PayPeriodViewModel Get(int payPeriod)
        {
            //get all employees 
            var employees = _employeeRepository.GetEmployees();

            //calculate paychecks
            var emloyeeCalulators = employees.Select(e => new EmployeeBenefitCalculator(e));

            return new PayPeriodViewModel(emloyeeCalulators, payPeriod);

        }

    }

    public class PayPeriodViewModel
    {
        public List<EmployeePaycheck> EmployeePaychecks { get; private set; }

        public PayPeriodViewModel(IEnumerable<EmployeeBenefitCalculator> calculators, int payPeriod)
        {
            EmployeePaychecks =
                calculators.Select(c =>
                        new EmployeePaycheck()
                        {
                            FullName = c._employee.FirstName + " " + c._employee.LastName,
                            Amount = c.GetPayCheck(payPeriod)
                        }).ToList();
        }
    }

    public class EmployeePaycheck
    {
        public string FullName { get; set; }
        public decimal Amount { get; set; }
    }
}
