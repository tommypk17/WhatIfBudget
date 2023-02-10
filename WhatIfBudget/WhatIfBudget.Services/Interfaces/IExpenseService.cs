using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Services.Interfaces
{
    public interface IExpenseService
    {
        public IList<Expense> GetAllExpenses();

        public Expense? AddNewExpense(Expense newExpense);
        public Expense? UpdateExpense(Expense modifiedExpense);
    }
}
