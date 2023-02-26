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

        private double GetTotalMonthlyContributions(UserInvestmentGoal investmentGoal)
        {
            return investmentGoal.additionalBudgetAllocation;
        }

        private IList<double> GetBalanceOverTime(UserInvestmentGoal investmentGoal)
        {
            int iMax = investmentGoal.YearsToTarget + 11;
            IList<double> monthlyContribution = new List<double>(iMax);
            for (int i = 0; i < iMax; i++)
            {

            }


            List<double> balanceList = new List<double>(iMax);
            balanceList[0] = investmentGoal.TotalBalance;

            for (int i = 1; i < iMax; i++)
            {

            }

            return balanceList;
        }

        public UserInvestmentGoal? GetInvestmentGoal(int investmentGoalId)
        {
            var investmentGoal = _investmentGoalService.GetInvestmentGoal(investmentGoalId);
            if(investmentGoal is null) return null;

            return UserInvestmentGoal.FromInvestmentGoal(investmentGoal);
        }

        public double GetBalanceAtTarget(int investmentGoalId)
        {
            var dbInvestmentGoal = _investmentGoalService.GetInvestmentGoal(investmentGoalId);
            if (dbInvestmentGoal is null) { throw new NullReferenceException(); }
            UserInvestmentGoal investmentGoal = UserInvestmentGoal.FromInvestmentGoal(dbInvestmentGoal);

            var balanceList = GetBalanceOverTime(investmentGoal);
            return balanceList[investmentGoal.YearsToTarget + 1];
        }

        public IList<double> GetBalanceOverTime(int investmentGoalId)
        {
            return new List<double>(2) { 1.0, 2.0 };
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
