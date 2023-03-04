using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Logic.Models;

namespace WhatIfBudget.Logic.Interfaces
{
    public interface IExpenseLogic
    {
        public IList<UserExpense> GetUserExpenses(Guid userId);

        public IList<UserExpense> GetBudgetExpenses(int budgetId);

        public double GetBudgetMonthlyExpense(int budgetId);

        public double GetBudgetMonthlyNeed(int budgetId);

        public double GetBudgetMonthlyWant(int budgetId);

        public UserExpense? AddUserExpense(Guid userId, UserExpense expense);

        public UserExpense? ModifyUserExpense(Guid userId, UserExpense expense);

        public UserExpense? DeleteBudgetExpense(int id, int budgetId);

    }
}
