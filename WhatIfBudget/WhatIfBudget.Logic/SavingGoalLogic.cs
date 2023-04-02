using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Logic.Models;
using WhatIfBudget.Logic.Utilities;
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

        private (Dictionary<int, double>, SavingGoalTotals) CalculateSavingsOverTime(UserSavingGoal savingGoal)
        {
            var stepper = new BalanceStepUtility(savingGoal.CurrentBalance, savingGoal.AnnualReturnRate_Percent / 12);

            // Return Values
            var balanceDict = new Dictionary<int, double> { { 0, 0.0 } };
            var savingGoalTotals = new SavingGoalTotals();

            balanceDict[0] = stepper.Balance;
            while (stepper.Balance < savingGoal.TargetBalance)
            {
                _ = stepper.Step(savingGoal.AdditionalBudgetAllocation);
                balanceDict[stepper.NumberOfSteps] = stepper.Balance;
            }
            // Final dictionary entry is full balance
            savingGoalTotals.MonthsToTarget = stepper.NumberOfSteps;
            savingGoalTotals.TotalInterestAccrued = stepper.InterestAccumulated;
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
            (_, var totals) = CalculateSavingsOverTime(res);
            return totals;
        }

        public Dictionary<int, double> GetBalanceOverTime(int savingGoalId)
        {
            var dbSavingGoal = _savingGoalService.GetSavingGoal(savingGoalId);
            if (dbSavingGoal is null) { throw new NullReferenceException(); }
            var res = UserSavingGoal.FromSavingGoal(dbSavingGoal);
            (var balanceDict, _) = CalculateSavingsOverTime(res);
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
