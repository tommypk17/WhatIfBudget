using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Services.Interfaces
{
    public interface IDebtGoalService
    {
        public DebtGoal? GetDebtGoal(int id);
        public DebtGoal? AddDebtGoal(DebtGoal debtGoal);
        public DebtGoal? UpdateDebtGoal(DebtGoal modifiedDebtGoal);
        public DebtGoal? DeleteDebtGoal(int id);

    }
}
