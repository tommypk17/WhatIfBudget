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
    public class IncomeLogic : IIncomeLogic
    {
        private readonly IIncomeService _incomeService;
        private readonly IBudgetIncomeService _budgetIncomeService;
        public IncomeLogic(IIncomeService incomeService, IBudgetIncomeService budgetIncomeService) { 
            _incomeService = incomeService;
            _budgetIncomeService = budgetIncomeService;
        }

        public IList<UserIncome> GetUserIncomes(Guid userId)
        {
            return _incomeService.GetAllIncomes().Where(x => x.UserId == userId)
                                                .Select(x => UserIncome.FromIncome(x))
                                                .ToList();
        }
        public IList<UserIncome> GetBudgetIncomes(int budgetId)
        {
            var budgetIncomeIdList = _budgetIncomeService.GetAllBudgetIncomes()
                .Where(x => x.BudgetId == budgetId)
                .Select(x => x.IncomeId)
                .ToList();

            return _incomeService.GetAllIncomes().Where(x => budgetIncomeIdList.Contains(x.Id))
                                                .Select(x => UserIncome.FromIncome(x))
                                                .ToList();
        }
        public UserIncome AddUserIncome(Guid userId, UserIncome income, int budgetId)
        {
            // Associate income element to current budget
            var budgetIncomeToCreate = new BudgetIncome { Id = 1, BudgetId = budgetId, IncomeId = income.Id };
            var dbBudgetIncome = _budgetIncomeService.AddNewBudgetIncome(budgetIncomeToCreate);
            if (dbBudgetIncome== null)
            {
                throw new NullReferenceException();
            }

            // Create income element if it does not yet exist
            var dbIncome = _incomeService.GetAllIncomes()
                    .Where(x => x.Id == income.Id)
                    .FirstOrDefault();
            if (dbIncome == null)
            {
                var dbNewIncome = _incomeService.AddNewIncome(income.ToIncome(userId));
                if (dbNewIncome == null)
                {
                    throw new NullReferenceException();
                }
                return UserIncome.FromIncome(dbNewIncome);
            }
            else
            {
                return UserIncome.FromIncome(dbIncome);
            }
        }

        public UserIncome ModifyUserIncome(Guid userId, UserIncome income)
        {
            var toUpdate = income.ToIncome(userId);

            var dbIncome = _incomeService.UpdateIncome(toUpdate);
            if (dbIncome == null)
            {
                throw new NullReferenceException();
            }
            return UserIncome.FromIncome(dbIncome);
        }

        public UserIncome DeleteUserIncome(Guid userId, int incomeId, int budgetId)
        {
            // De-associate income element from current budget
            var budgetIncomeToDelete = new BudgetIncome { Id = 1 /*TODO*/, BudgetId = budgetId, IncomeId = incomeId };
            var dbBudgetIncome = _budgetIncomeService.DeleteBudgetIncome(budgetIncomeToDelete.Id);
            if (dbBudgetIncome == null)
            {
                throw new NullReferenceException();
            }
            // Delete income element if it is not associated with any other budget Id
            if (!_budgetIncomeService.GetAllBudgetIncomes()
                    .Where(x => x.IncomeId == incomeId)
                    .Any())
            {
                var dbDeleteIncome = _incomeService.DeleteIncome(dbBudgetIncome.IncomeId);
                if (dbDeleteIncome == null)
                {
                    throw new NullReferenceException();
                }
                return UserIncome.FromIncome(dbDeleteIncome);
            }
            else
            {
                // Keep income element in database for use by other budgets
                return UserIncome.FromIncome(_incomeService.GetAllIncomes()
                    .Where(x => x.Id == incomeId)
                    .FirstOrDefault());
            }
        }
    }
}
