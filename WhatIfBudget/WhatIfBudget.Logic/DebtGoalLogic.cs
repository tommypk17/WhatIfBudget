using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Logic.Models;
using WhatIfBudget.Services.Interfaces;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Logic;
using System.Dynamic;
using System.Globalization;

namespace WhatIfBudget.Logic
{
    public class DebtGoalLogic : IDebtGoalLogic
    {
        private readonly IDebtGoalService _debtGoalService;
        private readonly IDebtService _debtService;
        public DebtGoalLogic(IDebtGoalService debtGoalService, IDebtService debtService) { 
            _debtGoalService = debtGoalService;
            _debtService = debtService;
        }

        public UserDebtGoal? GetDebtGoal(int debtGoalId)
        {
            var debtGoal = _debtGoalService.GetDebtGoal(debtGoalId);
            if(debtGoal is null) return null;
            return UserDebtGoal.FromDebtGoal(debtGoal);
        }
    }
}
