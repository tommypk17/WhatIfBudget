using System.Xml.Linq;
using WhatIfBudget.Common.Enumerations;
using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Logic.Models
{
    public class UserInvestmentGoal
    {
        public int Id { get; set; }
        public double TotalBalance { get; set; } = 0;
        public double AnnualReturnRate_Percent { get; set; } = 0;
        public UInt16 YearsToTarget { get; set; } = 0;
        public double additionalBudgetAllocation { get; set; } = 0;

        public InvestmentGoal ToInvestmentGoal()
        {
            return new InvestmentGoal()
            {
                Id = Id,
                AnnualReturnRate_Percent = AnnualReturnRate_Percent,
                YearsToTarget = YearsToTarget,
                AdditionalBudgetAllocation = additionalBudgetAllocation
            };
        }

        public static UserInvestmentGoal FromInvestmentGoal(InvestmentGoal investmentGoal)
        {
            return new UserInvestmentGoal()
            {
                Id = investmentGoal.Id,
                AnnualReturnRate_Percent = investmentGoal.AnnualReturnRate_Percent,
                YearsToTarget = investmentGoal.YearsToTarget,
                additionalBudgetAllocation = investmentGoal.AdditionalBudgetAllocation
            };
        }
    }
}
