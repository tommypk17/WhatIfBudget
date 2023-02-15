using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Logic.Models
{
    public class UserInvestment
    {
        public int id { get; set; }
        public int GoalId { get; set; }
        public string Name { get; set; } = String.Empty;
        public double CurrentBalance { get; set; } = 0;
        public double MonthlyPersonalContribution { get; set; } = 0;
        public double MonthlyEmployerContribution { get; set; } = 0;
    }
}
