using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using EmployeeBenefits.Data;

namespace EmployeeBenefits.Domain
{
    public class EmployeeBenefitCalculator : BenefitCalculator
    {
        public Employee _employee;
        private const decimal BaseYearlyCost = 1000m;
        private const decimal PayCheckAmount = 2000m;
        private const decimal PayCheckCount = 26m;
        
      
        private readonly IBenefitStrategy _benefitStrategy;
        private readonly List<DependentBenefitCalculator> _dependentBenefits = new List<DependentBenefitCalculator>();

        public EmployeeBenefitCalculator(Employee employee) : base(employee.FirstName)
        {
            this._employee = employee;
            _dependentBenefits = _employee.Dependents.Select(d => new DependentBenefitCalculator(d)).ToList();
        }

     
        public decimal CalculateEmployeeBenefitCost()
        {
            return BenefitStrategy.AdjustBenefit(BaseYearlyCost);
        }

        public decimal CalculateDepedentBenefitCost()
        {
            return _dependentBenefits.Select(d => d.Calculate()).Sum();
        }

        public decimal CalculateYearlyNet()
        {
            
            var totalYearlyCost = CalculateEmployeeBenefitCost() + CalculateDepedentBenefitCost();

            var totalYearlyNet = (PayCheckAmount*PayCheckCount) - totalYearlyCost;

            return totalYearlyNet;

        }

        public List<decimal> GetPayChecksForYear()
        {
            var netYearly = CalculateYearlyNet();

            var netPayCheck = netYearly/PayCheckCount;

            var roundedAmount = Math.Round(netPayCheck, 2); //round for paycheck value

            var payChecks = CreatePaychecks(roundedAmount);

            //handle rounded differences so that all paychecks add to netyearly
            var roundedYearlyTotal = roundedAmount*PayCheckCount;

            //get diference between round paycheck and yearlynet
            var difference = netYearly - roundedYearlyTotal;
            if (difference != 0)
            {
                payChecks = AdjustPaychecks(difference, payChecks);
            }
            
            return payChecks;
        }

        private List<decimal> CreatePaychecks(decimal roundedAmount)
        {
            var checks = new List<decimal>();
            for (int i = 0; i < PayCheckCount; i++)
            {
                checks.Add(roundedAmount);
            }
            return checks;
        }

        private List<decimal> AdjustPaychecks(decimal difference, List<decimal> payChecks)
        {
            //apply differnce accross paychecks
            var totalAmountToAdjust = difference/0.01m;
            //if only adjusting by penny could be a bug if adjustments greater > 0.26
            var paychecksToAdjust = Math.Abs(totalAmountToAdjust);
            var adjustAmount = difference/paychecksToAdjust;
            for (int i = 0; i < paychecksToAdjust; i++)
            {
                payChecks[i] = payChecks[i] + adjustAmount;
            }

            return payChecks;
        }

        public decimal GetPayCheck(int payPeriod)
        {
            if (payPeriod > PayCheckCount)
                throw new ArgumentException("The pay period was not valid");

            
            return GetPayChecksForYear()[payPeriod -1];
        }
    }
}