using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Services.Interfaces
{
    public interface IMortgageGoalService
    {
        public MortgageGoal? GetMortgageGoal(int id);
        public MortgageGoal? AddMortgageGoal(MortgageGoal mortgageGoal);
        public MortgageGoal? ModifyMortgageGoal(MortgageGoal mortgageGoal);
        public MortgageGoal? DeleteMortgageGoal(int id);
    }
}
