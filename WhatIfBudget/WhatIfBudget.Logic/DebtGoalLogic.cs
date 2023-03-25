using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Logic.Models;
using WhatIfBudget.Services.Interfaces;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Logic;
using System.Dynamic;
using System.Globalization;
using WhatIfBudget.Services;
using WhatIfBudget.Logic.Utilities;

namespace WhatIfBudget.Logic
{
    public class DebtGoalLogic : IDebtGoalLogic
    {
        private readonly IDebtGoalService _debtGoalService;
        private readonly IDebtService _debtService;
        public DebtGoalLogic(IDebtGoalService debtGoalService, IDebtService debtService) { 
            _debtGoalService = debtGoalService;
            _debtService = debtService;
        }

        public UserDebtGoal? GetDebtGoal(int debtGoalId)
        {
            var debtGoal = _debtGoalService.GetDebtGoal(debtGoalId);
            if(debtGoal is null) return null;
            return UserDebtGoal.FromDebtGoal(debtGoal);
        }

        public UserDebtGoal? ModifyUserDebtGoal(UserDebtGoal debtGoal)
        {
            var toUpdate = debtGoal.ToDebtGoal();

            var dbDebtGoal = _debtGoalService.UpdateDebtGoal(toUpdate);
            if (dbDebtGoal == null)
            {
                throw new NullReferenceException();
            }
            return UserDebtGoal.FromDebtGoal(dbDebtGoal);
        }

        public Dictionary<int, double> GetBalanceOverTime(int debtGoalId)
        {
            var dbDebtGoal = _debtGoalService.GetDebtGoal(debtGoalId);
            if (dbDebtGoal is null) { throw new NullReferenceException(); }
            UserDebtGoal debtGoal = UserDebtGoal.FromDebtGoal(dbDebtGoal);
            (var dict, _) = CalculateBalanceOverTime(debtGoal);
            return dict;
        }

        private (Dictionary<int, double>, DebtGoalTotals) CalculateBalanceOverTime(UserDebtGoal debtGoal)
        {
            //Dict<int, double> int is month, double is balance left
            //need to use - for balance

            var debtList = _debtService.GetDebtsByDebtGoalId(debtGoal.Id);

            // Return Values
            Dictionary<int, double> balanceDict = new Dictionary<int, double>();
            var debtGoalTotals = new DebtGoalTotals();

            debtList = debtList.OrderByDescending(x => x.InterestRate).ToList();

            foreach (var debt in debtList)
            {
                var currentMonth = 1;
                var currentBalance = debt.CurrentBalance;
                var interestRate = debt.InterestRate / 12;
                var interestAccrued = 0.0;

                var stepper = new BalanceStepUtility(currentBalance, interestRate);
                stepper.StepToZero(debtGoal.AdditionalBudgetAllocation + debt.MinimumPayment);
                var totalMonths = stepper.StepsCompleted();
                var remainder = 0.0;

                while(currentMonth <= totalMonths)
                {
                    var contribution = (debtGoal.AdditionalBudgetAllocation + debt.MinimumPayment);
                    //if we carry over any contribution, use it and set remainder back to 0
                    if(remainder > 0)
                    {
                        contribution += remainder;
                        remainder = 0.0;
                    }
                    //if we have more to contribute than what is left on the balance, carry over to next debt
                    if (contribution > currentBalance)
                    {
                        remainder = contribution - currentBalance;
                        contribution = currentBalance;
                    }
                    
                    _ = stepper.Step(-1 * contribution);
                    debtGoalTotals.TotalInterestAccrued += interestAccrued;

                    if (!balanceDict.ContainsKey(currentMonth)) balanceDict.Add(currentMonth, 0);

                    balanceDict[currentMonth] += Math.Round(stepper.GetBalance(), 2);

                    currentMonth++;
                }
                debtGoalTotals.TotalInterestAccrued += stepper.GetAccumulatedInterest();
            }
            return (balanceDict, debtGoalTotals);
        }
    }
}
