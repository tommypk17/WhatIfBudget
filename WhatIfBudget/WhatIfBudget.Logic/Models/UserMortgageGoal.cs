using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Logic.Models
{
    public class UserMortgageGoal
    {
        public int Id { get; set; }
        public double TotalBalance { get; set; } = 0;
        public double InterestRate_Percent { get; set; } = 0;
        public double MonthlyPayment { get; set; } = 0;
        public double EstimatedCurrentValue { get; set; } = 0;
        public double AdditionalBudgetAllocation { get; set; } = 0;

        public MortgageGoal ToMortgageGoal()
        {
            return new MortgageGoal()
            {
                Id = Id,
                TotalBalance = TotalBalance,
                InterestRate_Percent = InterestRate_Percent,
                MonthlyPayment = MonthlyPayment,
                EstimatedCurrentValue= EstimatedCurrentValue,
                AdditionalBudgetAllocation = AdditionalBudgetAllocation
            };
        }

        public static UserMortgageGoal FromMortgageGoal(MortgageGoal MortgageGoal)
        {
            return new UserMortgageGoal()
            {
                Id = MortgageGoal.Id,
                TotalBalance = MortgageGoal.TotalBalance,
                InterestRate_Percent = MortgageGoal.InterestRate_Percent,
                MonthlyPayment = MortgageGoal.MonthlyPayment,
                EstimatedCurrentValue= MortgageGoal.EstimatedCurrentValue,
                AdditionalBudgetAllocation = MortgageGoal.AdditionalBudgetAllocation
            };
        }
    }
    public class MortgageGoalTotals
    {
        public int MonthsToPayoff { get; set; } = 0;
        public double TotalInterestAccrued { get; set; } = 0;
        public double TotalCostToPayoff { get; set; } = 0;
        public double AllocationSavings { get; set; } = 0;
    }
}
