using WhatIfBudget.Common.Enumerations;
using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Logic.Models
{
    public class UserBudget
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public int SavingGoalId { get; set; } = 0;
        public int DebtGoalId { get; set; } = 0;
        public int MortgageGoalId { get; set; } = 0;
        public int InvestmentGoalId { get; set; } = 0;

        public Budget ToBudget(Guid? userId = null)
        {
            return new Budget()
            {
                Id = Id,
                UserId = userId != null ? userId.Value : Guid.Empty,
                Name = Name,
                SavingGoalId = SavingGoalId,
                DebtGoalId = DebtGoalId,
                MortgageGoalId= MortgageGoalId,
                InvestmentGoalId= InvestmentGoalId
            };
        }

        public static UserBudget FromBudget(Budget budget)
        {
            return new UserBudget()
            {
                Id = budget.Id,
                Name = budget.Name,
                SavingGoalId= budget.SavingGoalId,
                DebtGoalId= budget.DebtGoalId,
                MortgageGoalId = budget.MortgageGoalId,
                InvestmentGoalId = budget.InvestmentGoalId
            };
        }
    }
}
