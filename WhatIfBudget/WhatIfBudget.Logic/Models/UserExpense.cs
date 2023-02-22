using WhatIfBudget.Common.Enumerations;
using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Logic.Models
{
    public class UserExpense
    {
        public int Id { get; set; } 
        public string Name { get; set; } = String.Empty;
        public double Amount { get; set; }
        public EFrequency Frequency { get; set; }
        public EPriority Priority { get; set; }
        public int BudgetId { get; set; }

        public Expense ToExpense(Guid? userId = null)
        {
            return new Expense()
            {
                Id = Id,
                Amount = Amount,
                Frequency = Frequency,
                Priority = Priority,
                UserId = userId != null ? userId.Value : Guid.Empty,
                Name = Name
            };
        }

        public static UserExpense FromExpense(Expense expense, int budgetId)
        {
            return new UserExpense()
            {
                Id = expense.Id,
                Amount = expense.Amount,
                Frequency = expense.Frequency,
                Priority = expense.Priority,
                Name = expense.Name,
                BudgetId= budgetId
            };
        }
    }
}
