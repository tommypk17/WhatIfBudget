using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatIfBudget.Data.Models
{
    public class BudgetExpense : BaseEntity
    {
        public int BudgetId { get; set; }
        public int ExpenseId { get; set; }

        public virtual Expense Expense { get; set; } = new Expense();
        public virtual Budget Budget { get; set; } = new Budget();
    }
}
