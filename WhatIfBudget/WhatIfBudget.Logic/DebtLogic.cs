using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Logic.Models;
using WhatIfBudget.Services;
using WhatIfBudget.Services.Interfaces;

namespace WhatIfBudget.Logic
{
    public class DebtLogic : IDebtLogic
    {
        private readonly IDebtService _debtService;

        public DebtLogic(IDebtService debtService)
        {
            _debtService = debtService;
        }

        public IList<UserDebt> GetUserDebts(Guid userId)
        {
            return _debtService.GetAllDebts().Where(x => x.UserId == userId)
                                       .Select(x => UserDebt.FromDebt(x))
                                       .ToList();
        }
    }
}
