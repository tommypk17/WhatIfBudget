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

            var stepper = new BalanceStepUtility(dbSavingGoal.CurrentBalance, dbSavingGoal.AnnualReturnRate_Percent / 12);
            var savingGoalTotals = new SavingGoalTotals();
            stepper.StepToTarget(dbSavingGoal.AdditionalBudgetAllocation, dbSavingGoal.TargetBalance);
            savingGoalTotals.MonthsToTarget = stepper.NumberOfSteps;
            savingGoalTotals.TotalInterestAccrued = stepper.InterestAccumulated;

            return savingGoalTotals;
        }

        public Dictionary<int, double> GetBalanceOverTime(int savingGoalId, int totalMonths = 0)
        {
            var dbSavingGoal = _savingGoalService.GetSavingGoal(savingGoalId);
            if (dbSavingGoal is null) { throw new NullReferenceException(); }
            var res = UserSavingGoal.FromSavingGoal(dbSavingGoal);

            var stepper = new BalanceStepUtility(dbSavingGoal.CurrentBalance, dbSavingGoal.AnnualReturnRate_Percent / 12);
            var balanceDict = new Dictionary<int, double> { { 0, 0.0 } };

            balanceDict[0] = stepper.Balance;
            while (stepper.Balance < dbSavingGoal.TargetBalance)
            {
                _ = stepper.Step(dbSavingGoal.AdditionalBudgetAllocation);
                balanceDict[stepper.NumberOfSteps] = stepper.Balance;
            }
            // Further dictionary entries only accumulate interest
            while (stepper.NumberOfSteps < totalMonths)
            {
                _ = stepper.Step(0.0);
                balanceDict[stepper.NumberOfSteps] = stepper.Balance;
            }

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
