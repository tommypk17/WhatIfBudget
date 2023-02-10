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
    public class ExpenseService : IExpenseService
    {
        private readonly AppDbContext _ctx;
        public ExpenseService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public IList<Expense> GetAllExpenses()
        {
            return _ctx.Expenses.ToList();
        }

        public Expense AddNewExpense(Expense expense)
        {
            _ctx.Expenses.Add(expense);
            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return null;
            }
            return _ctx.Expenses.FirstOrDefault(x=> x.Id == expense.Id);
        }

        public Expense? UpdateExpense(Expense expense)
        {
            _ctx.Expenses.Update(expense);
            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return _ctx.Expenses.FirstOrDefault(x => x.Id == expense.Id);
        }
    }
}
