using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatIfBudget.Data.Models
{
    public class InvestmentGoal : BaseEntity
    {
        public double TotalBalance { get; set; } = 0;
        public double AnnualReturnRate_Percent { get; set; } = 0;
        public UInt16 YearsToMaturation { get; set; } = 0;
        public double AnnualRaiseFactor_Percent { get; set; } = 0;
        public double additionalBudgetAllocation { get; set; } = 0;
    }
}
