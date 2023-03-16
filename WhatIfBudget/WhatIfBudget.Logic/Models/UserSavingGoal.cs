using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Logic.Models
{
    public class UserSavingGoal
    {
        public int Id { get; set; }
        public double CurrentBalance { get; set; } = 0;
        public double TargetBalance { get; set; } = 0;
        public double AnnualReturnRate_Percent { get; set; } = 0;
        public double AdditionalBudgetAllocation { get; set; } = 0;

        public SavingGoal ToSavingGoal()
        {
            return new SavingGoal()
            {
                Id = Id,
                CurrentBalance = CurrentBalance,
                TargetBalance = TargetBalance,
                AnnualReturnRate_Percent = AnnualReturnRate_Percent,
                AdditionalBudgetAllocation = AdditionalBudgetAllocation
            };
        }

        public static UserSavingGoal FromSavingGoal(SavingGoal savingGoal)
        {
            return new UserSavingGoal()
            {
                Id = savingGoal.Id,
                CurrentBalance = savingGoal.CurrentBalance,
                TargetBalance = savingGoal.TargetBalance,
                AnnualReturnRate_Percent = savingGoal.AnnualReturnRate_Percent,
                AdditionalBudgetAllocation = savingGoal.AdditionalBudgetAllocation
            };
        }
    }
}
