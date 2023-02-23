using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatIfBudget.Data.Models
{
    public class Investment : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = String.Empty;
        public double CurrentBalance { get; set; } = 0;
        public double MonthlyPersonalContribution { get; set; } = 0;
        public double MonthlyEmployerContribution { get; set; } = 0;
    }
}
