using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Logic.Models;
using WhatIfBudget.Services.Interfaces;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Services;

namespace WhatIfBudget.Logic
{
    public class BudgetLogic : IBudgetLogic
    {
        private readonly IBudgetService _budgetService;
        private readonly IIncomeService _incomeService;
        private readonly IBudgetIncomeService _budgetIncomeService;
        /*
        private readonly ISavingGoalService _savingGoalService;
        private readonly IDebtGoalService _debtGoalService;
        private readonly IMortgageGoalService _mortgageGoalService;
        private readonly IInvestmentGoalService _investmentGoalService;
        */
        public BudgetLogic(IBudgetService budgetService, IIncomeService incomeService, IBudgetIncomeService budgetIncomeService /* HERE */) { 
            _budgetService = budgetService;
            _incomeService = incomeService;
            _budgetIncomeService= budgetIncomeService;
            /*
            _savingGoalService = savingGoalService;
            _debtGoalService = debtGoalService;
            _mortgageGoalService = mortgageGoalService;
            _investmentGoalService = investmentGoalService;
            */
        }

        public IList<UserBudget> GetUserBudgets(Guid userId)
        {
            return _budgetService.GetAllBudgets()
                                                .Where(x => x.UserId == userId)
                                                .Select(x => UserBudget.FromBudget(x))
                                                .ToList();
        }
        public UserBudget AddUserBudget(Guid userId, UserBudget budget)
        {
            // Create goals for this budget
            /*
            dbSavingGoal = _savingGoalService.AddNewSavingGoal(UserSavingGoal.FromId(budget.SavingGoalId).ToSavingGoal());
            dbDebtGoal = _debtGoalService.AddNewDebtGoal(UserDebtGoal.FromId(budget.DebtGoalId).ToDebtGoal());
            dbMortgageGoal = _mortgageGoalService.AddNewMortageGoal(UserMortageGoal.FromId(budget.MortageGoalId).ToMortageGoal());
            dbInvestmentGoal = _investmentGoalService.AddNewInvestmentGoal(UserInvestmentGoal.FromId(budget.InvestmentGoalId).ToInvestmentGoal());
            */

            var toCreate = budget.ToBudget(userId);

            var dbBudget = _budgetService.AddNewBudget(toCreate);
            if (dbBudget == null)
            {
                throw new NullReferenceException();
            }
            return UserBudget.FromBudget(dbBudget);
        }

        public UserBudget ModifyUserBudget(Guid userId, UserBudget budget)
        {
            var toUpdate = budget.ToBudget(userId);

            var dbBudget = _budgetService.UpdateBudget(toUpdate);
            if (dbBudget == null)
            {
                throw new NullReferenceException();
            }
            return UserBudget.FromBudget(dbBudget);
        }

        public UserBudget DeleteUserBudget(int id)
        {
            // Delete associated goals
            /*
            dbSavingGoal = _savingGoalService.DeleteSavingGoal(budget.SavingGoalId);
            dbDebtGoal = _debtGoalService.DeleteDebtGoal(budget.DebtGoalId);
            dbMortgageGoal = _mortgageGoalService.DeleteMortageGoal(budget.MortageGoalId);
            dbInvestmentGoal = _investmentGoalService.DeleteInvestmentGoal(budget.InvestmentGoalId);
            */
            // Delete relations of deleted goals (From the relation tables and the debts/investments tables)

            // Delete associated incomes
            var budgetIncomeList = _budgetIncomeService.GetAllBudgetIncomes()
                .Where(x => x.BudgetId == id)
                .ToList();
            foreach (var budgetIncome in budgetIncomeList)
            {
                var dbBudgetIncome = _budgetIncomeService.DeleteBudgetIncome(budgetIncome.Id);
                if (dbBudgetIncome == null)
                {
                    throw new NullReferenceException();
                }
                // Delete income element that is not associated with any other budget Id
                if (!_budgetIncomeService.GetAllBudgetIncomes()
                        .Where(x => x.IncomeId == dbBudgetIncome.IncomeId)
                        .Any())
                {
                    var dbIncome = _incomeService.DeleteIncome(dbBudgetIncome.IncomeId);
                    if (dbIncome == null)
                    {
                        throw new NullReferenceException();
                    }
                }
            }


            var dbBudget = _budgetService.DeleteBudget(id);
            if (dbBudget == null)
            {
                throw new NullReferenceException();
            }
            return UserBudget.FromBudget(dbBudget);
        }
    }
}
