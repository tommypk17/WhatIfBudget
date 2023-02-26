using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatIfBudget.Data.Models
{
    public class DebtGoal : BaseEntity
    {
        public double AdditionalBudgetAllocation { get; set; } = 0;
        public virtual Budget? Budget { get; set; }
    }
}
