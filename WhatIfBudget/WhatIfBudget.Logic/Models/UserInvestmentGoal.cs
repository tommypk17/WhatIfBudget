using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Logic.Models
{
    public class UserInvestmentGoal
    {
        public int id { get; set; }
        public int budgetId { get; set; }
        public double TotalBalance { get; set; } = 0;
        public double AnnualReturnRate_Percent { get; set; } = 0;
        public UInt16 YearsToMaturation { get; set; } = 30;
        public double AnnualRaiseFactor_Percent { get; set; } = 0;
        public double additionalPersonalAllocation { get; set; } = 0;
    }
}
