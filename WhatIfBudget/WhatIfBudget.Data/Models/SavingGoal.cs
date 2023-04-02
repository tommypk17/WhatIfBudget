using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatIfBudget.Data.Models
{
    public class SavingGoal : BaseEntity
    {
        public double CurrentBalance { get; set; } = 0;
        public double TargetBalance { get; set; } = 0;
        public double AnnualReturnRate_Percent { get; set; } = 0;
        public double AdditionalBudgetAllocation { get; set; } = 0;

        public virtual Budget? Budget { get; set; }
    }
}
