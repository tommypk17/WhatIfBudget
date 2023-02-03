﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Common.Interfaces;
using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Logic.Interfaces
{
    public interface IIncomeLogic
    {
        public IList<IResponseObject> GetUserIncome(Guid userId);

        public IList<IResponseObject> AddUserIncome(Income incomeToAdd);

        public IList<IResponseObject> ModifyUserIncome(Income incomeToModify);

        public IList<IResponseObject> DeleteUserIncome(Income incomeToRemove);
    }
}
