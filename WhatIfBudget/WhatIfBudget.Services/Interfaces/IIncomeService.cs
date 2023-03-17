using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Services.Interfaces
{
    public interface IIncomeService
    {
        public IList<Income> GetAllIncomes();

        public Income? AddNewIncome(Income newIncome);

        public Income? UpdateIncome(Income modifiedIncome);

        public Income? DeleteIncome(int id);

        public IList<Income> GetIncomesByBudgetId(int budgetId);
    }
}
