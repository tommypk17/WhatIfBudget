using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Logic.Models;

namespace WhatIfBudget.Logic.Interfaces
{
    public interface IDebtLogic
    {
        public IList<UserDebt> GetUserDebts(Guid userId);
        public IList<UserDebt> GetUserDebtsByGoalId(Guid userId, int goalId);

    }
}
