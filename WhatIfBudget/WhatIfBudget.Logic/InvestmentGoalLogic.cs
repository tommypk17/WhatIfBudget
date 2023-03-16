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

        private Dictionary<int, double> CalculateBalanceOverTime(UserInvestmentGoal investmentGoal)
        {
            int iMonth = 0;
            double iBalance = investmentGoal.TotalBalance;
            Dictionary<int, double> balanceDict = new Dictionary<int, double>();
            double monthlyInterestRate = investmentGoal.AnnualReturnRate_Percent / 1200;

            var investmentList = _investmentService.GetInvestmentsByInvestmentGoalId(investmentGoal.Id);
            double baseContribution = investmentGoal.AdditionalBudgetAllocation;
            foreach (var inv in investmentList)
            {
                baseContribution += inv.MonthlyPersonalContribution;
                baseContribution += inv.MonthlyEmployerContribution;
            }

            while (iMonth <= investmentGoal.YearsToTarget * 12)
            {
                if (iMonth % 12 == 0)
                {
                    balanceDict[iMonth / 12] = iBalance;
                }
                var iContribution = baseContribution; // + GetCompletedGoalContributions(investmentGoal, iMonth);
                // new balance = ( previous balance + ( total contributions * months ) ) * ( 1 + interest rate )
                iBalance = Math.Round((iBalance + iContribution) * (1 + monthlyInterestRate), 2);
                iMonth++;
            }

            return balanceDict;
        }

        public UserInvestmentGoal? GetInvestmentGoal(int investmentGoalId)
        {
            var investmentGoal = _investmentGoalService.GetInvestmentGoal(investmentGoalId);
            var investments = _investmentService.GetInvestmentsByInvestmentGoalId(investmentGoalId);
            if(investmentGoal is null) return null;

            var res = UserInvestmentGoal.FromInvestmentGoal(investmentGoal);
            res.TotalBalance = investments.Select(x => x.CurrentBalance).Sum();

            return res;
        }

        public double GetBalanceAtTarget(int investmentGoalId)
        {
            var dbInvestmentGoal = _investmentGoalService.GetInvestmentGoal(investmentGoalId);
            var investments = _investmentService.GetInvestmentsByInvestmentGoalId(investmentGoalId);
            if (dbInvestmentGoal is null) { throw new NullReferenceException(); }
            UserInvestmentGoal investmentGoal = UserInvestmentGoal.FromInvestmentGoal(dbInvestmentGoal);
            investmentGoal.TotalBalance = investments.Select(x => x.CurrentBalance).Sum();

            var balanceDict = CalculateBalanceOverTime(investmentGoal);
            return balanceDict[investmentGoal.YearsToTarget];
        }

        public Dictionary<int, double> GetBalanceOverTime(int investmentGoalId)
        {
            var dbInvestmentGoal = _investmentGoalService.GetInvestmentGoal(investmentGoalId);
            var investments = _investmentService.GetInvestmentsByInvestmentGoalId(investmentGoalId);
            if (dbInvestmentGoal is null) { throw new NullReferenceException(); }
            UserInvestmentGoal investmentGoal = UserInvestmentGoal.FromInvestmentGoal(dbInvestmentGoal);
            investmentGoal.TotalBalance = investments.Select(x => x.CurrentBalance).Sum();

            return CalculateBalanceOverTime(investmentGoal);
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
