using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Logic.Models;
using WhatIfBudget.Logic.Utilities;
using WhatIfBudget.Services.Interfaces;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Logic;
using System.Dynamic;
using System.Globalization;
using System.Security.Cryptography;

namespace WhatIfBudget.Logic
{
    public class InvestmentGoalLogic : IInvestmentGoalLogic
    {
        private readonly IInvestmentGoalService _investmentGoalService;
        private readonly IInvestmentService _investmentService;
        private readonly ISavingGoalService _savingGoalService;
        private readonly IDebtGoalService _debtGoalService;
        private readonly IDebtService _debtService;
        private readonly IMortgageGoalService _mortgageGoalService;
        private readonly ISavingGoalLogic _savingGoalLogic;
        private readonly IDebtGoalLogic _debtGoalLogic;
        private readonly IMortgageGoalLogic _mortgageGoalLogic;
        public InvestmentGoalLogic(IInvestmentGoalService investmentGoalService,
                                   IInvestmentService investmentService,
                                   ISavingGoalService savingGoalService,
                                   IDebtGoalService debtGoalService,
                                   IDebtService debtService,
                                   IMortgageGoalService mortgageGoalService,
                                   ISavingGoalLogic savingGoalLogic,
                                   IDebtGoalLogic debtGoalLogic,
                                   IMortgageGoalLogic mortgageGoalLogic) { 
            _investmentGoalService = investmentGoalService;
            _investmentService = investmentService;
            _savingGoalService = savingGoalService;
            _debtGoalService = debtGoalService;
            _debtService = debtService;
            _mortgageGoalService = mortgageGoalService;
            _savingGoalLogic = savingGoalLogic;
            _debtGoalLogic = debtGoalLogic;
            _mortgageGoalLogic = mortgageGoalLogic;
        }

        private List<double> GetCompletedGoalContributions(InvestmentGoal investmentGoal)
        {
            if (!investmentGoal.RolloverCompletedGoals)
            {
                // Return a dict of correct size with all 0
                return Enumerable.Repeat(0.0, investmentGoal.YearsToTarget * 12).ToList();
            }

            var rolloverList = Enumerable.Repeat(0.0, investmentGoal.YearsToTarget * 12).ToList();

            var budget = investmentGoal.Budget;
            if (budget is null) { throw new NullReferenceException(); }
            var dbSG = _savingGoalService.GetSavingGoal(budget.SavingGoalId);
            if (dbSG is null) { throw new NullReferenceException(); }
            var dbDG = _debtGoalService.GetDebtGoal(budget.DebtGoalId);
            if (dbDG is null) { throw new NullReferenceException(); }
            var dbMG = _mortgageGoalService.GetMortgageGoal(budget.MortgageGoalId);
            if (dbMG is null) { throw new NullReferenceException(); }

            var savingMonth = _savingGoalLogic.GetSavingTotals(budget.SavingGoalId).MonthsToTarget;
            var savingAllocation = dbSG.AdditionalBudgetAllocation;
            var debtMonth = _debtGoalLogic.GetDebtTotals(budget.DebtGoalId).MonthsToPayoff;
            var debtAllocation = dbDG.AdditionalBudgetAllocation;
            var debtPayments = _debtService.GetDebtsByDebtGoalId(budget.DebtGoalId).Select(x => x.MinimumPayment).Sum();
            var mortgageMonth = _mortgageGoalLogic.GetMortgageTotals(budget.MortgageGoalId).MonthsToPayoff;
            var mortgageAllocation = dbMG.AdditionalBudgetAllocation;
            var mortgagePayment = dbMG.MonthlyPayment;

            for (int iMonth = 0; iMonth < investmentGoal.YearsToTarget * 12; iMonth++)
            {
                // Saving
                if (iMonth > savingMonth) { rolloverList[iMonth] += savingAllocation; }

                // Debt
                if (iMonth > debtMonth)
                {
                    rolloverList[iMonth] += debtAllocation;
                    rolloverList[iMonth] += debtPayments;
                }

                // Mortgage
                if (iMonth > mortgageMonth)
                {
                    rolloverList[iMonth] += mortgageAllocation;
                    rolloverList[iMonth] += mortgagePayment;
                }
            }

            return rolloverList;
        }

        private (Dictionary<int, double>, InvestmentGoalTotals) CalculateInvestmentsOverTime(InvestmentGoal investmentGoal)
        {
            var investmentList = _investmentService.GetInvestmentsByInvestmentGoalId(investmentGoal.Id);
            var startingBalance = investmentList.Select(x => x.CurrentBalance).Sum();

            var investmentStepper = new BalanceStepUtility(startingBalance, investmentGoal.AnnualReturnRate_Percent / 12);
            var contributionStepper = new BalanceStepUtility(0, investmentGoal.AnnualReturnRate_Percent / 12);

            // Return Values
            var balanceDict = new Dictionary<int, double>();
            var investmentGoalTotals = new InvestmentGoalTotals();

            var baseContribution = investmentGoal.AdditionalBudgetAllocation;
            baseContribution += investmentList.Select(x => x.MonthlyPersonalContribution).Sum();
            baseContribution += investmentList.Select(x => x.MonthlyEmployerContribution).Sum();
            var rolloverContribution = GetCompletedGoalContributions(investmentGoal);

            while (investmentStepper.NumberOfSteps <= investmentGoal.YearsToTarget * 12)
            {
                balanceDict[investmentStepper.NumberOfSteps] = investmentStepper.Balance;
                var iContribution = baseContribution + rolloverContribution[investmentStepper.NumberOfSteps];
                _ = investmentStepper.Step(iContribution);
                _ = contributionStepper.Step(investmentGoal.AdditionalBudgetAllocation);
            }

            investmentGoalTotals.BalanceAtTarget = investmentStepper.Balance;
            investmentGoalTotals.TotalInterestAccrued = investmentStepper.InterestAccumulated;
            investmentGoalTotals.AddedDueToContribution = Math.Round(investmentGoalTotals.BalanceAtTarget - contributionStepper.Balance,2);
            return (balanceDict, investmentGoalTotals);
        }

        public UserInvestmentGoal? GetInvestmentGoal(int investmentGoalId)
        {
            var investmentGoal = _investmentGoalService.GetInvestmentGoal(investmentGoalId);
            if(investmentGoal is null) return null;
            return UserInvestmentGoal.FromInvestmentGoal(investmentGoal);
        }

        public InvestmentGoalTotals GetInvestmentTotals(int investmentGoalId)
        {
            var dbInvestmentGoal = _investmentGoalService.GetInvestmentGoal(investmentGoalId);
            if (dbInvestmentGoal is null) { throw new NullReferenceException(); }

            (_, var totals) = CalculateInvestmentsOverTime(dbInvestmentGoal);
            return totals;
        }

        public Dictionary<int, double> GetBalanceOverTime(int investmentGoalId)
        {
            var dbInvestmentGoal = _investmentGoalService.GetInvestmentGoal(investmentGoalId);
            if (dbInvestmentGoal is null) { throw new NullReferenceException(); }
            (var dict, _) = CalculateInvestmentsOverTime(dbInvestmentGoal);
            return dict;
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
