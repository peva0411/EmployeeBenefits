using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeBenefits.Data
{
    public class Employee
    {
        public Employee()
        {
            Dependents = new Collection<Dependent>();
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public decimal PayCheckAmount { get { return 2000; }}

        public virtual Collection<Dependent> Dependents { get; set; }

    }
}
