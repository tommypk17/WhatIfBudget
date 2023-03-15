using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Data.DAL;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Services.Interfaces;

namespace WhatIfBudget.Services
{
    public class DebtGoalDebtService : IDebtGoalDebtService
    {
        private readonly AppDbContext _ctx;
        public DebtGoalDebtService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public IList<DebtGoalDebt> GetAllDebtGoalDebts()
        {
            return _ctx.DebtGoalDebts.ToList();
        }
    }
}