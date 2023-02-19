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

        public IList<Budget> GetAllBudget()
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
            var Budget = _ctx.Budgets.FirstOrDefault(x => x.Id == id); 
            if (Budget != null)
            {
                _ctx.Budgets.Remove(Budget);
            }
            try
            {
                _ctx.SaveChanges();
                return Budget;
             }
            catch(DbUpdateException)
            {
                return null;
            }
        }
    }
}
