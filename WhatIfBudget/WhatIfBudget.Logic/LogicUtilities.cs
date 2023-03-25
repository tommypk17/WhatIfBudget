using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatIfBudget.Logic
{
    internal class LogicUtilities
    {
        public (double, double) InterestStep (double startBalance, double monthlyInterest, double contribution)
        {
            var newBalance = startBalance + contribution;
            var interestAccrued = newBalance * monthlyInterest;
            newBalance += interestAccrued;
            return (Math.Round(newBalance, 2), Math.Round(interestAccrued, 2));
        }

        public int FinalPayDownMonth(double startBalance, double interestRate, double contribution)
        {
            var months = 0;
            var interestAccrued = 0.0;
            var newBalance = (double)startBalance;
            while (newBalance > 0)
            {
                interestAccrued = newBalance * (interestRate / 100);
                newBalance += interestAccrued;
                newBalance -= contribution;
                months++;
            }
            return months;
        }
    }
}
