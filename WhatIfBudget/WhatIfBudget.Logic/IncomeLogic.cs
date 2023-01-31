using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Common.Interfaces;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Services.Interfaces;

namespace WhatIfBudget.Logic
{
    public class IncomeLogic : IIncomeLogic
    {
        private readonly IIncomeService _incomeService;
        public IncomeLogic(IIncomeService incomeService) { 
            _incomeService = incomeService;
        }

        public IList<IResponseObject> GetUserIncome()
        {
            throw new NotImplementedException();
        }
    }
}
