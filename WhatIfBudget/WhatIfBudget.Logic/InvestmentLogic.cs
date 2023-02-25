using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Logic.Models;
using WhatIfBudget.Services.Interfaces;

namespace WhatIfBudget.Logic
{
    public class InvestmentLogic : IInvestmentLogic
    {
        private readonly IInvestmentService _investmentService;
        public InvestmentLogic(IInvestmentService investmentService) {
            _investmentService = investmentService;
        }
        public UserInvestment? AddUserInvestment(Guid userId, UserInvestment userInvestment)
        {
            var dbInvestment = _investmentService.AddNewInvestment(userInvestment.ToInvestment(userId));
            if (dbInvestment == null)
            {
                throw new NullReferenceException();
            }

            return UserInvestment.FromInvestment(dbInvestment);
        }
    }
}
