using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Logic.Models
{
    public class UserInvestmentGoalMemberships
    {
        public int id { get; set; }
        public int InvestmentGoalId { get; set; }
        public int InvestmentId { get; set; }
    }
}
