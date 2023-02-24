using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Logic.Models;
using WhatIfBudget.Services.Interfaces;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Common.Enumerations;

namespace WhatIfBudget.Logic
{
    public class MonthlyExpenseLogic : IMonthlyExpenseLogic
    {
        private readonly IExpenseService _expenseService;
        private readonly IBudgetExpenseService _budgetExpenseService;

        public MonthlyExpenseLogic(IExpenseService expenseService,
                           IBudgetExpenseService budgetExpenseService) { 
            _expenseService = expenseService;
            _budgetExpenseService= budgetExpenseService;
        }

        public double GetBudgetMonthlyExpense(int budgetId)
        {
            var expenseIdList = _budgetExpenseService.GetAllBudgetExpenses()
                .Where(x => x.BudgetId== budgetId)
                .Select(x => x.ExpenseId)
                .ToList();
            var expenseList = _expenseService.GetAllExpenses()
                .Where(x => expenseIdList.Contains(x.Id))
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
    }
}
