﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Logic.Models;

namespace WhatIfBudget.Logic.Interfaces
{
    public interface IMonthlyNeedLogic
    {
        public double GetBudgetMonthlyNeed(int budgetId);
    }
}
