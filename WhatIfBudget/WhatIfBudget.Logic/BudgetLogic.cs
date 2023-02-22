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
        private readonly IExpenseService _expenseService;
        private readonly IBudgetIncomeService _budgetIncomeService;
        private readonly IBudgetExpenseService _budgetExpenseService;
        private readonly IInvestmentGoalService _investmentGoalService;
        /*
        private readonly ISavingGoalService _savingGoalService;
        private readonly IDebtGoalService _debtGoalService;
        private readonly IMortgageGoalService _mortgageGoalService;
        */
        public BudgetLogic(IBudgetService budgetService,
                           IIncomeService incomeService,
                           IExpenseService expenseService,
                           IBudgetIncomeService budgetIncomeService,
                           IBudgetExpenseService budgetExpenseService,
                           IInvestmentGoalService investmentGoalService/* HERE */) { 
            _budgetService = budgetService;
            _incomeService = incomeService;
            _expenseService = expenseService;
            _budgetIncomeService= budgetIncomeService;
            _budgetExpenseService= budgetExpenseService;
            _investmentGoalService = investmentGoalService;
            /*
            _savingGoalService = savingGoalService;
            _debtGoalService = debtGoalService;
            _mortgageGoalService = mortgageGoalService;
            */
        }

        public IList<UserBudget> GetUserBudgets(Guid userId)
        {
            return _budgetService.GetAllBudgets()
                                                .Where(x => x.UserId == userId)
                                                .Select(x => UserBudget.FromBudget(x))
                                                .ToList();
        }
        public UserBudget GetBudget(int budgetId)
        {
            return _budgetService.GetAllBudgets()
                                                .Where(x => x.Id == budgetId)
                                                .Select(x => UserBudget.FromBudget(x))
                                                .FirstOrDefault();
        }
        public UserBudget CreateUserBudget(Guid userId, UserBudget budget)
        {
            // Create goals for this budget
            /*
            dbSavingGoal = _savingGoalService.AddNewSavingGoal(UserSavingGoal.FromId(budget.SavingGoalId).ToSavingGoal());
            dbDebtGoal = _debtGoalService.AddNewDebtGoal(UserDebtGoal.FromId(budget.DebtGoalId).ToDebtGoal());
            dbMortgageGoal = _mortgageGoalService.AddNewMortageGoal(UserMortageGoal.FromId(budget.MortageGoalId).ToMortageGoal());
            */
            var newInvestmentGoal = new InvestmentGoal();
            var dbInvestmentGoal = _investmentGoalService.AddInvestmentGoal(newInvestmentGoal);
            if (dbInvestmentGoal == null)
            {
                throw new NullReferenceException();
            }

            // Populate user budget attributes
            budget.InvestmentGoalId = dbInvestmentGoal.Id;
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

        public UserBudget DeleteUserBudget(UserBudget budget)
        {
            // Delete associated debts & investments

            // Delete associated goals
            var dbInvestmentGoal = _investmentGoalService.DeleteInvestmentGoal(budget.InvestmentGoalId);
            /*
            dbSavingGoal = _savingGoalService.DeleteSavingGoal(budget.SavingGoalId);
            dbDebtGoal = _debtGoalService.DeleteDebtGoal(budget.DebtGoalId);
            dbMortgageGoal = _mortgageGoalService.DeleteMortageGoal(budget.MortageGoalId);
            */

            // Delete associated incomes
            var budgetIncomeList = _budgetIncomeService.GetAllBudgetIncomes()
                .Where(x => x.BudgetId == budget.Id)
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

            // Delete associated expenses
            var budgetExpenseList = _budgetExpenseService.GetAllBudgetExpenses()
                .Where(x => x.BudgetId == budget.Id)
                .ToList();
            foreach (var budgetExpense in budgetExpenseList)
            {
                var dbBudgetExpense = _budgetExpenseService.DeleteBudgetExpense(budgetExpense.Id);
                if (dbBudgetExpense == null)
                {
                    throw new NullReferenceException();
                }
                // Delete income element that is not associated with any other budget Id
                if (!_budgetExpenseService.GetAllBudgetExpenses()
                        .Where(x => x.ExpenseId == dbBudgetExpense.ExpenseId)
                        .Any())
                {
                    var dbExpense = _expenseService.DeleteExpense(dbBudgetExpense.ExpenseId);
                    if (dbExpense == null)
                    {
                        throw new NullReferenceException();
                    }
                }
            }


            var dbBudget = _budgetService.DeleteBudget(budget.Id);
            if (dbBudget == null)
            {
                throw new NullReferenceException();
            }
            return UserBudget.FromBudget(dbBudget);
        }
    }
}
