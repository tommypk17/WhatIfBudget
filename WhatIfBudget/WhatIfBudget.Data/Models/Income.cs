using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Common.Enumerations;

namespace WhatIfBudget.Data.Models
{
    public class Income : BaseEntity
    {
        public Guid UserId { get; set; }
        public double Amount { get; set; } = 0;
        public EFrequency Frequency { get; set; } = EFrequency.None;
    }
}
