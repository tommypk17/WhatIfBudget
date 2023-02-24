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
    public class MonthlyWantLogic : IMonthlyWantLogic
    {
        private readonly IExpenseService _expenseService;
        private readonly IBudgetExpenseService _budgetExpenseService;

        public MonthlyWantLogic(IExpenseService expenseService,
                           IBudgetExpenseService budgetExpenseService) { 
            _expenseService = expenseService;
            _budgetExpenseService= budgetExpenseService;
        }

        public double GetBudgetMonthlyWant(int budgetId)
        {
            var expenseIdList = _budgetExpenseService.GetAllBudgetExpenses()
                .Where(x => x.BudgetId== budgetId)
                .Select(x => x.ExpenseId)
                .ToList();
            var wantList = _expenseService.GetAllExpenses()
                .Where(x => expenseIdList.Contains(x.Id) && x.Priority == EPriority.Want)
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
    }
}
