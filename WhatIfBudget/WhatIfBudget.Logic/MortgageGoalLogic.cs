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
            var allocationStepper = new BalanceStepUtility(res.TotalBalance, res.InterestRate_Percent / 12);

            // Return Values
            var MortgageGoalTotals = new MortgageGoalTotals();

            // Allocation stepper will always equal or lag balance stepper
            balanceStepper.StepToZero(-1 * (res.MonthlyPayment + res.AdditionalBudgetAllocation));
            allocationStepper.StepToZero(-1 * res.MonthlyPayment);

            MortgageGoalTotals.MonthsToPayoff = balanceStepper.NumberOfSteps;
            MortgageGoalTotals.TotalInterestAccrued = balanceStepper.InterestAccumulated;
            MortgageGoalTotals.TotalCostToPayoff = balanceStepper.CumulativeContribution;
            MortgageGoalTotals.AllocationSavings = Math.Round(allocationStepper.CumulativeContribution - balanceStepper.CumulativeContribution, 2);
            return MortgageGoalTotals;
        }

        public Dictionary<int, double> GetNetValueOverTime(int MortgageGoalId)
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
                if (balanceStepper.NumberOfSteps % 12 == 0)
                {
                    netDict[balanceStepper.NumberOfSteps / 12] = valueStepper.Balance - balanceStepper.Balance;
                }
            }
            // Final dictionary entry is home value
            var endYear = Math.Ceiling(balanceStepper.NumberOfSteps / 12.0);
            netDict[(int)endYear] = valueStepper.Balance;
            Console.WriteLine("Balance steps: {0} Value steps: {1}", balanceStepper.NumberOfSteps, valueStepper.NumberOfSteps);

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
                if (stepper.NumberOfSteps % 12 == 0)
                {
                    amorDict[stepper.NumberOfSteps / 12] = new List<double>()
                    {
                        stepper.Balance, // Balance
                        stepper.CumulativeContribution - stepper.InterestAccumulated, // Principle paid down so far
                        stepper.InterestAccumulated // Interest paid so far
                    };
                 }
            }
            var endYear = Math.Ceiling(stepper.NumberOfSteps / 12.0);
            amorDict[(int)endYear] = new List<double>()
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
