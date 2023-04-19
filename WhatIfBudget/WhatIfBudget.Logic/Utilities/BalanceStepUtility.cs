using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatIfBudget.Logic.Utilities
{
    internal class BalanceStepUtility
    {
        private double originalBalance = 0;
        private double balance = 0;
        private double interestAccumulated = 0;
        private double cumulativeContribution = 0;

        public double Balance 
        {
            get
            {
                return Math.Round(balance, 2);
            }
            set
            {
                balance = value;
            }
        }
        public double InterestRate { get; set; } = 0;
        public double InterestAccumulated 
        { 
            get
            {
                return Math.Round(interestAccumulated, 2);
            }
            set
            {
                interestAccumulated = value;
            }
        }
        public double CumulativeContribution
        {
            get
            {
                return Math.Round(cumulativeContribution, 2);
            }
            set
            {
                cumulativeContribution = value;
            }
        }
        public int NumberOfSteps { get; set; } = 0;

        public BalanceStepUtility(double startBalance, double interestRatePercent)
        {
            originalBalance = startBalance;
            Balance = startBalance;
            InterestRate = interestRatePercent / 100;
        }
        public double Step(double contribution)
        {
            if (Balance + contribution <= 0)
            {
                contribution = -1 * Balance;
            }
            Balance += contribution;
            CumulativeContribution += Math.Abs(contribution);
            var interestAccrued = Math.Round(Balance * InterestRate, 2);
            Balance += interestAccrued;
            InterestAccumulated += interestAccrued;
            NumberOfSteps++;
            return Math.Round(interestAccrued, 2);
        }
        public void StepToTarget(double monthlyContribution, double target)
        {
            // Incrementing down
            if (target < balance)
            {
                if (monthlyContribution < 0)
                {
                    while (balance > target)
                    {
                        Step(monthlyContribution);
                    }
                }
            }
            // Incrementing up
            else
            {
                if (monthlyContribution >= 0)
                {
                    while (balance < target)
                    {
                        Step(monthlyContribution);
                    }
                }
            }
        }

        public void Reset()
        {
            Balance = originalBalance;
            InterestRate = 0;
            InterestAccumulated = 0;
            CumulativeContribution = 0;
            NumberOfSteps = 0;
        }
    }
}
