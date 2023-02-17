using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatIfBudget.Data.Models
{
    public class InvestmentGoal : BaseEntity
    {
        public Guid UserId { get; set; }
        public double TotalBalance { get; set; } = 0;
        public double AnnualReturnRate_Percent { get; set; } = 0;
        public UInt16 YearsToMaturation { get; set; } = 30;
        public double AnnualRaiseFactor_Percent { get; set; } = 0;
        public double additionalPersonalAllocation { get; set; } = 0;
    }
}
