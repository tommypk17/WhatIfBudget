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
    public class InvestmentGoalInvestmentService : IInvestmentGoalInvestmentService
    {
        private readonly AppDbContext _ctx;
        public InvestmentGoalInvestmentService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public IList<InvestmentGoalInvestment> GetAllInvestmentGoalInvestments()
        {
            return _ctx.InvestmentGoalInvestments.ToList();
        }

        public InvestmentGoalInvestment? AddNewInvestmentGoalInvestment(InvestmentGoalInvestment investmentGoalInvestment)
        {
            _ctx.InvestmentGoalInvestments.Add(investmentGoalInvestment);
            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return _ctx.InvestmentGoalInvestments.FirstOrDefault(x=> x.Id == investmentGoalInvestment.Id);
        }

        public InvestmentGoalInvestment? DeleteInvestmentGoalInvestment(int id)
        {
            var InvestmentGoalInvestment = _ctx.InvestmentGoalInvestments.FirstOrDefault(x => x.Id == id);
            if (InvestmentGoalInvestment != null)
            {
                _ctx.InvestmentGoalInvestments.Remove(InvestmentGoalInvestment);
            }
            try
            {
                _ctx.SaveChanges();
                return InvestmentGoalInvestment;
             }
            catch(DbUpdateException)
            {
                return null;
            }
        }
    }
}