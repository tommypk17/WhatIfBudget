using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Logic.Models;
using WhatIfBudget.Services.Interfaces;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Services;

namespace WhatIfBudget.Logic
{
    public class ExpenseLogic : IExpenseLogic
    {
        private readonly IExpenseService _expenseService;
        public ExpenseLogic(IExpenseService expenseService) { 
            _expenseService = expenseService;
        }

        public IList<UserExpense> GetUserExpenses(Guid userId)
        {
            return _expenseService.GetAllExpenses().Where(x => x.UserId == userId).Select(x => UserExpense.FromExpense(x)).ToList();
        }

        public UserExpense AddUserExpense(Guid userId, UserExpense expense)
        {
            var toCreate = expense.ToExpense(userId);

            var dbExpense = _expenseService.AddNewExpense(toCreate);
            if (dbExpense == null)
            {
                throw new NullReferenceException();
            }
            return UserExpense.FromExpense(dbExpense);
        }

        public UserExpense? ModifyUserExpense(Guid userId, UserExpense expense)
        {
            var toUpdate = expense.ToExpense(userId);

            var dbExpense = _expenseService.UpdateExpense(toUpdate);
            if (dbExpense == null)
            {
                throw new NullReferenceException();
            }
            return UserExpense.FromExpense(dbExpense);
        }

        public UserExpense? DeleteUserExpense(Guid userId, int id)
        {
            var dbExpense = _expenseService.DeleteExpense(id);
            if (dbExpense == null)
            {
                throw new NullReferenceException();
            }
            return UserExpense.FromExpense(dbExpense);
        }
    }
}
