using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Common.Enumerations;
using WhatIfBudget.Common.Interfaces;

namespace WhatIfBudget.Logic.Models
{
    public class UserIncome : IResponseObject
    {
        public int Id { get; set; } 
        public double Amount { get; set; }
        public EFrequency Frequency { get; set; }
    }
}
