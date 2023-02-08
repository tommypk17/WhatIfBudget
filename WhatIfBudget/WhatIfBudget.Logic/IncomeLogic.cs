using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Logic.Models;
using WhatIfBudget.Services.Interfaces;
using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Logic
{
    public class IncomeLogic : IIncomeLogic
    {
        private readonly IIncomeService _incomeService;
        public IncomeLogic(IIncomeService incomeService) { 
            _incomeService = incomeService;
        }

        public IList<UserIncome> GetUserIncome(Guid userId)
        {
            return _incomeService.GetAllIncome()
                                                .Where(x => x.UserId == userId)
                                                .Select(x =>
                                                                UserIncome.FromIncome(x)
                                                        )
                                                .ToList();
        }
        public UserIncome AddUserIncome(Guid userId, UserIncome income)
        {
            var toCreate = income.ToIncome(userId);

            var dbIncome = _incomeService.AddNewIncome(toCreate);
            if (dbIncome == null)
            {
                throw new NullReferenceException();
            }
            return UserIncome.FromIncome(dbIncome);
        }

        public UserIncome ModifyUserIncome(Guid userId, UserIncome income)
        {
            var toUpdate = income.ToIncome(userId);

            var dbIncome = _incomeService.UpdateIncome(toUpdate);
            if (dbIncome == null)
            {
                throw new NullReferenceException();
            }
            return UserIncome.FromIncome(dbIncome);
        }

        public UserIncome DeleteUserIncome(Guid userId, int id)
        {
            var dbIncome = _incomeService.DeleteIncome(id);
            if (dbIncome == null)
            {
                throw new NullReferenceException();
            }
            return UserIncome.FromIncome(dbIncome);
        }
    }
}
