using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatIfBudget.Data.Models
{
    public class InvestmentGoalMemberships : BaseEntity
    {
        public int InvestmentGoalId { get; set; }
        public int InvestmentId { get; set; }
    }
}
