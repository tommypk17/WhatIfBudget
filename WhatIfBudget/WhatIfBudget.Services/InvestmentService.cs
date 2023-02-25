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
    public class InvestmentService : IInvestmentService
    {
        private readonly AppDbContext _ctx;
        public InvestmentService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public IList<Investment> GetAllInvestments()
        {
            return _ctx.Investments.ToList();
        }

        public Investment? AddNewInvestment(Investment investment)
        {
            _ctx.Investments.Add(investment);
            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return _ctx.Investments.FirstOrDefault(x => x.Id == investment.Id);
        }

        public Investment? UpdateInvestment(Investment investment)
        {
            _ctx.Investments.Update(investment);
            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return _ctx.Investments.FirstOrDefault(x => x.Id == investment.Id);
        }
    }
}
