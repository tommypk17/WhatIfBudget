using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Logic.Models;

namespace WhatIfBudget.Logic.Interfaces
{
    public interface IInvestmentGoalLogic
    {
        public UserInvestmentGoal? GetBudgetInvestmentGoal(int budgetId);

        public UserInvestmentGoal? AddUserInvestmentGoal(Guid userId, UserInvestmentGoal income);

        public UserInvestmentGoal? ModifyUserInvestmentGoal(Guid userId, UserInvestmentGoal income);
    }
}
