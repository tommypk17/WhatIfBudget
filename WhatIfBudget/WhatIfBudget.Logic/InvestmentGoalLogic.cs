using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Logic.Models;
using WhatIfBudget.Services.Interfaces;
using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Logic
{
    public class InvestmentGoalLogic : IInvestmentGoalLogic
    {
        private readonly IInvestmentGoalService _investmentGoalService;
        private readonly IBudgetService _budgetService;
        public InvestmentGoalLogic(IInvestmentGoalService investmentGoalService, IBudgetService budgetService) { 
            _investmentGoalService = investmentGoalService;
            _budgetService = budgetService;
        }

        public UserInvestmentGoal? GetBudgetInvestmentGoal(int budgetId)
        {
            // Get the investment goal associated with this budget
            var dbBudget = _budgetService.GetBudget(budgetId);
            if (dbBudget is null) return null;
            
            var investmentGoal = _investmentGoalService.GetInvestmentGoal(dbBudget.InvestmentGoalId);
            if(investmentGoal is null) return null;

            return UserInvestmentGoal.FromInvestmentGoal(investmentGoal);
        }

        public UserInvestmentGoal? GetBudgetInvestmentGoal(UserBudget budget)
        {
            // Get the investment goal associated with this budget
            var investmentGoal = _investmentGoalService.GetInvestmentGoal(budget.InvestmentGoalId);
            if (investmentGoal is null) return null;
            return UserInvestmentGoal.FromInvestmentGoal(investmentGoal);
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
