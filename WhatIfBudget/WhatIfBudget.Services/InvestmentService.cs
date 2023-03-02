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

        public Investment? DeleteInvestment(int id)
        {
            var investment = _ctx.Investments.FirstOrDefault(x => x.Id == id);
            if (investment != null)
            {
                _ctx.Investments.Remove(investment);
            }
            try
            {
                _ctx.SaveChanges();
                return investment;
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }

        public IList<Investment> GetInvestmentsByInvestmentGoalId(int id)
        {
            var investments = _ctx.InvestmentGoalInvestments
                        .Include(x => x.Investment)
                        .Where(x => x.InvestmentGoalId == id && x.Investment != null)
                        .Select(x => x.Investment).ToList();
            if (investments.Any())
            {
                return investments!;
            }
            return new List<Investment>();
        }
    }
}
