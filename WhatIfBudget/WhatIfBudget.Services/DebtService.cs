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
    public class DebtService : IDebtService
    {
        private readonly AppDbContext _ctx;
        public DebtService(AppDbContext ctx) {
            _ctx = ctx;
        }
        
        public IList<Debt> GetAllDebts()
        {
            return _ctx.Debts.ToList();
        }
    }
}
