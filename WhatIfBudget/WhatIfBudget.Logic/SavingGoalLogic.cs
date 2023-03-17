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
using System.Runtime.CompilerServices;

namespace WhatIfBudget.Logic
{
    public class SavingGoalLogic : ISavingGoalLogic
    {
        private readonly ISavingGoalService _savingGoalService;

        public SavingGoalLogic(ISavingGoalService savingGoalService) {
            _savingGoalService = savingGoalService;
        }

        private (Dictionary<int, double>, SavingGoalTotals) CalculateBalanceOverTime(UserSavingGoal savingGoal)
        {
            var utilities = new LogicUtilities();
            double monthlyRate = savingGoal.AnnualReturnRate_Percent / (12 * 100);

            // Return Values
            var balanceDict = new Dictionary<int, double> { { 0, 0.0 } };
            var savingGoalTotals = new SavingGoalTotals();
            double iBalance = savingGoal.CurrentBalance;
            double iInterest;
            double totalInterest = 0;
            int iMonth = 0;

            while (iBalance < savingGoal.TargetBalance)
            {
                balanceDict[iMonth] = iBalance;
                (iBalance, iInterest) = utilities.InterestStep(iBalance, monthlyRate, savingGoal.AdditionalBudgetAllocation);
                savingGoalTotals.TotalInterestAccrued += iInterest;
                iMonth++;
            }
            // Final dictionary entry is full balance
            balanceDict[iMonth] = savingGoal.TargetBalance;
            savingGoalTotals.MonthsToTarget = iMonth;
            return (balanceDict, savingGoalTotals);
        }

        public UserSavingGoal? GetSavingGoal(int savingGoalId)
        {
            var dbSavingGoal = _savingGoalService.GetSavingGoal(savingGoalId);
            if(dbSavingGoal is null) return null;

            var res = UserSavingGoal.FromSavingGoal(dbSavingGoal);
            return res;
        }

        public SavingGoalTotals GetSavingTotals(int savingGoalId)
        {
            var dbSavingGoal = _savingGoalService.GetSavingGoal(savingGoalId);
            if (dbSavingGoal is null) { throw new NullReferenceException(); }
            var res = UserSavingGoal.FromSavingGoal(dbSavingGoal);
            (_, var totals) = CalculateBalanceOverTime(res);
            return totals;
        }

        public Dictionary<int, double> GetBalanceOverTime(int savingGoalId)
        {
            var dbSavingGoal = _savingGoalService.GetSavingGoal(savingGoalId);
            if (dbSavingGoal is null) return null;
            var res = UserSavingGoal.FromSavingGoal(dbSavingGoal);
            (var balanceDict, _) = CalculateBalanceOverTime(res);
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
