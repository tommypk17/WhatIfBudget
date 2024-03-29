﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatIfBudget.Data.Models
{
    public class Budget : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = String.Empty;
        public int SavingGoalId { get; set; }
        public int DebtGoalId { get; set; }
        public int MortgageGoalId { get; set; }
        public int InvestmentGoalId { get; set; }

        public virtual InvestmentGoal? InvestmentGoal { get; set; }
        public virtual SavingGoal? SavingGoal { get; set; }
        public virtual MortgageGoal? MortgageGoal { get; set; }
        public virtual DebtGoal? DebtGoal { get; set; }
    }
}
