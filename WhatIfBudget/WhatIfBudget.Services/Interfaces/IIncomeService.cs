﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Services.Interfaces
{
    public interface IIncomeService
    {
        public IList<Income> GetAllIncome();

        public void AddNewIncome(Income newIncome);

        public void UpdateIncome(Income modifiedIncome);

        public void DeleteIncome(Income toRemove);
    }
}
