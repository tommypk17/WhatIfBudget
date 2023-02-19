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
    }
}
