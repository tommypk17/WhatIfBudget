using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatIfBudget.Logic.Utilities
{
    internal class BalanceStepUtility
    {
        private double mBalance = 0;
        private double mInterestRate = 0;
        private double mInterestAccumulated = 0;
        private int mNumberOfSteps = 0;

        public BalanceStepUtility(double startBalance, double interestRatePercent)
        {
            mBalance = startBalance;
            mInterestRate = interestRatePercent / 100;
        }
        public double Step(double contribution)
        {
            mBalance += contribution;
            var interestAccrued = Math.Round(mBalance * mInterestRate, 2);
            mBalance += interestAccrued;
            mInterestAccumulated += interestAccrued;
            mNumberOfSteps++;
            return Math.Round(interestAccrued, 2);
        }
        public double GetBalance()
        {
            return Math.Round(mBalance, 2);
        }
        public double GetAccumulatedInterest()
        {
            return Math.Round(mInterestAccumulated, 2);
        }
        public int StepsCompleted()
        {
            return mNumberOfSteps;
        }

    }
}
