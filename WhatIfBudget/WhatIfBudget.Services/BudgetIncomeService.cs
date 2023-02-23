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
    public class BudgetIncomeService : IBudgetIncomeService
    {
        private readonly AppDbContext _ctx;
        public BudgetIncomeService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public IList<BudgetIncome> GetAllBudgetIncomes()
        {
            return _ctx.BudgetIncomes.ToList();
        }

        public BudgetIncome? AddNewBudgetIncome(BudgetIncome budgetIncome)
        {
            _ctx.BudgetIncomes.Add(budgetIncome);
            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return _ctx.BudgetIncomes.FirstOrDefault(x=> x.Id == budgetIncome.Id);
        }

        public BudgetIncome? DeleteBudgetIncome(int id)
        {
            var BudgetIncome = _ctx.BudgetIncomes.FirstOrDefault(x => x.Id == id);
            if (BudgetIncome != null)
            {
                _ctx.BudgetIncomes.Remove(BudgetIncome);
            }
            try
            {
                _ctx.SaveChanges();
                return BudgetIncome;
             }
            catch(DbUpdateException)
            {
                return null;
            }
        }
    }
}