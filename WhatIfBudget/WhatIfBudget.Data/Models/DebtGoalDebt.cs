using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatIfBudget.Data.Models
{
    public class DebtGoalDebt : BaseEntity
    {
        public int DebtGoalId { get; set; }
        public int DebtId { get; set; }
        public virtual DebtGoal? DebtGoal { get; set; }
        public virtual Debt? Debt { get; set; }

    }
}
