using EmployeeBenefits.Data;

namespace EmployeeBenefits.Domain
{
    public class DependentBenefitCalculator  : BenefitCalculator
    {
        
        private const decimal BaseYearlyCost = 500m;

        public DependentBenefitCalculator(Dependent dependent):base(dependent.FirstName)
        {
        }

        public decimal Calculate()
        {
            return BenefitStrategy.AdjustBenefit(BaseYearlyCost);
        }

    }
}