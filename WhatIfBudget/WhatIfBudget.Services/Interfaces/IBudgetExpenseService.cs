using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Services.Interfaces
{
    public interface IBudgetExpenseService
    {
        public IList<BudgetExpense> GetAllBudgetExpenses();

        public BudgetExpense? AddNewBudgetExpense(BudgetExpense newBudgetExpense);

        public BudgetExpense? DeleteBudgetExpense(int id);
    }
}
