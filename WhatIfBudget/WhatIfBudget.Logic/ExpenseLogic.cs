﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Logic.Models;
using WhatIfBudget.Services.Interfaces;
using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Logic
{
    public class ExpenseLogic : IExpenseLogic
    {
        private readonly IExpenseService _expenseService;
        public ExpenseLogic(IExpenseService expenseService) { 
            _expenseService = expenseService;
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
    }
}