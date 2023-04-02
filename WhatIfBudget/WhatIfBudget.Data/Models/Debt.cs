using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatIfBudget.Data.Models
{
    public class Debt : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = String.Empty;
        public double CurrentBalance { get; set; } = 0;
        public float InterestRate { get; set; } = 0;
        public double MinimumPayment { get; set; } = 0;
    }
}
