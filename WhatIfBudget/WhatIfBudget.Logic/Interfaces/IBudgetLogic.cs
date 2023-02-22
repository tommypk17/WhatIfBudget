using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Logic.Models;

namespace WhatIfBudget.Logic.Interfaces
{
    public interface IBudgetLogic
    {
        public IList<UserBudget> GetUserBudgets(Guid userId);

        public UserBudget? GetBudget(int budgetId);

        public UserBudget? AddUserBudget(Guid userId, UserBudget budget);

        public UserBudget? ModifyUserBudget(Guid userId, UserBudget budget);

        public UserBudget? DeleteUserBudget(int id);
    }
}
