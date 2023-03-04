using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Logic.Models;

namespace WhatIfBudget.Logic.Interfaces
{
    public interface IIncomeLogic
    {
        public IList<UserIncome> GetUserIncomes(Guid userId);

        public IList<UserIncome> GetBudgetIncomes(int budgetId);

        public double GetBudgetMonthlyIncome(int budgetId);

        public UserIncome? AddUserIncome(Guid userId, UserIncome income);

        public UserIncome? ModifyUserIncome(Guid userId, UserIncome income);

        public UserIncome? DeleteBudgetIncome(int incomeId, int budgetId);
    }
}
