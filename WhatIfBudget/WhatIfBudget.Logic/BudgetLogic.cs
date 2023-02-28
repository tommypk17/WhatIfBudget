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
        private readonly IInvestmentService _investmentService;
        private readonly IInvestmentGoalInvestmentService _igiService;
        private readonly ISavingGoalService _savingGoalService;
        private readonly IDebtGoalService _debtGoalService;
        private readonly IMortgageGoalService _mortgageGoalService;
        
        public BudgetLogic(IBudgetService budgetService,
                           IIncomeService incomeService,
                           IExpenseService expenseService,
                           IBudgetIncomeService budgetIncomeService,
                           IBudgetExpenseService budgetExpenseService,
                           IInvestmentGoalService investmentGoalService,
                           IInvestmentService investmentService,
                           IInvestmentGoalInvestmentService investmentGoalInvestmentService,
                           IMortgageGoalService mortgageGoalService,
                           IDebtGoalService debtGoalService,
                           ISavingGoalService savingGoalService) { 
            _budgetService = budgetService;
            _incomeService = incomeService;
            _expenseService = expenseService;
            _budgetIncomeService= budgetIncomeService;
            _budgetExpenseService= budgetExpenseService;
            _investmentGoalService = investmentGoalService;
            _investmentService = investmentService;
            _igiService = investmentGoalInvestmentService;
            _savingGoalService = savingGoalService;
            _debtGoalService = debtGoalService;
            _mortgageGoalService = mortgageGoalService;
        }

        public IList<UserBudget> GetUserBudgets(Guid userId)
        {
            return _budgetService.GetAllBudgets()
                                                .Where(x => x.UserId == userId)
                                                .Select(x => UserBudget.FromBudget(x))
                                                .ToList();
        }

        public UserBudget? GetBudget(int budgetId)
        {
            var dbBudget = _budgetService.GetAllBudgets()
                                                .Where(x => x.Id == budgetId)
                                                .Select(x => UserBudget.FromBudget(x))
                                                .FirstOrDefault();
            if (dbBudget == null) { throw new NullReferenceException(); }
            else { return dbBudget; }
        }

        public double GetSumOfGoalAllocations(int budgetId)
        {
            // TODO: complete this as other goal services are fleshed out.

            var dbInvestmentGoal = _investmentGoalService.GetInvestmentGoal(budgetId);
            if (dbInvestmentGoal == null) { throw new NullReferenceException(); }
            UserInvestmentGoal investmentGoal = UserInvestmentGoal.FromInvestmentGoal(dbInvestmentGoal);

            return investmentGoal.additionalBudgetAllocation /* + savingGoal.additionalBudgetAllocation...*/;
        }

        public UserBudget? CreateUserBudget(Guid userId, UserBudget budget)
        {
            // Create goals for this budget
            var dbSavingGoal = _savingGoalService.AddSavingGoal(new SavingGoal());
            if (dbSavingGoal == null)
            {
                throw new NullReferenceException();
            }

            var dbDebtGoal = _debtGoalService.AddDebtGoal(new DebtGoal());
            if (dbDebtGoal == null)
            {
                throw new NullReferenceException();
            }

            var dbMortgageGoal = _mortgageGoalService.AddMortgageGoal(new MortgageGoal());
            if (dbMortgageGoal == null)
            {
                throw new NullReferenceException();
            }

            var dbInvestmentGoal = _investmentGoalService.AddInvestmentGoal(new InvestmentGoal());
            if (dbInvestmentGoal == null)
            {
                throw new NullReferenceException();
            }

            // Populate user budget attributes
            budget.InvestmentGoalId = dbInvestmentGoal.Id;
            budget.DebtGoalId = dbDebtGoal.Id;
            budget.MortgageGoalId = dbMortgageGoal.Id;
            budget.SavingGoalId = dbSavingGoal.Id;
            var toCreate = budget.ToBudget(userId);

            var dbBudget = _budgetService.AddNewBudget(toCreate);
            if (dbBudget == null) { throw new NullReferenceException(); }
            return UserBudget.FromBudget(dbBudget);
        }

        public UserBudget? ModifyUserBudget(Guid userId, UserBudget budget)
        {
            var toUpdate = budget.ToBudget(userId);

            var dbBudget = _budgetService.UpdateBudget(toUpdate);
            if (dbBudget == null) { throw new NullReferenceException(); }
            return UserBudget.FromBudget(dbBudget);
        }

        public UserBudget? DeleteUserBudget(UserBudget budget)
        {
            // Delete associated debts
            // TODO

            // Delete associated investments
            var igiList = _igiService.GetAllInvestmentGoalInvestments()
                .Where(x => x.InvestmentGoalId == budget.InvestmentGoalId)
                .ToList();
            foreach (var igi in igiList)
            {
                var dbIGI = _igiService.DeleteInvestmentGoalInvestment(igi.Id);
                if (dbIGI == null) { throw new NullReferenceException(); }
                // Delete investment element if it has no remaining associations
                if (!_igiService.GetAllInvestmentGoalInvestments()
                    .Where(x => x.InvestmentId == dbIGI.InvestmentId)
                    .Any())
                {
                    var dbInvestment = _investmentService.DeleteInvestment(igi.InvestmentId);
                    if (dbInvestment == null) { throw new NullReferenceException(); }
                }
            }

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
                if (dbBudgetIncome == null) { throw new NullReferenceException(); }
                // Delete income element if it has no remaining associations
                if (!_budgetIncomeService.GetAllBudgetIncomes()
                        .Where(x => x.IncomeId == dbBudgetIncome.IncomeId)
                        .Any())
                {
                    var dbIncome = _incomeService.DeleteIncome(dbBudgetIncome.IncomeId);
                    if (dbIncome == null) { throw new NullReferenceException(); }
                }
            }

            // Delete associated expenses
            var budgetExpenseList = _budgetExpenseService.GetAllBudgetExpenses()
                .Where(x => x.BudgetId == budget.Id)
                .ToList();
            foreach (var budgetExpense in budgetExpenseList)
            {
                var dbBudgetExpense = _budgetExpenseService.DeleteBudgetExpense(budgetExpense.Id);
                if (dbBudgetExpense == null) { throw new NullReferenceException(); }

                // Delete income element that is not associated with any other budget Id
                if (!_budgetExpenseService.GetAllBudgetExpenses()
                        .Where(x => x.ExpenseId == dbBudgetExpense.ExpenseId)
                        .Any())
                {
                    var dbExpense = _expenseService.DeleteExpense(dbBudgetExpense.ExpenseId);
                    if (dbExpense == null) { throw new NullReferenceException(); }
                }
            }


            var dbBudget = _budgetService.DeleteBudget(budget.Id);
            if (dbBudget == null) { throw new NullReferenceException(); }
            return UserBudget.FromBudget(dbBudget);
        }
    }
}
