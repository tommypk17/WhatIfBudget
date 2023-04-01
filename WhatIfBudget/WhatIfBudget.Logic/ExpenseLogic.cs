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
using WhatIfBudget.Common.Enumerations;

namespace WhatIfBudget.Logic
{
    public class ExpenseLogic : IExpenseLogic
    {
        private readonly IExpenseService _expenseService;
        private readonly IBudgetExpenseService _budgetExpenseService;
        private readonly IBudgetService _budgetService;

        public ExpenseLogic(IExpenseService expenseService,
                            IBudgetExpenseService budgetExpenseService,
                            IBudgetService budgetService) { 
            _expenseService = expenseService;
            _budgetExpenseService= budgetExpenseService;
            _budgetService = budgetService;
        }

        public IList<UserExpense> GetUserExpenses(Guid userId)
        {
            return _expenseService.GetAllExpenses().Where(x => x.UserId == userId)
                                                   .Select(x => UserExpense.FromExpense(x, 0)) // TODO: budgetId
                                                   .ToList();
        }

        public IList<UserExpense> GetBudgetExpenses(int budgetId)
        {
            return _expenseService.GetExpensesByBudgetId(budgetId)
                .Select(x => UserExpense.FromExpense(x, budgetId))
                .ToList();
        }

        public double GetBudgetMonthlyExpense(int budgetId)
        {
            var expenseList = _expenseService.GetExpensesByBudgetId(budgetId)
                .Select(x => UserExpense.FromExpense(x, budgetId))
                .ToList();

            double monthlyExpense = 0;
            var noneFreqList = expenseList.Where(x => x.Frequency == EFrequency.None).Select(x => x.Amount).ToList();
            var weekFreqList = expenseList.Where(x => x.Frequency == EFrequency.Weekly).Select(x => x.Amount).ToList();
            var monthFreqList = expenseList.Where(x => x.Frequency == EFrequency.Monthly).Select(x => x.Amount).ToList();
            var quartFreqList = expenseList.Where(x => x.Frequency == EFrequency.Quarterly).Select(x => x.Amount).ToList();
            var yearFreqList = expenseList.Where(x => x.Frequency == EFrequency.Yearly).Select(x => x.Amount).ToList();

            monthlyExpense += noneFreqList.Sum();
            monthlyExpense += weekFreqList.Sum() * 4;
            monthlyExpense += monthFreqList.Sum();
            monthlyExpense += quartFreqList.Sum() / 3;
            monthlyExpense += yearFreqList.Sum() / 12;

            return monthlyExpense;
        }

        public double GetBudgetMonthlyNeed(int budgetId)
        {
            var needList = _expenseService.GetExpensesByBudgetId(budgetId)
                .Where(x => x.Priority == EPriority.Need)
                .Select(x => UserExpense.FromExpense(x, budgetId))
                .ToList();

            double monthlyNeed = 0;
            var noneFreqList = needList.Where(x => x.Frequency == EFrequency.None).Select(x => x.Amount).ToList();
            var weekFreqList = needList.Where(x => x.Frequency == EFrequency.Weekly).Select(x => x.Amount).ToList();
            var monthFreqList = needList.Where(x => x.Frequency == EFrequency.Monthly).Select(x => x.Amount).ToList();
            var quartFreqList = needList.Where(x => x.Frequency == EFrequency.Quarterly).Select(x => x.Amount).ToList();
            var yearFreqList = needList.Where(x => x.Frequency == EFrequency.Yearly).Select(x => x.Amount).ToList();

            monthlyNeed += noneFreqList.Sum();
            monthlyNeed += weekFreqList.Sum() * 4;
            monthlyNeed += monthFreqList.Sum();
            monthlyNeed += quartFreqList.Sum() / 3;
            monthlyNeed += yearFreqList.Sum() / 12;

            return monthlyNeed;
        }

