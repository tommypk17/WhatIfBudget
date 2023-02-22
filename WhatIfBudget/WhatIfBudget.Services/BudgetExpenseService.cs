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
    public class BudgetExpenseService : IBudgetExpenseService
    {
        private readonly AppDbContext _ctx;
        public BudgetExpenseService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public IList<BudgetExpense> GetAllBudgetExpenses()
        {
            return _ctx.BudgetExpenses.ToList();
        }

        public BudgetExpense? AddNewBudgetExpense(BudgetExpense budgetExpense)
        {
            _ctx.BudgetExpenses.Add(budgetExpense);
            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return _ctx.BudgetExpenses.FirstOrDefault(x=> x.Id == budgetExpense.Id);
        }

        public BudgetExpense? DeleteBudgetExpense(int id)
        {
            var BudgetExpense = _ctx.BudgetExpenses.FirstOrDefault(x => x.Id == id);
            if (BudgetExpense != null)
            {
                _ctx.BudgetExpenses.Remove(BudgetExpense);
            }
            try
            {
                _ctx.SaveChanges();
                return BudgetExpense;
             }
            catch(DbUpdateException)
            {
                return null;
            }
        }
    }
}