using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatIfBudget.Logic
{
    internal class LogicUtilities
    {
        public double InterestStep (double startBalance, double monthlyInterest, double contribution)
        {
            var newBalance = startBalance + contribution;
            newBalance *= 1 + monthlyInterest;
            return Math.Round(newBalance, 2);
        }
    }
}
