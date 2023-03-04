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
    public class BudgetService : IBudgetService
    {
        private readonly AppDbContext _ctx;
        public BudgetService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public IList<Budget> GetAllBudgets()
        {
            return _ctx.Budgets.ToList();
        }

        public Budget? AddNewBudget(Budget budget)
        {
            _ctx.Budgets.Add(budget);
            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return _ctx.Budgets.FirstOrDefault(x=> x.Id == budget.Id);
        }

        public Budget? UpdateBudget(Budget budget)
        {
            _ctx.Budgets.Update(budget);
            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return _ctx.Budgets.FirstOrDefault(x=> x.Id == budget.Id);
        }

        public Budget? DeleteBudget(int id)
        {
            var dbBudget = _ctx.Budgets.FirstOrDefault(x => x.Id == id); 
            if (dbBudget != null)
            {
                _ctx.Budgets.Remove(dbBudget);
            }
            try
            {
                _ctx.SaveChanges();
                return dbBudget;
             }
            catch(DbUpdateException)
            {
                return null;
            }
        }

        public Budget? GetBudget(int budgetId)
        {
            return _ctx.Budgets.FirstOrDefault(x => x.Id == budgetId);
        }

        public bool Exists(int budgetId)
        {
            return _ctx.Budgets.Any(x => x.Id == budgetId);
        }
    }
}
