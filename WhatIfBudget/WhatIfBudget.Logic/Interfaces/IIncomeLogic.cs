using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Common.Interfaces;

namespace WhatIfBudget.Logic.Interfaces
{
    public interface IIncomeLogic
    {
        public IList<IResponseObject> GetUserIncome();
    }
}
