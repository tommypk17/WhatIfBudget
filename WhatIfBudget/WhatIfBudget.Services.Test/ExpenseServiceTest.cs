using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WhatIfBudget.Common.Enumerations;
using WhatIfBudget.Data.DAL;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Services.Interfaces;

namespace WhatIfBudget.Services.Test
{
    [TestClass]
    public class ExpenseServiceTest
    {
        private IExpenseService _expenseService;
        private AppDbContext _ctx;

        public ExpenseServiceTest()
        {
            DbContextOptions<AppDbContext> options;
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("WhatIfBudget");
            options = builder.Options;
            _ctx = new AppDbContext(options);

            _expenseService = new ExpenseService(_ctx);
        }

        [TestMethod]
        public void GetAllExpense_CollectionAreEqual()
        {
            Helper_SeedDB();
            var expected = (List<Expense>)Helper_SeedExpenses();

            var actual = (List<Expense>)_expenseService.GetAllExpenses();

            actual.Should().BeEquivalentTo(expected);
        }

        public void Helper_SeedDB()
        {
            _ctx.Expenses.AddRange(Helper_SeedExpenses());
            _ctx.SaveChanges();
        }

        public IList<Expense> Helper_SeedExpenses()
        {
            var expenses = new List<Expense>();
            for (var i = 1; i <= 10; i++)
            {
                expenses.Add(new Expense() { Id = i, Amount = i * 102, Frequency = EFrequency.None, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue });
            }

            return expenses;
        }
    }
}