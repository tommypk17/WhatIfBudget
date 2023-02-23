using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Common.Enumerations;

namespace WhatIfBudget.Data.Models
{
    public class Income : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = String.Empty;
        public double Amount { get; set; } = 0;
        public EFrequency Frequency { get; set; } = EFrequency.None;
        public virtual ICollection<BudgetIncome> BudgetIncomes { get; set; } = new Collection<BudgetIncome>();

    }
}
