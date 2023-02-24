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
    public class MonthlyNeedLogic : IMonthlyNeedLogic
    {
        private readonly IExpenseService _expenseService;
        private readonly IBudgetExpenseService _budgetExpenseService;

        public MonthlyNeedLogic(IExpenseService expenseService,
                           IBudgetExpenseService budgetExpenseService) { 
            _expenseService = expenseService;
            _budgetExpenseService= budgetExpenseService;
        }

        public double GetBudgetMonthlyNeed(int budgetId)
        {
            var expenseIdList = _budgetExpenseService.GetAllBudgetExpenses()
                .Where(x => x.BudgetId== budgetId)
                .Select(x => x.ExpenseId)
                .ToList();
            var needList = _expenseService.GetAllExpenses()
                .Where(x => expenseIdList.Contains(x.Id) && x.Priority == EPriority.Need)
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
    }
}
