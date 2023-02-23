using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Services.Interfaces
{
    public interface IInvestmentGoalService
    {
        public InvestmentGoal? GetInvestmentGoal(int id);

        public InvestmentGoal? AddInvestmentGoal(InvestmentGoal investmentGoal);

        public InvestmentGoal? UpdateInvestmentGoal(InvestmentGoal modifiedInvestmentGoal);

        public InvestmentGoal? DeleteInvestmentGoal(int id);
    }
}
