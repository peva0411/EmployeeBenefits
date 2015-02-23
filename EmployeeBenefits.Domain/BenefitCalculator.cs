using System;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeBenefits.Domain
{
    public abstract class BenefitCalculator
    {
        protected IBenefitStrategy BenefitStrategy;

        protected BenefitCalculator(string name)
        {
            if (name.ToLower().StartsWith("a"))
            {
                BenefitStrategy = new StartsWithADiscountStrategy();
            }
            else
            {
                BenefitStrategy = new DefaultStrategy();
            }
        }
    }

    public class StartsWithADiscountStrategy : IBenefitStrategy
    {
        private const Decimal Discount = 0.10m;

        public decimal AdjustBenefit(decimal benefitAmount)
        {
            return (benefitAmount - (benefitAmount*Discount));
        }
    }

    public class DefaultStrategy : IBenefitStrategy
    {
        public decimal AdjustBenefit(decimal benefitAmount)
        {
            return (benefitAmount);
        }
    }

    public interface IBenefitStrategy
    {
        decimal AdjustBenefit(decimal benefitAmount);
    }
}
