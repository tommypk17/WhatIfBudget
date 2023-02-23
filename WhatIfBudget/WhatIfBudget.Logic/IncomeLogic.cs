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
        private readonly IBudgetService _budgetService;

        public IncomeLogic(IIncomeService incomeService,
                           IBudgetIncomeService budgetIncomeService,
                            IBudgetService budgetService) { 
            _incomeService = incomeService;
            _budgetIncomeService= budgetIncomeService;
            _budgetService = budgetService;
        }

        public IList<UserIncome> GetUserIncomes(Guid userId)
        {
            return _incomeService.GetAllIncomes().Where(x => x.UserId == userId)
                                                .Select(x => UserIncome.FromIncome(x, 0)) // TODO: Return with budgetId 0?
                                                .ToList();
        }
        public IList<UserIncome> GetBudgetIncomes(int budgetId)
        {
            var budgetIncomeIdList = _budgetIncomeService.GetAllBudgetIncomes()
                .Where(x => x.BudgetId == budgetId)
                .Select(x => x.IncomeId)
                .ToList();

            return _incomeService.GetAllIncomes().Where(x => budgetIncomeIdList.Contains(x.Id))
                                                .Select(x => UserIncome.FromIncome(x, budgetId))
                                                .ToList();
        }
        public UserIncome? AddUserIncome(Guid userId, UserIncome income)
        {
            if (!_budgetService.Exists(income.BudgetId)) return null;
            // Create income element if it does not yet exist
            var dbIncome = _incomeService.GetAllIncomes()
                    .Where(x => x.Id == income.Id)
                    .FirstOrDefault();
            if (dbIncome == null)
            {
                dbIncome = _incomeService.AddNewIncome(income.ToIncome(userId));
                if (dbIncome == null)
                {
                    throw new NullReferenceException();
                }
            }
            // Associate income element to current budget
            var budgetIncomeToCreate = new BudgetIncome
            {
                BudgetId = income.BudgetId,
                IncomeId = income.Id
            };

            var dbBudgetIncome = _budgetIncomeService.AddNewBudgetIncome(budgetIncomeToCreate);
            if (dbBudgetIncome== null)
            {
                throw new NullReferenceException();
            }
            return UserIncome.FromIncome(dbIncome, income.BudgetId);
        }

        public UserIncome? ModifyUserIncome(Guid userId, UserIncome income)
        {
            var toUpdate = income.ToIncome(userId);

            var dbIncome = _incomeService.UpdateIncome(toUpdate);
            if (dbIncome == null)
            {
                throw new NullReferenceException();
            }
            return UserIncome.FromIncome(dbIncome, income.BudgetId);
        }

        public UserIncome? DeleteBudgetIncome(int incomeId, int budgetId)
        {
            // De-associate income element from current budget
            var idToDelete = _budgetIncomeService.GetAllBudgetIncomes()
                    .Where(x => x.IncomeId == incomeId && x.BudgetId == budgetId)
                    .Select(x => x.Id)
                    .FirstOrDefault();
            var dbBudgetIncome = _budgetIncomeService.DeleteBudgetIncome(idToDelete);
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
                return UserIncome.FromIncome(dbDeleteIncome, budgetId);
            }
            else
            {
                // Keep income element in database for use by other budgets
                return UserIncome.FromIncome(_incomeService.GetAllIncomes()
                    .Where(x => x.Id == incomeId)
                    .FirstOrDefault(), budgetId);
            }
        }
    }
}
