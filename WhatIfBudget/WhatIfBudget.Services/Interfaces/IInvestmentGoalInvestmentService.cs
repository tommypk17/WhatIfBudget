﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Services.Interfaces
{
    public interface IInvestmentGoalInvestmentService
    {
        public IList<InvestmentGoalInvestment> GetAllInvestmentGoalInvestments();

        public InvestmentGoalInvestment? AddNewInvestmentGoalInvestment(InvestmentGoalInvestment newInvestmentGoalInvestment);

        public InvestmentGoalInvestment? DeleteInvestmentGoalInvestment(int id);
    }
}