        public double GetBudgetMonthlyWant(int budgetId)
        {
            var wantList = _expenseService.GetExpensesByBudgetId(budgetId)
                .Where(x => x.Priority == EPriority.Want)
                .Select(x => UserExpense.FromExpense(x, budgetId))
                .ToList();

            double monthlyWant = 0;
            var noneFreqList = wantList.Where(x => x.Frequency == EFrequency.None).Select(x => x.Amount).ToList();
            var weekFreqList = wantList.Where(x => x.Frequency == EFrequency.Weekly).Select(x => x.Amount).ToList();
            var monthFreqList = wantList.Where(x => x.Frequency == EFrequency.Monthly).Select(x => x.Amount).ToList();
            var quartFreqList = wantList.Where(x => x.Frequency == EFrequency.Quarterly).Select(x => x.Amount).ToList();
            var yearFreqList = wantList.Where(x => x.Frequency == EFrequency.Yearly).Select(x => x.Amount).ToList();

            monthlyWant += noneFreqList.Sum();
            monthlyWant += weekFreqList.Sum() * 4;
            monthlyWant += monthFreqList.Sum();
            monthlyWant += quartFreqList.Sum() / 3;
            monthlyWant += yearFreqList.Sum() / 12;

            return monthlyWant;
        }

        public UserExpense? AddUserExpense(Guid userId, UserExpense expense)
        {
            if (!_budgetService.Exists(expense.BudgetId)) return null;
            if (expense.BudgetId == 0) return null;
            // Create expense element if it does not yet exist
            var dbExpense = _expenseService.GetAllExpenses()
                .Where(x => x.Id == expense.Id)
                .FirstOrDefault();
            if (dbExpense == null)
            {
                dbExpense = _expenseService.AddNewExpense(expense.ToExpense(userId));
                if (dbExpense == null)
                {
                    throw new NullReferenceException();
                }
            }

            // Associate expense element to current budget
            var budgetExpenseToCreate = new BudgetExpense
            {
                BudgetId = expense.BudgetId,
                ExpenseId = dbExpense.Id
            };

            var dbBudgetExpense = _budgetExpenseService.AddNewBudgetExpense(budgetExpenseToCreate);
            if (dbBudgetExpense == null)
            {
                throw new NullReferenceException();
            }
            return UserExpense.FromExpense(dbExpense, expense.BudgetId);

        }

        public UserExpense? ModifyUserExpense(Guid userId, UserExpense expense)
        {
            var toUpdate = expense.ToExpense(userId);

            var dbExpense = _expenseService.UpdateExpense(toUpdate);
            if (dbExpense == null)
            {
                throw new NullReferenceException();
            }
            return UserExpense.FromExpense(dbExpense, expense.BudgetId);
        }

        public UserExpense? DeleteBudgetExpense(int expenseId, int budgetId)
        {
            // Get all instances of this expense being used
            var beList = _budgetExpenseService.GetAllBudgetExpenses()
                .Where(x => x.ExpenseId == expenseId)
                .ToList();
            // Remove this budget expense from the database
            var thisBE = beList.Where(x => x.BudgetId == budgetId).FirstOrDefault();
            if (thisBE == null) { throw new NullReferenceException() ; }
            var dbBudgetExpense = _budgetExpenseService.DeleteBudgetExpense(thisBE.Id);
            if (dbBudgetExpense == null) { throw new NullReferenceException(); }
            // Delete expense element if it is not associated with any other budget Id
            beList.Remove(thisBE);
            if (!beList.Where(x => x.ExpenseId == expenseId).Any())
            {
                var dbDeleteExpense = _expenseService.DeleteExpense(dbBudgetExpense.ExpenseId);
                if (dbDeleteExpense == null)
                {
                    throw new NullReferenceException();
                }
                return UserExpense.FromExpense(dbDeleteExpense, budgetId);
            }
            else
            {
                // Keep expense element in database for use by other budgets
                var dbDeleteExpense = _expenseService.GetAllExpenses()
                    .Where(x => x.Id == expenseId)
                    .FirstOrDefault();
                if (dbDeleteExpense == null)
                {
                    throw new NullReferenceException();
                }
                else { return UserExpense.FromExpense(dbDeleteExpense, budgetId); }
            }
        }
    }
}
