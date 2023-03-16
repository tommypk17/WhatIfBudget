using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Logic.Models;
using WhatIfBudget.Services.Interfaces;
using WhatIfBudget.Data.Models;
using System.Dynamic;

namespace WhatIfBudget.Logic
{
    public class SavingGoalLogic : ISavingGoalLogic
    {
        private readonly ISavingGoalService _savingGoalService;

        public SavingGoalLogic(ISavingGoalService savingGoalService) {
            _savingGoalService = savingGoalService;
        }

        private Dictionary<int, double> GetBalanceOverTime(UserSavingGoal savingGoal)
        {
            int iMonth = 0;
            double iBalance = savingGoal.CurrentBalance;
            var balanceDict = new Dictionary<int, double> { { 0, 0.0 } };
            double monthlyRate = savingGoal.AnnualReturnRate_Percent / (12 * 100);

            while (iBalance < savingGoal.TargetBalance)
            {
                balanceDict[iMonth] = iBalance;
                iBalance += savingGoal.AdditionalBudgetAllocation;
                iBalance = Math.Round(iBalance * (1.0 + monthlyRate), 2);
                iMonth++;
            }
            // Final dictionary entry is full balance
            balanceDict[iMonth] = savingGoal.TargetBalance;
            return balanceDict;
        }

        public UserSavingGoal? GetSavingGoal(int savingGoalId)
        {
            var dbSavingGoal = _savingGoalService.GetSavingGoal(savingGoalId);
            if(dbSavingGoal is null) return null;

            var res = UserSavingGoal.FromSavingGoal(dbSavingGoal);
            return res;
        }

        public int GetTimeToTarget(int savingGoalId)
        {
            var dbSavingGoal = _savingGoalService.GetSavingGoal(savingGoalId);
            if (dbSavingGoal is null) { throw new NullReferenceException(); }
            var res = UserSavingGoal.FromSavingGoal(dbSavingGoal);
            var balanceDict = GetBalanceOverTime(res);
            return balanceDict.Keys.Last();
        }

        public Dictionary<int, double> GetBalanceOverTime(int savingGoalId)
        {
            var dbSavingGoal = _savingGoalService.GetSavingGoal(savingGoalId);
            if (dbSavingGoal is null) return null;
            var res = UserSavingGoal.FromSavingGoal(dbSavingGoal);
            var balanceDict = GetBalanceOverTime(res);
            return balanceDict;
        }

        public UserSavingGoal? ModifyUserSavingGoal(UserSavingGoal savingGoal)
        {
            var toModify = savingGoal.ToSavingGoal();

            var dbSavingGoal = _savingGoalService.ModifySavingGoal(toModify);
            if (dbSavingGoal == null)
            {
                throw new NullReferenceException();
            }
            var res = UserSavingGoal.FromSavingGoal(dbSavingGoal);
            return res;
        }
    }
}
