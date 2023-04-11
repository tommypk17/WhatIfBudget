using WhatIfBudget.Common.Enumerations;
using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Logic.Models
{
    public class UserBudget
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public int SavingGoalId { get; set; }
        public int DebtGoalId { get; set; }
        public int MortgageGoalId { get; set; }
        public int InvestmentGoalId { get; set; }

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

    public class UserBudgetAllocations
    {
        public double DebtGoal { get; set; } = 0;
        public double MortgageGoal { get; set; } = 0;
        public double SavingGoal { get; set; } = 0;
        public double InvestmentGoal { get; set; } = 0;
    }

    public class NetWorthTotals
    {
        public Dictionary<int, double> Balance { get; set; }
        public int SavingGoalMonth { get; set; }
        public int DebtGoalMonth { get; set; }
        public int MortgageGoalMonth { get; set; }
    }
}
