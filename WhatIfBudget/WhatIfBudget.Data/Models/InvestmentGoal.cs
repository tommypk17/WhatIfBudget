using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatIfBudget.Data.Models
{
    public class InvestmentGoal : BaseEntity
    {
        public double AnnualReturnRate_Percent { get; set; } = 0;
        public UInt16 YearsToTarget { get; set; } = 0;
        public bool RolloverCompletedGoals { get; set; } = false;
        public double AdditionalBudgetAllocation { get; set; } = 0;

        public virtual Budget? Budget { get; set; }
    }
}
