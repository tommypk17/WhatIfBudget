using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Logic.Models;
using WhatIfBudget.Services.Interfaces;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Common.Enumerations;
using WhatIfBudget.Services;
using System.Collections;

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
            return _incomeService.GetIncomesByBudgetId(budgetId)
                .Select(x => UserIncome.FromIncome(x, budgetId))
                .ToList();
        }

        public double GetBudgetMonthlyIncome(int budgetId)
        {
            var incomeList = _incomeService.GetIncomesByBudgetId(budgetId)
                .Select(x => UserIncome.FromIncome(x, budgetId))
                .ToList();

            double monthlyIncome = 0;
            var noneFreqList = incomeList.Where(x => x.Frequency == EFrequency.None).Select(x => x.Amount).ToList();
            var weekFreqList = incomeList.Where(x => x.Frequency == EFrequency.Weekly).Select(x => x.Amount).ToList();
            var monthFreqList = incomeList.Where(x => x.Frequency == EFrequency.Monthly).Select(x => x.Amount).ToList();
            var quartFreqList = incomeList.Where(x => x.Frequency == EFrequency.Quarterly).Select(x => x.Amount).ToList();
            var yearFreqList = incomeList.Where(x => x.Frequency == EFrequency.Yearly).Select(x => x.Amount).ToList();

            monthlyIncome += noneFreqList.Sum();
            monthlyIncome += weekFreqList.Sum() * 4;
            monthlyIncome += monthFreqList.Sum();
            monthlyIncome += quartFreqList.Sum() / 3;
            monthlyIncome += yearFreqList.Sum() / 12;

            return monthlyIncome;
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
                IncomeId = dbIncome.Id
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
            // Get all instances of this income being used
            var biList = _budgetIncomeService.GetAllBudgetIncomes()
                .Where(x => x.IncomeId == incomeId)
                .ToList();
            // Remove this budget income from the database
            var thisBI = biList.Where(x => x.BudgetId == budgetId).FirstOrDefault();
            if (thisBI == null) { throw new NullReferenceException(); }
            var dbBudgetIncome = _budgetIncomeService.DeleteBudgetIncome(thisBI.Id);
            if (dbBudgetIncome == null) { throw new NullReferenceException(); }
            // Delete income element if it is not associated with any other budget Id
            biList.Remove(thisBI);
            if (!biList.Where(x => x.IncomeId == incomeId).Any())
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
                var dbDeleteIncome = _incomeService.GetAllIncomes()
                    .Where(x => x.Id == incomeId)
                    .FirstOrDefault();
                if (dbDeleteIncome == null)
                {
                    throw new NullReferenceException();
                }
                else { return UserIncome.FromIncome(dbDeleteIncome, budgetId); }
            }
        }
    }
}
