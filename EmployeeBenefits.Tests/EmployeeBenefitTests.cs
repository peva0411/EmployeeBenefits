using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeBenefits.Domain;
using NUnit.Framework;

namespace EmployeeBenefits.Tests
{
    [TestFixture]
    public class EmployeeBenefitTests
    {

        private const decimal EmployeeCost = 1000m;
        private const decimal DependentCost = 500m;
        private const decimal AdjustmentRate = 0.90m;
        private const decimal EmployeeSalary = 2000m;
        private const decimal PayCheckCount = 26m;


        [Test]
        public void CalculateEmployeeBenefitCost_WhenNoDependents_Returns1000()
        {
            var employee = EmployeeBuilder
                .DefaultEmployee()
                .Build();

            var employeeBenefit = new EmployeeBenefitCalculator(employee);

            var result = employeeBenefit.CalculateEmployeeBenefitCost();

            Assert.That(result, Is.EqualTo(EmployeeCost));
        }

        [Test]
        public void CalculateEmployeeBenefitCost_WhenNameStartsWithA_ReturnsAdjustedCost()
        {
            var employee = EmployeeBuilder.DefaultEmployee().WithAFirstName().Build();

            var employeeBenefit = new EmployeeBenefitCalculator(employee);

            var result = employeeBenefit.CalculateEmployeeBenefitCost();

            var expected = EmployeeCost*AdjustmentRate;

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateDependentEmployeeBenefitCost_WhenNoDependents_ReturnsZero()
        {
            var employee = EmployeeBuilder.DefaultEmployee().Build();

            var employeeBenefit = new EmployeeBenefitCalculator(employee);

            var result = employeeBenefit.CalculateDepedentBenefitCost();
        
            Assert.That(result, Is.EqualTo(0m));
        }

        [Test]
        public void CalculateDependentEmployeeBenefitCost_WithOneDependent_Returns500()
        {
            var employee = EmployeeBuilder
                            .DefaultEmployee()
                            .AddDependent()
                            .Build();

            var employeeBenefit = new EmployeeBenefitCalculator(employee);

            var result = employeeBenefit.CalculateDepedentBenefitCost();

            Assert.That(result, Is.EqualTo(DependentCost));
        }

        [Test]
        public void CalculateDependentEmployeeBenefitCost_WithTwoDependents_Returns1000()
        {
            var employee = EmployeeBuilder
                            .DefaultEmployee()
                            .AddDependent()
                            .AddDependent()
                            .Build();

            var employeeBenefit = new EmployeeBenefitCalculator(employee);

            var result = employeeBenefit.CalculateDepedentBenefitCost();

            Assert.That(result, Is.EqualTo((DependentCost * 2m)));
        }

        [Test]
        public void CalculateDependentEmployeeBenefitCost_WithDependentStartsWithA_ReturnsAdjustedValue()
        {
            var employee = EmployeeBuilder
                            .DefaultEmployee()
                            .AddDependentThatStartsWithA()
                            .Build();

            var employeeBenefit = new EmployeeBenefitCalculator(employee);

            var result = employeeBenefit.CalculateDepedentBenefitCost();
            var expected = DependentCost*AdjustmentRate;

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetPayChecks_EmployeeWithNoDependents_RetunsCorrectAmount()
        {
            var employee = EmployeeBuilder
                .DefaultEmployee()
                .Build();

            var employeeBenefit = new EmployeeBenefitCalculator(employee);

            var result = employeeBenefit.GetPayChecksForYear();

            var expected = ((EmployeeSalary*PayCheckCount - EmployeeCost));

            Assert.That(result.Sum(), Is.EqualTo(expected));
        }

        [Test]
        public void GetPayChecks_EmployeeWithAName_ReturnsCorrectAmount()
        {
            var employee = EmployeeBuilder
                .DefaultEmployee()
                .WithAFirstName()
                .Build();

            var employeeBenefit = new EmployeeBenefitCalculator(employee);

            var result = employeeBenefit.GetPayChecksForYear();

            var expected = (EmployeeSalary*PayCheckCount - (EmployeeCost*AdjustmentRate));

            Assert.That(result.Sum(), Is.EqualTo(expected));
        }

        [Test]
        public void GetPayChecks_EmployeeWith2Dependents_ReturnsCorrectAmount()
        {
            var employee = EmployeeBuilder
                .DefaultEmployee()
                .AddDependent()
                .AddDependent()
                .Build();

            var employeeBenefit = new EmployeeBenefitCalculator(employee);

            var result = employeeBenefit.GetPayChecksForYear();

            var expected = (EmployeeSalary * PayCheckCount - (EmployeeCost + (DependentCost * 2m)));

            Assert.That(result.Sum(), Is.EqualTo(expected));
        }

        [Test]
        public void GetPayChecks_EmployeeWith2DependentsOneWithAName_ReturnsCorrectAmount()
        {
            var employee = EmployeeBuilder
                .DefaultEmployee()
                .AddDependent()
                .AddDependentThatStartsWithA()
                .Build();

            var employeeBenefit = new EmployeeBenefitCalculator(employee);

            var result = employeeBenefit.GetPayChecksForYear();

            var expected = (EmployeeSalary * PayCheckCount - (EmployeeCost + (DependentCost) + (DependentCost * AdjustmentRate)));

            Assert.That(result.Sum(), Is.EqualTo(expected));
        }

        [Test]
        public void GetPayChecks_EmployeeWithANameWith2DependentsOneWithAName_ReturnsCorrectAmount()
        {
            var employee = EmployeeBuilder
                .DefaultEmployee()
                .WithAFirstName()
                .AddDependent()
                .AddDependentThatStartsWithA()
                .Build();

            var employeeBenefit = new EmployeeBenefitCalculator(employee);

            var result = employeeBenefit.GetPayChecksForYear();

            var expected = (EmployeeSalary * PayCheckCount - ((EmployeeCost * AdjustmentRate) + (DependentCost) + (DependentCost * AdjustmentRate)));

            Assert.That(result.Sum(), Is.EqualTo(expected));
        }

    }
}
