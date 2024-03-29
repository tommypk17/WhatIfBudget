﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Logic.Models;

namespace WhatIfBudget.Logic.Interfaces
{
    public interface IInvestmentLogic
    {
        public IList<UserInvestment> GetUserInvestments(Guid userId);
        public IList<UserInvestment> GetUserInvestmentsByGoalId(int goalId);
        public UserInvestment? AddUserInvestment(Guid userId, UserInvestment userInvestment);
        public UserInvestment? ModifyUserInvestment(Guid userId, UserInvestment investment);
        public UserInvestment? DeleteInvestment(int investmentId, int investmentGoalId);
    }
}
