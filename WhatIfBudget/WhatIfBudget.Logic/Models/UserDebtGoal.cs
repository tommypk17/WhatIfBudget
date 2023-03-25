using System.Xml.Linq;
using WhatIfBudget.Common.Enumerations;
using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Logic.Models
{
    public class UserDebtGoal
    {
        public int Id { get; set; }
        public double AdditionalBudgetAllocation { get; set; } = 0;

        public DebtGoal ToDebtGoal()
        {
            return new DebtGoal()
            {
                Id = Id,
                AdditionalBudgetAllocation = AdditionalBudgetAllocation
            };
        }

        public static UserDebtGoal FromDebtGoal(DebtGoal debtGoal)
        {
            return new UserDebtGoal()
            {
                Id = debtGoal.Id,
                AdditionalBudgetAllocation = debtGoal.AdditionalBudgetAllocation
            };
        }
    }

    public class DebtGoalTotals
    {
        public double TotalInterestAccrued { get; set; } = 0;
        public double AddedDueToContribution { get; set; } = 0;
    }
}
