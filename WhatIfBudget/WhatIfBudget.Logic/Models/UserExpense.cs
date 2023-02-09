using WhatIfBudget.Common.Enumerations;
using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Logic.Models
{
    public class UserExpense
    {
        public int Id { get; set; } 
        public double Amount { get; set; }
        public EFrequency Frequency { get; set; }
        public EPriority Priority { get; set; }

        public Expense ToExpense(Guid? userId = null)
        {
            return new Expense()
            {
                Id = Id,
                Amount = Amount,
                Frequency = Frequency,
                Priority = Priority,
                UserId = userId != null ? userId.Value : Guid.Empty
            };
        }

        public static UserExpense FromExpense(Expense expense)
        {
            return new UserExpense()
            {
                Id = expense.Id,
                Amount = expense.Amount,
                Frequency = expense.Frequency,
                Priority = expense.Priority
            };
        }
    }
}
