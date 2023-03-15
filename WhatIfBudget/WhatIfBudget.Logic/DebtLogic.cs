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
        private readonly IDebtGoalDebtService _dgdService;

        public DebtLogic(IDebtService debtService, IDebtGoalDebtService dgdService)
        {
            _debtService = debtService;
            _dgdService = dgdService;
        }

        public IList<UserDebt> GetUserDebts(Guid userId)
        {
            return _debtService.GetAllDebts().Where(x => x.UserId == userId)
                                       .Select(x => UserDebt.FromDebt(x))
                                       .ToList();
        }

        public IList<UserDebt> GetUserDebtsByGoalId(Guid userId, int goalId)
        {
            var debtGoalDebts = _dgdService.GetAllDebtGoalDebts().Where(x => x.DebtGoalId == goalId)
                                                                    .Select(x => x.DebtId).ToList();
            if (debtGoalDebts.Any())
            {
                return _debtService.GetAllDebts()
                                        .Where(x => debtGoalDebts.Contains(x.Id))
                                        .Select(x => UserDebt.FromDebt(x, goalId))
                                        .ToList();
            }
            else
            {
                return new List<UserDebt>();
            }
        }
    }
}
