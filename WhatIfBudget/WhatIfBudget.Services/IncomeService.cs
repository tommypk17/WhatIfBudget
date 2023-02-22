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
    public class IncomeService : IIncomeService
    {
        private readonly AppDbContext _ctx;
        public IncomeService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public IList<Income> GetAllIncomes()
        {
            return _ctx.Incomes.ToList();
        }

        public Income? AddNewIncome(Income income)
        {
            _ctx.Incomes.Add(income);
            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return _ctx.Incomes.FirstOrDefault(x=> x.Id == income.Id);
        }

        public Income? UpdateIncome(Income income)
        {
            _ctx.Incomes.Update(income);
            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return _ctx.Incomes.FirstOrDefault(x=> x.Id == income.Id);
        }

        public Income? DeleteIncome(int id)
        {
            var Income = _ctx.Incomes.FirstOrDefault(x => x.Id == id); 
            if (Income != null)
            {
                _ctx.Incomes.Remove(Income);
            }
            try
            {
                _ctx.SaveChanges();
                return Income;
             }
            catch(DbUpdateException)
            {
                return null;
            }
        }
    }
}
