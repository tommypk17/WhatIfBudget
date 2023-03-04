using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatIfBudget.Data.Models
{
    public class BudgetIncome : BaseEntity
    {
        public int BudgetId { get; set; }
        public int IncomeId { get; set; }
        public virtual Income? Income { get; set; }
        public virtual Budget? Budget { get; set; }
    }
}
