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

        private readonly IIncomeLogic _incomeLogic;
        private readonly IExpenseLogic _expenseLogic;
        
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
                           ISavingGoalService savingGoalService,
                           IIncomeLogic incomeLogic,
                           IExpenseLogic expenseLogic) { 
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
            _incomeLogic = incomeLogic;
            _expenseLogic = expenseLogic;
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

        public double GetAvailableMonthlyNet(int budgetId)
        {
            // Get net of user-entered incomes and expenses
            var monthlyNet = _incomeLogic.GetBudgetMonthlyIncome(budgetId);
            monthlyNet -= _expenseLogic.GetBudgetMonthlyExpense(budgetId);

            var dbBudget = _budgetService.GetAllBudgets().Where(x => x.Id == budgetId).FirstOrDefault();
            if (dbBudget == null) { throw new NullReferenceException(); }

            // Subtract existing goal allocations
            var dbSavingGoal = _savingGoalService.GetSavingGoal(dbBudget.SavingGoalId);
            if (dbSavingGoal == null) { throw new NullReferenceException(); }
            monthlyNet -= dbSavingGoal.AdditionalBudgetAllocation;

            var dbDebtGoal = _debtGoalService.GetDebtGoal(dbBudget.DebtGoalId);
            monthlyNet -= dbDebtGoal.AdditionalBudgetAllocation;

            var dbMortgageGoal = _mortgageGoalService.GetMortgageGoal(dbBudget.MortgageGoalId);
            monthlyNet -= dbMortgageGoal.AdditionalBudgetAllocation;

            var dbInvestmentGoal = _investmentGoalService.GetInvestmentGoal(dbBudget.InvestmentGoalId);
            if (dbInvestmentGoal == null) { throw new NullReferenceException(); }
            monthlyNet -= dbInvestmentGoal.AdditionalBudgetAllocation;

            return monthlyNet;
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
            var allIGI_List = _igiService.GetAllInvestmentGoalInvestments()
                .ToList();
            var budgetIGI_List = allIGI_List.Where(x => x.InvestmentGoalId == budget.InvestmentGoalId).ToList();
            foreach (var igi in budgetIGI_List)
            {
                var dbIGI = _igiService.DeleteInvestmentGoalInvestment(igi.Id);
                if (dbIGI == null) { throw new NullReferenceException(); }
                allIGI_List.Remove(igi);
                // Delete investment element if it has no remaining associations
                if (!allIGI_List.Where(x => x.InvestmentId == igi.InvestmentId)
                                .Any())
                {
                    var dbInvestment = _investmentService.DeleteInvestment(igi.InvestmentId);
                    if (dbInvestment == null) { throw new NullReferenceException(); }
                }
            }

            // Delete associated goals
            var dbIG = _investmentGoalService.DeleteInvestmentGoal(budget.InvestmentGoalId);
            if (dbIG == null) { throw new NullReferenceException(); }

            var dbSG = _savingGoalService.DeleteSavingGoal(budget.SavingGoalId);
            if (dbSG == null) { throw new NullReferenceException(); }
            /*
            var dbDG = _debtGoalService.DeleteDebtGoal(budget.DebtGoalId);
            var dbMG = _mortgageGoalService.DeleteMortageGoal(budget.MortageGoalId);
            */

            // Delete associated incomes
            var allBI_List = _budgetIncomeService.GetAllBudgetIncomes()
                .ToList();
            var biList = allBI_List.Where(x => x.BudgetId == budget.Id).ToList();
            foreach (var budgetIncome in biList)
            {
                var dbBudgetIncome = _budgetIncomeService.DeleteBudgetIncome(budgetIncome.Id);
                if (dbBudgetIncome == null) { throw new NullReferenceException(); }
                allBI_List.Remove(budgetIncome);
                // Delete income element if it has no remaining associations
                if (allBI_List.Where(x => x.IncomeId == budgetIncome.IncomeId)
                              .Any())
                {
                    var dbIncome = _incomeService.DeleteIncome(dbBudgetIncome.IncomeId);
                    if (dbIncome == null) { throw new NullReferenceException(); }
                }
            }

            // Delete associated expenses
            var allBE_List = _budgetExpenseService.GetAllBudgetExpenses()
                .ToList();
            var beList = allBE_List.Where(x => x.BudgetId == budget.Id).ToList();
            foreach (var budgetExpense in beList)
            {
                var dbBudgetExpense = _budgetExpenseService.DeleteBudgetExpense(budgetExpense.Id);
                if (dbBudgetExpense == null) { throw new NullReferenceException(); }
                allBE_List.Remove(budgetExpense);
                // Delete income element that is not associated with any other budget Id
                if (!allBE_List.Where(x => x.ExpenseId == budgetExpense.ExpenseId)
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

        public UserBudgetAllocations? GetBudgetAllocations(int budgetId)
        {
            var budget = _budgetService.GetBudget(budgetId);
            if(budget == null) { throw new NullReferenceException(); }

            var allocations = new UserBudgetAllocations();
            var debtGoal = _debtGoalService.GetDebtGoal(budget.DebtGoalId);
            if (debtGoal is not null) allocations.DebtGoal = debtGoal.AdditionalBudgetAllocation;
            
            var mortgageGoal = _mortgageGoalService.GetMortgageGoal(budget.MortgageGoalId);
            if (mortgageGoal is not null) allocations.MortgageGoal = mortgageGoal.AdditionalBudgetAllocation;
            
            var savingGoal = _savingGoalService.GetSavingGoal(budget.SavingGoalId);
            if (savingGoal is not null) allocations.SavingGoal = savingGoal.AdditionalBudgetAllocation;
            
            var investmentGoal = _investmentGoalService.GetInvestmentGoal(budget.InvestmentGoalId);
            if (investmentGoal is not null) allocations.InvestmentGoal = investmentGoal.AdditionalBudgetAllocation;

            return allocations;
        }
    }
}
