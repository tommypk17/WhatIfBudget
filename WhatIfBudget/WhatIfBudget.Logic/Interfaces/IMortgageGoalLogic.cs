using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Logic.Models;

namespace WhatIfBudget.Logic.Interfaces
{
    public interface IMortgageGoalLogic
    {
        public UserMortgageGoal? GetMortgageGoal(int MortgageGoalId);
        public MortgageGoalTotals GetMortgageTotals(int MortgageGoalId);
        public Dictionary<int, double> GetNetValueOverTime(int MortgageGoalId, int totalMonths = 0);
        public Dictionary<int, List<double>> GetAmortizationTable(int MortgageGoalId);
        public UserMortgageGoal? ModifyUserMortgageGoal(UserMortgageGoal MortgageGoal);
    }
}
