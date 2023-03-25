using System.Xml.Linq;
using WhatIfBudget.Common.Enumerations;
using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Logic.Models
{
    public class UserInvestmentGoal
    {
        public int Id { get; set; }
        public double AnnualReturnRate_Percent { get; set; } = 0;
        public UInt16 YearsToTarget { get; set; } = 0;
        public double AdditionalBudgetAllocation { get; set; } = 0;
        public bool RolloverCompletedGoals { get; set; } = false;

        public InvestmentGoal ToInvestmentGoal()
        {
            return new InvestmentGoal()
            {
                Id = Id,
                AnnualReturnRate_Percent = AnnualReturnRate_Percent,
                YearsToTarget = YearsToTarget,
                AdditionalBudgetAllocation = AdditionalBudgetAllocation
            };
        }

        public static UserInvestmentGoal FromInvestmentGoal(InvestmentGoal investmentGoal)
        {
            return new UserInvestmentGoal()
            {
                Id = investmentGoal.Id,
                AnnualReturnRate_Percent = investmentGoal.AnnualReturnRate_Percent,
                YearsToTarget = investmentGoal.YearsToTarget,
                AdditionalBudgetAllocation = investmentGoal.AdditionalBudgetAllocation
            };
        }
    }

    public class InvestmentGoalTotals
    {
        public double BalanceAtTarget { get; set; } = 0;
        public double TotalInterestAccrued { get; set; } = 0;
        public double AddedDueToContribution { get; set; } = 0;
    }
}
