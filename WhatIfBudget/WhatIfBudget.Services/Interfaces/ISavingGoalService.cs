﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Services.Interfaces
{
    public interface ISavingGoalService
    {
        public SavingGoal? AddSavingGoal(SavingGoal savingGoal);
    }
}