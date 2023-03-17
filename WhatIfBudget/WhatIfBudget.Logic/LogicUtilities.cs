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
    }
}
