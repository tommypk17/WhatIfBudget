using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Logic.Models;

namespace WhatIfBudget.Logic.Interfaces
{
    public interface ISavingGoalLogic
    {
        public UserSavingGoal? GetSavingGoal(int savingGoalId);
        public int GetTimeToTarget(int savingGoalId);
        public Dictionary<int, double> GetBalanceOverTime(int savingGoalId);
        public UserSavingGoal? ModifyUserSavingGoal(UserSavingGoal savingGoal);
    }
}
