using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeBenefits.Data
{
    public class EmployeeBenefitContext : DbContext
    {
        public EmployeeBenefitContext():base("DefaultConnection")
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Dependent> Dependents { get; set; }
    }
}
