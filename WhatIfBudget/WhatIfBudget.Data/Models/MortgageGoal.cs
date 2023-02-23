using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatIfBudget.Data.Models
{
    public class MortgageGoal : BaseEntity
    {
        public double TotalBalance { get; set; } = 0;
        public double InterestRate_Percent { get; set; } = 0;
        public double MonthlyPayment { get; set; } = 0;

        public virtual Budget Budget { get; set; } = new Budget();
    }
}
