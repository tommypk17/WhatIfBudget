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
    public class IncomeService : IIncomeService
    {
        private readonly AppDbContext _ctx;
        public IncomeService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public IList<Income> GetAllIncome()
        {
            return _ctx.Incomes.ToList();
        }

        public void AddNewIncome(Income newIncome)
        {
            _ctx.Incomes.Add(newIncome);
            _ctx.SaveChanges();
        }

        public void UpdateIncome(Income modifiedIncome)
        {
            _ctx.Incomes.Update(modifiedIncome);
            _ctx.SaveChanges();
        }

        public void DeleteIncome(Income toRemove)
        {
            _ctx.Incomes.Remove(toRemove);
            _ctx.SaveChanges();
        }
    }
}
