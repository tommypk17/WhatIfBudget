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
    public class MonthlyIncomeLogic : IMonthlyIncomeLogic
    {
        private readonly IIncomeService _incomeService;
        private readonly IBudgetIncomeService _budgetIncomeService;

        public MonthlyIncomeLogic(IIncomeService incomeService,
                           IBudgetIncomeService budgetIncomeService) { 
            _incomeService = incomeService;
            _budgetIncomeService= budgetIncomeService;
        }

        public double GetBudgetMonthlyIncome(int budgetId)
        {
            var incomeIdList = _budgetIncomeService.GetAllBudgetIncomes()
                .Where(x => x.BudgetId== budgetId)
                .Select(x => x.IncomeId)
                .ToList();
            var incomeList = _incomeService.GetAllIncomes()
                .Where(x => incomeIdList.Contains(x.Id))
                .Select(x => UserIncome.FromIncome(x, budgetId))
                .ToList();

            double monthlyIncome = 0;
            var noneFreqList = incomeList.Where(x => x.Frequency == EFrequency.None).Select(x => x.Amount).ToList();
            var weekFreqList = incomeList.Where(x => x.Frequency == EFrequency.Weekly).Select(x => x.Amount).ToList();
            var monthFreqList = incomeList.Where(x => x.Frequency == EFrequency.Monthly).Select(x => x.Amount).ToList();
            var quartFreqList = incomeList.Where(x => x.Frequency == EFrequency.Quarterly).Select(x => x.Amount).ToList();
            var yearFreqList = incomeList.Where(x => x.Frequency == EFrequency.Yearly).Select(x => x.Amount).ToList();

            monthlyIncome += noneFreqList.Sum();
            monthlyIncome += weekFreqList.Sum() * 4;
            monthlyIncome += monthFreqList.Sum();
            monthlyIncome += quartFreqList.Sum() / 3;
            monthlyIncome += yearFreqList.Sum() / 12;

            return monthlyIncome;
        }
    }
}
