using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using EmployeeBenefits.Data;
using EmployeeBenefits.Domain;

namespace EmployeeBenefits.EmployeeWeb.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository; //new EmployeeRepository(new EmployeeBenefitContext());
        }


        [Route("api/employee/costPreview")]
        [HttpPost]
        public IHttpActionResult GetCostPreview(Employee employee)
        {
            var employeeBenefit = new EmployeeBenefitCalculator(employee);

            var result = employeeBenefit.CalculateEmployeeBenefitCost() + employeeBenefit.CalculateDepedentBenefitCost();

            return Ok(result);
        }

        [Route("api/employee/{firstName}/{lastName}")]
        public IHttpActionResult Get(string firstName, string lastName)
        {
            var employee = _employeeRepository.GetEmployeeByName(firstName, lastName);

            if (employee == null) return NotFound();

            return Ok(employee);
        }

        [Route("api/employee/")]
        [HttpPost]
        public IHttpActionResult Post(Employee employee)
        {
            _employeeRepository.Add(employee);
            _employeeRepository.Save();
            return Ok();
        }

        [Route("api/employee/")]
        [HttpPut]
        public IHttpActionResult Put(EmployeeUpdateViewModel employeeViewModel)
        {
            _employeeRepository.Update(employeeViewModel.Employee, employeeViewModel.DependentIdsToDelete);
            _employeeRepository.Save();
            return Ok();
        }


    }

    public class EmployeeUpdateViewModel
    {
        public Employee Employee { get; set; }
        public IEnumerable<int> DependentIdsToDelete { get; set; }
    }
}
