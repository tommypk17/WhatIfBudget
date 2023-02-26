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
        public UserInvestmentGoal? GetInvestmentGoal(int investmentGoalId);
        public double GetBalanceAtTarget(int investmentGoalId);
        public Dictionary<int, double> GetBalanceOverTime(int investmentGoalId);
        public UserInvestmentGoal? AddUserInvestmentGoal(Guid userId, UserInvestmentGoal investmentGoal);
        public UserInvestmentGoal? ModifyUserInvestmentGoal(UserInvestmentGoal investmentGoal);
    }
}
