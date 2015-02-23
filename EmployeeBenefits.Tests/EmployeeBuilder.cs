using EmployeeBenefits.Data;

namespace EmployeeBenefits.Tests
{
    public class EmployeeBuilder
    {
        private Employee _employee;

        public EmployeeBuilder()
        {
            _employee = new Employee();
            _employee.FirstName = "Test";
            _employee.LastName = "Employee";
        }

        public static EmployeeBuilder DefaultEmployee()
        {
            return new EmployeeBuilder();
        }

        public EmployeeBuilder WithAFirstName()
        {
            _employee.FirstName = "Albert";
            return this;
        }

        public Employee Build()
        {
            return _employee;
        }

        public EmployeeBuilder AddDependent()
        {
            _employee.Dependents.Add(new Dependent(){FirstName = "Test", LastName = "Dependent"});
            return this;
        }

        public EmployeeBuilder AddDependentThatStartsWithA()
        {
            _employee.Dependents.Add(new Dependent(){FirstName = "Al", LastName = "Dependent"});
            return this;
        }
    }
}