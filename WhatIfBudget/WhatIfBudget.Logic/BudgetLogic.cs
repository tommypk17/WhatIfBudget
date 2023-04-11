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
using Microsoft.EntityFrameworkCore.ValueGeneration;

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
        private readonly IDebtService _debtService;
        private readonly IDebtGoalDebtService _dgdService;
        private readonly IMortgageGoalService _mortgageGoalService;

        private readonly IIncomeLogic _incomeLogic;
        private readonly IExpenseLogic _expenseLogic;
        private readonly ISavingGoalLogic _savingGoalLogic;
        private readonly IDebtGoalLogic _debtGoalLogic;
        private readonly IMortgageGoalLogic _mortgageGoalLogic;
        private readonly IInvestmentGoalLogic _investmentGoalLogic;

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
                           IDebtService debtService,
                           IDebtGoalDebtService debtGoalDebtService,
                           ISavingGoalService savingGoalService,
                           IIncomeLogic incomeLogic,
                           IExpenseLogic expenseLogic,
                           ISavingGoalLogic savingGoalLogic,
                           IDebtGoalLogic debtGoalLogic,
                           IMortgageGoalLogic mortgageGoalLogic,
                           IInvestmentGoalLogic investmentGoalLogic) { 
            _budgetService = budgetService;
            _incomeService = incomeService;
            _expenseService = expenseService;
            _budgetIncomeService = budgetIncomeService;
            _budgetExpenseService = budgetExpenseService;
            _investmentGoalService = investmentGoalService;
            _investmentService = investmentService;
            _igiService = investmentGoalInvestmentService;
            _savingGoalService = savingGoalService;
            _debtGoalService = debtGoalService;
            _debtService = debtService;
            _dgdService = debtGoalDebtService;
            _mortgageGoalService = mortgageGoalService;
            _incomeLogic = incomeLogic;
            _expenseLogic = expenseLogic;
            _savingGoalLogic = savingGoalLogic;
            _debtGoalLogic = debtGoalLogic;
            _mortgageGoalLogic = mortgageGoalLogic;
            _investmentGoalLogic = investmentGoalLogic;
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
            var dbBudget = _budgetService.GetBudget(budgetId);
            if (dbBudget == null) { throw new NullReferenceException(); }
            else { return UserBudget.FromBudget(dbBudget); }
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

        public double GetCurrentNetWorth(int budgetId)
        {
            var dbBudget = _budgetService.GetBudget(budgetId);
            if (dbBudget == null) { throw new NullReferenceException(); }

            double netWorth = 0.0;

            var dbSG = _savingGoalService.GetSavingGoal(dbBudget.SavingGoalId);
            if (dbSG == null) { throw new NullReferenceException(); }
            netWorth += dbSG.CurrentBalance;

            var dbDebts = _debtService.GetDebtsByDebtGoalId(dbBudget.DebtGoalId);
            foreach( var dbDebt in dbDebts )
            {
                netWorth -= dbDebt.CurrentBalance;
            }

            var dbMG = _mortgageGoalService.GetMortgageGoal(dbBudget.MortgageGoalId);
            if (dbMG == null) { throw new NullReferenceException(); }
            netWorth += dbMG.EstimatedCurrentValue;
            netWorth -= dbMG.TotalBalance;

            var dbInvestments = _investmentService.GetInvestmentsByInvestmentGoalId(dbBudget.InvestmentGoalId);
            foreach( var investment in dbInvestments )
            {
                netWorth += investment.CurrentBalance;
            }

            return netWorth;
        }

        public NetWorthTotals GetNetWorthOverTime(int budgetId)
        {
            var dbBudget = _budgetService.GetBudget(budgetId);
            if (dbBudget == null) { throw new NullReferenceException(); }

            var nwTotals = new NetWorthTotals();
            nwTotals.SavingGoalMonth = _savingGoalLogic.GetSavingTotals(dbBudget.SavingGoalId).MonthsToTarget;
            nwTotals.DebtGoalMonth = _debtGoalLogic.GetDebtTotals(dbBudget.DebtGoalId).MonthsToPayoff;
            nwTotals.MortgageGoalMonth = _mortgageGoalLogic.GetMortgageTotals(dbBudget.MortgageGoalId).MonthsToPayoff;
            nwTotals.Balance = new Dictionary<int, double>();

            var savingBalance = _savingGoalLogic.GetBalanceOverTime(dbBudget.SavingGoalId);
            var debtBalance = _debtGoalLogic.GetBalanceOverTime(dbBudget.DebtGoalId);
            var mortBalance = _mortgageGoalLogic.GetNetValueOverTime(dbBudget.MortgageGoalId);
            var investBalance = _investmentGoalLogic.GetBalanceOverTime(dbBudget.InvestmentGoalId);
 
            foreach (int iMonth in investBalance.Keys.ToList())
            {
                var netWorth = 0.0;

                if (savingBalance.ContainsKey(iMonth))
                {
                    netWorth += savingBalance[iMonth];
                }
                else
                {
                    netWorth += savingBalance.Values.Last();
                }

                if (debtBalance.ContainsKey(iMonth))
                {
                    netWorth += debtBalance[iMonth];
                }
                else
                {
                    netWorth += debtBalance.Values.Last();
                }

                if (mortBalance.ContainsKey(iMonth))
                {
                    netWorth += mortBalance[iMonth];
                }
                else
                {
                    netWorth += mortBalance.Values.Last();
                }

                netWorth += investBalance[iMonth];

                nwTotals.Balance[iMonth] = netWorth;
            }

            return nwTotals;
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

        public UserBudget? DeleteUserBudget(int budgetId)
        {
            var budget = _budgetService.GetBudget(budgetId);
            if (budget == null) { throw new NullReferenceException(); }

            var dbBudget = _budgetService.DeleteBudget(budget.Id);
            if (dbBudget == null) { throw new NullReferenceException(); }

            // Delete associated debts
            var allDGD_List = _dgdService.GetAllDebtGoalDebts()
                .ToList();
            var budgetDGD_List = allDGD_List.Where(x => x.DebtGoalId == budget.DebtGoalId).ToList();
            foreach (var dgd in budgetDGD_List)
            {
                var dbDGD = _dgdService.DeleteDebtGoalDebt(dgd.Id);
                if (dbDGD == null) { throw new NullReferenceException(); }
                allDGD_List.Remove(dgd);
                // Delete debt element if it has no remaining associations
                if (!allDGD_List.Where(x => x.DebtId == dgd.DebtGoalId)
                                .Any())
                {
                    var dbDebt = _debtService.DeleteDebt(dgd.DebtId);
                    if (dbDebt == null) { throw new NullReferenceException(); }
                }
            }

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

            var dbDG = _debtGoalService.DeleteDebtGoal(budget.DebtGoalId);
            if (dbSG == null) { throw new NullReferenceException(); }

            var dbMG = _mortgageGoalService.DeleteMortgageGoal(budget.MortgageGoalId);
            if (dbSG == null) { throw new NullReferenceException(); }


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

            return UserBudget.FromBudget(dbBudget);
        }

        public UserBudgetAllocations? GetUserBudgetAllocations(int budgetId)
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

        public UserBudgetAllocations? UpdateUserBudgetAllocations(int budgetId, UserBudgetAllocations budgetAllocations)
        {
            var budget = _budgetService.GetBudget(budgetId);
            if (budget == null) { throw new NullReferenceException(); }

            var debtGoal = _debtGoalService.GetDebtGoal(budget.DebtGoalId);
            if (debtGoal is not null)
            {
                debtGoal.AdditionalBudgetAllocation = budgetAllocations.DebtGoal;
                _debtGoalService.UpdateDebtGoal(debtGoal);
            }

            var mortgageGoal = _mortgageGoalService.GetMortgageGoal(budget.MortgageGoalId);
            if (mortgageGoal is not null)
            {
                mortgageGoal.AdditionalBudgetAllocation = budgetAllocations.MortgageGoal;
                _mortgageGoalService.ModifyMortgageGoal(mortgageGoal);
            }

            var savingGoal = _savingGoalService.GetSavingGoal(budget.SavingGoalId);
            if (savingGoal is not null)
            {
                savingGoal.AdditionalBudgetAllocation = budgetAllocations.SavingGoal;
                _savingGoalService.ModifySavingGoal(savingGoal);
            }

            var investmentGoal = _investmentGoalService.GetInvestmentGoal(budget.InvestmentGoalId);
            if (investmentGoal is not null)
            {
                investmentGoal.AdditionalBudgetAllocation = budgetAllocations.InvestmentGoal;
                _investmentGoalService.UpdateInvestmentGoal(investmentGoal);
            }

            return GetUserBudgetAllocations(budgetId);
        }

        public double GetBudgetAvailableFreeCash(int budgetId)
        {
            var res = 0.0;
            var expenses = _expenseService.GetExpensesByBudgetId(budgetId);
            var incomes = _incomeService.GetIncomesByBudgetId(budgetId);

            res = incomes.Select(x => x.Amount).Sum() - expenses.Select(x => x.Amount).Sum();

            return res;
        }
    }
}
