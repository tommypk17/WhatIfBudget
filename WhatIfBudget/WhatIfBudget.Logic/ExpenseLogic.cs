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
        private readonly IBudgetExpenseService _budgetExpenseService;

        public ExpenseLogic(IExpenseService expenseService,
                            IBudgetExpenseService budgetExpenseService) { 
            _expenseService = expenseService;
            _budgetExpenseService= budgetExpenseService;
        }

        public IList<UserExpense> GetUserExpenses(Guid userId)
        {
            return _expenseService.GetAllExpenses().Where(x => x.UserId == userId).Select(x => UserExpense.FromExpense(x)).ToList();
        }

        public IList<UserExpense> GetBudgetExpenses(int budgetId)
        {
            var budgetExpenseIdList = _budgetExpenseService.GetAllBudgetExpenses()
                .Where(x => x.BudgetId == budgetId)
                .Select(x => x.ExpenseId)
                .ToList();

            return _expenseService.GetAllExpenses().Where(x => budgetExpenseIdList.Contains(x.Id))
                                                   .Select(x => UserExpense.FromExpense(x))
                                                   .ToList();
        }

        public UserExpense AddUserExpense(Guid userId, UserExpense expense, int budgetId)
        {
            // Associate expense element to current budget
            var budgetExpenseToCreate = new BudgetExpense
            {
                Id = 1, // TODO
                BudgetId = budgetId,
                ExpenseId = expense.Id
            };

            var dbBudgetExpense = _budgetExpenseService.AddNewBudgetExpense(budgetExpenseToCreate);
            if (dbBudgetExpense == null)
            {
                throw new NullReferenceException();
            }

            // Create expense element if it does not yet exist
            var dbExpense = _expenseService.GetAllExpenses()
                .Where(x => x.Id == expense.Id)
                .FirstOrDefault();
            if (dbExpense == null)
            {
                var dbNewExpense = _expenseService.AddNewExpense(expense.ToExpense(userId));
                if (dbNewExpense == null)
                {
                    throw new NullReferenceException();
                }
                return UserExpense.FromExpense(dbNewExpense);
            }
            else
            {
                return UserExpense.FromExpense(dbExpense);
            }
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

        public UserExpense? DeleteUserExpense(Guid userId, int expenseId, int budgetId)
        {
            // De-associate expense element from current budget
            var budgetExpenseToDelete = new BudgetExpense 
            {
                Id = 1 /*TODO*/,
                BudgetId = budgetId,
                ExpenseId = expenseId
            };
            var dbBudgetExpense = _budgetExpenseService.DeleteBudgetExpense(budgetExpenseToDelete.Id);
            if (dbBudgetExpense == null)
            {
                throw new NullReferenceException();
            }
            // Delete expense element if it is not associated with any other budget Id
            if (!_budgetExpenseService.GetAllBudgetExpenses()
                    .Where(x => x.ExpenseId == expenseId)
                    .Any())
            {
                var dbDeleteExpense = _expenseService.DeleteExpense(dbBudgetExpense.ExpenseId);
                if (dbDeleteExpense == null)
                {
                    throw new NullReferenceException();
                }
                return UserExpense.FromExpense(dbDeleteExpense);
            }
            else
            {
                // Keep expense element in database for use by other budgets
                return UserExpense.FromExpense(_expenseService.GetAllExpenses()
                    .Where(x => x.Id == expenseId)
                    .FirstOrDefault());
            }
        }
    }
}
