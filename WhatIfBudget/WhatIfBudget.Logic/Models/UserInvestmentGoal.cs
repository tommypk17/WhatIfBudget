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
        public UInt16 YearsToMaturation { get; set; } = 0;
        public double AnnualRaiseFactor_Percent { get; set; } = 0;
        public double additionalBudgetAllocation { get; set; } = 0;

        public InvestmentGoal ToInvestmentGoal()
        {
            return new InvestmentGoal()
            {
                Id = Id,
                TotalBalance = TotalBalance,
                AnnualReturnRate_Percent = AnnualReturnRate_Percent,
                YearsToMaturation = YearsToMaturation,
                AnnualRaiseFactor_Percent = AnnualRaiseFactor_Percent,
                AdditionalBudgetAllocation = additionalBudgetAllocation
            };
        }

        public static UserInvestmentGoal FromInvestmentGoal(InvestmentGoal investmentGoal)
        {
            return new UserInvestmentGoal()
            {
                Id = investmentGoal.Id,
                TotalBalance = investmentGoal.TotalBalance,
                AnnualReturnRate_Percent = investmentGoal.AnnualReturnRate_Percent,
                YearsToMaturation = investmentGoal.YearsToMaturation,
                AnnualRaiseFactor_Percent = investmentGoal.AnnualRaiseFactor_Percent,
                additionalBudgetAllocation = investmentGoal.AdditionalBudgetAllocation
            };
        }
    }
}
