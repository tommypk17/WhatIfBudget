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
        private readonly IInvestmentGoalInvestmentService _investmentGoalInvestmentService;
        public InvestmentGoalLogic(IInvestmentGoalService investmentGoalService, IInvestmentService investmentService, IInvestmentGoalInvestmentService investmentGoalInvestmentService) { 
            _investmentGoalService = investmentGoalService;
            _investmentService = investmentService;
            _investmentGoalInvestmentService = investmentGoalInvestmentService;
        }

        private Dictionary<int, double> GetTotalMonthlyContributions(UserInvestmentGoal investmentGoal, int yearsMax)
        {
            var investmentIdList = _investmentGoalInvestmentService.GetAllInvestmentGoalInvestments()
                .Where(x => x.InvestmentGoalId == investmentGoal.Id)
                .Select(x => x.InvestmentId)
                .ToList();
            var investmentList = _investmentService.GetAllInvestments()
                .Where(x => investmentIdList.Contains(x.Id))
                .Select(x => UserInvestment.FromInvestment(x))
                .ToList();

            double currentTotal = investmentGoal.additionalBudgetAllocation;
            foreach (var inv in investmentList)
            {
                currentTotal += inv.MonthlyPersonalContribution;
                currentTotal += inv.MonthlyEmployerContribution;
            }

            Dictionary<int, double> contributionDict = new Dictionary<int, double>();
            var iTotal = currentTotal;
            for (int i = 0; i < yearsMax; i++)
            {
                // TODO: Update iTotal when other goals are accomplished if we are rolling them in
                //if (i == savingGoal.YearsToReach)
                //{
                //    iTotal += SavingGoal.AdditionalBudgetAllocation;
                //}
                contributionDict[i] = iTotal;
            }

            return contributionDict;
        }

        private Dictionary<int, double> GetBalanceOverTime(UserInvestmentGoal investmentGoal)
        {
            int iMax = investmentGoal.YearsToTarget + 10;
            Dictionary<int, double> contributionList = GetTotalMonthlyContributions(investmentGoal, iMax);


            Dictionary<int, double> balanceDict = new Dictionary<int, double>();
            double iBalance = investmentGoal.TotalBalance;
            balanceDict[0] =iBalance;

            for (int i = 1; i < iMax; i++)
            {
                iBalance += contributionList[i - 1];
                iBalance = Math.Pow(iBalance, 1.0 + investmentGoal.AnnualReturnRate_Percent / 100.0);
                balanceDict[i] =iBalance;
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
            if (dbInvestmentGoal is null) { throw new NullReferenceException(); }
            UserInvestmentGoal investmentGoal = UserInvestmentGoal.FromInvestmentGoal(dbInvestmentGoal);

            var balanceDict = GetBalanceOverTime(investmentGoal);
            return balanceDict[investmentGoal.YearsToTarget];
        }

        public Dictionary<int, double> GetBalanceOverTime(int investmentGoalId)
        {
            var dbInvestmentGoal = _investmentGoalService.GetInvestmentGoal(investmentGoalId);
            if (dbInvestmentGoal is null) { throw new NullReferenceException(); }
            UserInvestmentGoal investmentGoal = UserInvestmentGoal.FromInvestmentGoal(dbInvestmentGoal);

            return GetBalanceOverTime(investmentGoal);
        }

        public UserInvestmentGoal? AddUserInvestmentGoal(Guid userId, UserInvestmentGoal investmentGoal)
        {
            var toCreate = investmentGoal.ToInvestmentGoal();

            var dbInvestmentGoal = _investmentGoalService.AddInvestmentGoal(toCreate);
            if (dbInvestmentGoal == null)
            {
                throw new NullReferenceException();
            }

            return UserInvestmentGoal.FromInvestmentGoal(dbInvestmentGoal);
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
