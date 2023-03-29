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

        private IList<Debt> OrderDebtAvalanche(IList<Debt> debtList)
        {
            var orderedList = new Dictionary<Debt, double>();
            foreach (var debt in debtList)
            {
                var stepper = new BalanceStepUtility((double)debt.CurrentBalance, debt.InterestRate / 12);
                while(stepper.Balance > 0)
                {
                    _ = stepper.Step(-1 * debt.MinimumPayment);
                }
                var totalMonths = stepper.NumberOfSteps;
                var totalInterestPaid = debt.CurrentBalance * (debt.InterestRate / 100) * totalMonths;
                orderedList.Add(debt, totalInterestPaid);
            }
            return orderedList.OrderByDescending(x => x.Value).Select(x => x.Key).ToList();
        }

        private (Dictionary<int, double>, DebtGoalTotals) CalculateBalanceOverTime(UserDebtGoal debtGoal)
        {
            //Dict<int, double> int is month, double is balance left
            //need to use - for balance

            var debtList = _debtService.GetDebtsByDebtGoalId(debtGoal.Id);

            // Return Values
            Dictionary<int, double> balanceDict = new Dictionary<int, double>();
            var debtGoalTotals = new DebtGoalTotals();

            var orderedDebtList = OrderDebtAvalanche(debtList);

            var allocationRolloverMonth = 0;
            var remainder = 0.0;
            var paidOffDebtsMinimumPayments = 0.0;

            foreach (var debt in orderedDebtList)
            {
                var currentBalance = debt.CurrentBalance;
                var interestRate = debt.InterestRate / 12;
                var interestAccrued = 0.0;

                var stepper = new BalanceStepUtility(currentBalance, interestRate);

                while(stepper.Balance > 0)
                {
                    var contribution = debt.MinimumPayment;
                    if(stepper.NumberOfSteps >= allocationRolloverMonth)
                    {
                        contribution += debtGoal.AdditionalBudgetAllocation;
                        contribution += paidOffDebtsMinimumPayments;
                    }
                    //if we carry over any contribution, use it and set remainder back to 0
                    if(remainder > 0)
                    {
                        contribution += remainder;
                        remainder = 0.0;
                    }
                    //if we have more to contribute than what is left on the balance, carry over to next debt
                    if (contribution > stepper.Balance)
                    {
                        remainder = contribution - stepper.Balance;
                        contribution = stepper.Balance;
                    }
                    
                    _ = stepper.Step(-1 * contribution);

                    if (!balanceDict.ContainsKey(stepper.NumberOfSteps)) balanceDict.Add(stepper.NumberOfSteps, 0);

                    balanceDict[stepper.NumberOfSteps] += Math.Round(stepper.Balance, 2);

                    if(balanceDict[stepper.NumberOfSteps] == 0.0)
                    {
                        allocationRolloverMonth = stepper.NumberOfSteps;
                        paidOffDebtsMinimumPayments += debt.MinimumPayment;
                    }
                }
                debtGoalTotals.TotalInterestAccrued += stepper.InterestAccumulated;
            }
            return (balanceDict, debtGoalTotals);
        }
    }
}
