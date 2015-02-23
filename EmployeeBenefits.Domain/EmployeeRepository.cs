using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using EmployeeBenefits.Data;

namespace EmployeeBenefits.Domain
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly EmployeeBenefitContext _context;

        public EmployeeRepository()
        {
            _context = new EmployeeBenefitContext();
        }

        public Employee GetEmployeeByName(string firstName, string lastName)
        {
            return _context.Employees.FirstOrDefault(e => e.FirstName == firstName && e.LastName == lastName);
        }

        public void Add(Employee employee)
        {
            _context.Employees.Add(employee);
            
        }

        public List<Employee> GetEmployees()
        {
            return _context.Employees.ToList();
        } 

        public void Update(Employee employee, IEnumerable<int> dependentsToDelete)
        {

            _context.Employees.Add(employee);
            _context.Entry(employee).State = EntityState.Modified;

            foreach (var dependent in employee.Dependents)
            {
                if (dependent.DependentId > 0)
                {
                    _context.Entry(dependent).State = EntityState.Modified;
                }
            }

            var toRemove = _context.Dependents.Where(d => dependentsToDelete.Contains(d.DependentId));

            foreach (var dependent in toRemove)
            {
                _context.Entry(dependent).State = EntityState.Deleted;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        
    }

    public interface IEmployeeRepository
    {
        List<Employee> GetEmployees();
        Employee GetEmployeeByName(string firstName, string lastName);
        void Add(Employee employee);
        void Save();
        void Update(Employee employee, IEnumerable<int> dependentsToDelete);

    }
}
