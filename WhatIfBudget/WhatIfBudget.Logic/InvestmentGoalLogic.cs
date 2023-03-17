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

namespace WhatIfBudget.Logic
{
    public class InvestmentGoalLogic : IInvestmentGoalLogic
    {
        private readonly IInvestmentGoalService _investmentGoalService;
        private readonly IInvestmentService _investmentService;
        public InvestmentGoalLogic(IInvestmentGoalService investmentGoalService, IInvestmentService investmentService) { 
            _investmentGoalService = investmentGoalService;
            _investmentService = investmentService;
        }

        private double GetCompletedGoalContributions(UserInvestmentGoal investmentGoal, int futureMonth)
        {
            var total = 0.0;
            // Rollover contributions from other goals
            /*
             * 1. Get all other goals from parent budget
             * 2. If saving goal is complete at <futureMonth> add the allocation to total
             * 3. If debt goal is complete at <futureMonth> add the allocation and minimum payments to total
             * 4. If mortgage goal is complete at <futureMonth> add the allocation and minimum payment to total
             */

            return total;
        }

        private (Dictionary<int, double>, InvestmentGoalTotals) CalculateBalanceOverTime(UserInvestmentGoal investmentGoal)
        {
            var utilities = new LogicUtilities();
            double monthlyInterestRate = investmentGoal.AnnualReturnRate_Percent / 1200;
            var investmentList = _investmentService.GetInvestmentsByInvestmentGoalId(investmentGoal.Id);
            var startingBalance = investmentList.Select(x => x.CurrentBalance).Sum();

            // Return Values
            Dictionary<int, double> balanceDict = new Dictionary<int, double>();
            var investmentGoalTotals = new InvestmentGoalTotals();
            double iInterestAccrued = 0;
            double iBalance = startingBalance;
            double iAllocationBalance = 0;
            double iAllocationInterest = 0;

            double baseContribution = investmentGoal.AdditionalBudgetAllocation;
            foreach (var inv in investmentList)
            {
                baseContribution += inv.MonthlyPersonalContribution;
                baseContribution += inv.MonthlyEmployerContribution;
            }

            int iMonth = 1;
            balanceDict[0] = Math.Round(iBalance, 2);
            while (iMonth <= investmentGoal.YearsToTarget * 12)
            {
                var iContribution = baseContribution; // + GetCompletedGoalContributions(investmentGoal, iMonth);
                (iBalance, iInterestAccrued) = utilities.InterestStep(iBalance, monthlyInterestRate, iContribution);
                (iAllocationBalance, iAllocationInterest) = utilities.InterestStep(iAllocationBalance, monthlyInterestRate, investmentGoal.AdditionalBudgetAllocation);
                investmentGoalTotals.TotalInterestAccrued += iInterestAccrued;

                if (iMonth % 12 == 0)
                {
                    balanceDict[iMonth / 12] = Math.Round(iBalance, 2);
                }
                iMonth++;
            }
            investmentGoalTotals.BalanceAtTarget = balanceDict[investmentGoal.YearsToTarget];
            investmentGoalTotals.AddedDueToContribution = investmentGoalTotals.BalanceAtTarget - iAllocationBalance;
            return (balanceDict, investmentGoalTotals);
        }

        public UserInvestmentGoal? GetInvestmentGoal(int investmentGoalId)
        {
            var investmentGoal = _investmentGoalService.GetInvestmentGoal(investmentGoalId);
            if(investmentGoal is null) return null;
            return UserInvestmentGoal.FromInvestmentGoal(investmentGoal);
        }

        public InvestmentGoalTotals GetInvestmentTotals(int investmentGoalId)
        {
            var dbInvestmentGoal = _investmentGoalService.GetInvestmentGoal(investmentGoalId);
            if (dbInvestmentGoal is null) { throw new NullReferenceException(); }
            UserInvestmentGoal investmentGoal = UserInvestmentGoal.FromInvestmentGoal(dbInvestmentGoal);

            (_, var totals) = CalculateBalanceOverTime(investmentGoal);
            return totals;
        }

        public Dictionary<int, double> GetBalanceOverTime(int investmentGoalId)
        {
            var dbInvestmentGoal = _investmentGoalService.GetInvestmentGoal(investmentGoalId);
            if (dbInvestmentGoal is null) { throw new NullReferenceException(); }
            UserInvestmentGoal investmentGoal = UserInvestmentGoal.FromInvestmentGoal(dbInvestmentGoal);
            (var dict, _) = CalculateBalanceOverTime(investmentGoal);
            return dict;
        }

        public UserInvestmentGoal? ModifyUserInvestmentGoal(UserInvestmentGoal investmentGoal)
        {
            var toUpdate = investmentGoal.ToInvestmentGoal();

            var dbInvestmentGoal = _investmentGoalService.UpdateInvestmentGoal(toUpdate);
            if (dbInvestmentGoal == null)
            {
                throw new NullReferenceException();
            }
            return UserInvestmentGoal.FromInvestmentGoal(dbInvestmentGoal);
        }
    }
}
