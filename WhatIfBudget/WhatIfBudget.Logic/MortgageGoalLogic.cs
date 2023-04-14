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
using System.Dynamic;
using System.Runtime.CompilerServices;

namespace WhatIfBudget.Logic
{
    public class MortgageGoalLogic : IMortgageGoalLogic
    {
        private readonly IMortgageGoalService _mortgageGoalService;

        public MortgageGoalLogic(IMortgageGoalService MortgageGoalService) {
            _mortgageGoalService = MortgageGoalService;
        }

        public UserMortgageGoal? GetMortgageGoal(int MortgageGoalId)
        {
            var dbMortgageGoal = _mortgageGoalService.GetMortgageGoal(MortgageGoalId);
            if(dbMortgageGoal is null) return null;

            var res = UserMortgageGoal.FromMortgageGoal(dbMortgageGoal);
            return res;
        }

        public MortgageGoalTotals GetMortgageTotals(int MortgageGoalId)
        {
            var dbMortgageGoal = _mortgageGoalService.GetMortgageGoal(MortgageGoalId);
            if (dbMortgageGoal is null) { throw new NullReferenceException(); }
            var res = UserMortgageGoal.FromMortgageGoal(dbMortgageGoal);

            var balanceStepper = new BalanceStepUtility(res.TotalBalance, res.InterestRate_Percent / 12);

            // Return Values
            var MortgageGoalTotals = new MortgageGoalTotals();

            // Allocation stepper will always equal or lag balance stepper
            balanceStepper.StepToTarget(-1 * (res.MonthlyPayment + res.AdditionalBudgetAllocation), 0.0);

            MortgageGoalTotals.MonthsToPayoff = balanceStepper.NumberOfSteps;
            MortgageGoalTotals.TotalInterestAccrued = balanceStepper.InterestAccumulated;
            MortgageGoalTotals.TotalCostToPayoff = balanceStepper.CumulativeContribution;
            return MortgageGoalTotals;
        }

        public Dictionary<int, double> GetNetValueOverTime(int MortgageGoalId, int totalMonths = 0)
        {
            var dbMortgageGoal = _mortgageGoalService.GetMortgageGoal(MortgageGoalId);
            if (dbMortgageGoal is null) { throw new NullReferenceException(); }
            var res = UserMortgageGoal.FromMortgageGoal(dbMortgageGoal);

            var balanceStepper = new BalanceStepUtility(res.TotalBalance, res.InterestRate_Percent / 12);
            var valueStepper = new BalanceStepUtility(res.EstimatedCurrentValue, 4.0 / 12);

            // Return Values
            var netDict = new Dictionary<int, double> { { 0, valueStepper.Balance - balanceStepper.Balance } };

            while (balanceStepper.Balance > 0.0)
            {
                _ = balanceStepper.Step(-1 * (res.MonthlyPayment + res.AdditionalBudgetAllocation));
                _ = valueStepper.Step(0.0);
                netDict[balanceStepper.NumberOfSteps] = Math.Round(valueStepper.Balance - balanceStepper.Balance, 2);
            }
            // Further dictionary entries are just home value
            netDict[balanceStepper.NumberOfSteps] = valueStepper.Balance;

            while (balanceStepper.NumberOfSteps < totalMonths)
            {
                _ = valueStepper.Step(0.0);
                netDict[balanceStepper.NumberOfSteps] = balanceStepper.Balance;
            }
            return netDict;
        }

        public Dictionary<int, List<double>> GetAmortizationTable(int MortgageGoalId)
        {
            var dbMortgageGoal = _mortgageGoalService.GetMortgageGoal(MortgageGoalId);
            if (dbMortgageGoal is null) { throw new NullReferenceException(); }
            var res = UserMortgageGoal.FromMortgageGoal(dbMortgageGoal);

            var stepper = new BalanceStepUtility(res.TotalBalance, res.InterestRate_Percent / 12);

            // Return Values
            var amorDict = new Dictionary<int, List<double>>
            {
                { 0, new List<double>() {stepper.Balance, 0.0, 0.0 } }
            };

            while (stepper.Balance > 0.0)
            {
                 _ = stepper.Step(-1 * (res.MonthlyPayment + res.AdditionalBudgetAllocation));
                    amorDict[stepper.NumberOfSteps] = new List<double>()
                    {
                        stepper.Balance, // Balance
                        stepper.CumulativeContribution - stepper.InterestAccumulated, // Principle paid down so far
                        stepper.InterestAccumulated // Interest paid so far
                    };
            }
            amorDict[stepper.NumberOfSteps] = new List<double>()
                    {
                        stepper.Balance, // Balance
                        stepper.CumulativeContribution - stepper.InterestAccumulated, // Principle paid down so far
                        stepper.InterestAccumulated // Interest paid so far
                    };

            return amorDict;
        }

        public UserMortgageGoal? ModifyUserMortgageGoal(UserMortgageGoal MortgageGoal)
        {
            var toModify = MortgageGoal.ToMortgageGoal();

            var dbMortgageGoal = _mortgageGoalService.ModifyMortgageGoal(toModify);
            if (dbMortgageGoal == null)
            {
                throw new NullReferenceException();
            }
            var res = UserMortgageGoal.FromMortgageGoal(dbMortgageGoal);
            return res;
        }
    }
}
