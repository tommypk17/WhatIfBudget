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

        private (Dictionary<int, double>, InvestmentGoalTotals) CalculateInvestmentsOverTime(UserInvestmentGoal investmentGoal)
        {
            var investmentList = _investmentService.GetInvestmentsByInvestmentGoalId(investmentGoal.Id);
            var startingBalance = investmentList.Select(x => x.CurrentBalance).Sum();

            var investmentStepper = new BalanceStepUtility(startingBalance, investmentGoal.AnnualReturnRate_Percent / 12);
            var contributionStepper = new BalanceStepUtility(0, investmentGoal.AnnualReturnRate_Percent / 12);

            // Return Values
            var balanceDict = new Dictionary<int, double>();
            var investmentGoalTotals = new InvestmentGoalTotals();

            var baseContribution = investmentGoal.AdditionalBudgetAllocation;
            baseContribution += investmentList.Select(x => x.MonthlyPersonalContribution).Sum();
            baseContribution += investmentList.Select(x => x.MonthlyEmployerContribution).Sum();

            while (investmentStepper.StepsCompleted() < investmentGoal.YearsToTarget * 12)
            {
                if (investmentStepper.StepsCompleted() % 12 == 0)
                {
                    balanceDict[investmentStepper.StepsCompleted() / 12] = investmentStepper.GetBalance();
                }
                var iContribution = baseContribution; // + GetCompletedGoalContributions(investmentGoal, iMonth);
                _ = investmentStepper.Step(iContribution);
                _ = contributionStepper.Step(investmentGoal.AdditionalBudgetAllocation);
            }
            // Populate the last dict entry
            balanceDict[investmentStepper.StepsCompleted() / 12] = investmentStepper.GetBalance();

            investmentGoalTotals.BalanceAtTarget = investmentStepper.GetBalance();
            investmentGoalTotals.TotalInterestAccrued = investmentStepper.GetAccumulatedInterest();
            investmentGoalTotals.AddedDueToContribution = Math.Round(investmentGoalTotals.BalanceAtTarget - contributionStepper.GetBalance(),2);
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

            (_, var totals) = CalculateInvestmentsOverTime(investmentGoal);
            return totals;
        }

        public Dictionary<int, double> GetBalanceOverTime(int investmentGoalId)
        {
            var dbInvestmentGoal = _investmentGoalService.GetInvestmentGoal(investmentGoalId);
            if (dbInvestmentGoal is null) { throw new NullReferenceException(); }
            UserInvestmentGoal investmentGoal = UserInvestmentGoal.FromInvestmentGoal(dbInvestmentGoal);
            (var dict, _) = CalculateInvestmentsOverTime(investmentGoal);
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
