using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Services.Interfaces
{
    public interface IBudgetService
    {
        public Budget? GetBudget(int budgetId);
        public IList<Budget> GetAllBudgets();

        public Budget? AddNewBudget(Budget newBudget);

        public Budget? UpdateBudget(Budget modifiedBudget);

        public Budget? DeleteBudget(int id);
    }
}
