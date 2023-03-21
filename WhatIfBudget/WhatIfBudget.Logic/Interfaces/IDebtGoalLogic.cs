﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Logic.Models;

namespace WhatIfBudget.Logic.Interfaces
{
    public interface IDebtGoalLogic
    {
        public UserDebtGoal? GetDebtGoal(int debtGoalId);
    }
}
