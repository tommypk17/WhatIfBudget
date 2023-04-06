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
    public class InvestmentGoalService : IInvestmentGoalService
    {
        private readonly AppDbContext _ctx;
        public InvestmentGoalService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public InvestmentGoal? GetInvestmentGoal(int id)
        {
            return _ctx.InvestmentGoals.Include(x => x.Budget).FirstOrDefault(x => x.Id == id);
        }

        public InvestmentGoal? AddInvestmentGoal(InvestmentGoal investmentGoal)
        {
            _ctx.InvestmentGoals.Add(investmentGoal);
            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return _ctx.InvestmentGoals.FirstOrDefault(x=> x.Id == investmentGoal.Id);
        }

        public InvestmentGoal? UpdateInvestmentGoal(InvestmentGoal investmentGoal)
        {
            _ctx.InvestmentGoals.Update(investmentGoal);
            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return _ctx.InvestmentGoals.FirstOrDefault(x=> x.Id == investmentGoal.Id);
        }

        public InvestmentGoal? DeleteInvestmentGoal(int id)
        {
            var InvestmentGoal = _ctx.InvestmentGoals.FirstOrDefault(x => x.Id == id);
            if (InvestmentGoal != null)
            {
                _ctx.InvestmentGoals.Remove(InvestmentGoal);
            }
            try
            {
                _ctx.SaveChanges();
                return InvestmentGoal;
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }
    }
}
