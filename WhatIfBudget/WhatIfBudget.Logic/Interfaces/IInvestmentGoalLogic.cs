using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Logic.Models;

namespace WhatIfBudget.Logic.Interfaces
{
    public interface IInvestmentGoalLogic
    {
        public UserInvestmentGoal? GetInvestmentGoal(int investmentGoalId);
        public InvestmentGoalTotals GetInvestmentTotals(int investmentGoalId);
        public Dictionary<int, double> GetBalanceOverTime(int investmentGoalId);
        public UserInvestmentGoal? ModifyUserInvestmentGoal(UserInvestmentGoal investmentGoal);
    }
}
