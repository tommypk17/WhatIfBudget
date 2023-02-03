﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Common.Interfaces;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Logic.Models;
using WhatIfBudget.Services.Interfaces;

namespace WhatIfBudget.Logic
{
    public class IncomeLogic : IIncomeLogic
    {
        private readonly IIncomeService _incomeService;
        public IncomeLogic(IIncomeService incomeService) { 
            _incomeService = incomeService;
        }

        public IList<IResponseObject> GetUserIncome(Guid userId)
        {
            return (IList<IResponseObject>)_incomeService.GetAllIncome()
                                                            .Where(x => x.UserId == userId)
                                                            .Select(x => (IResponseObject)new UserIncome()
                                                                {
                                                                    Id = x.Id,
                                                                    Amount = x.Amount,
                                                                    Frequency = x.Frequency
                                                                }).ToList();
        }
    }
}