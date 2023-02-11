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

        [TestCleanup()]
        public void TestCleanUp()
        {
            var allExpenses = _ctx.Expenses.ToList();
            _ctx.Expenses.RemoveRange(allExpenses);
            _ctx.SaveChanges();
        }

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

        [TestMethod]
        public void AddNewExpense_CollectionAreEqual()
        {
            var expected = new List<Expense>
            {
                new Expense()
                {
                    Id = 11,
                    Amount = 11 * 102,
                    Frequency = EFrequency.None,
                    Priority = EPriority.Need,
                    UserId = Guid.Empty,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                }
            };

            _expenseService.AddNewExpense(new Expense()
            {
                Id = 11,
                Amount = 11 * 102,
                Frequency = EFrequency.None,
                Priority = EPriority.Need,
                UserId = Guid.Empty,
                CreatedOn = DateTime.MinValue,
                UpdatedOn = DateTime.MinValue
            });

            var actual = _ctx.Expenses.Where(x => x.UserId == Guid.Empty).ToList();

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void UpdateExpense_CollectionAreEqual()
        {
            _ctx.Expenses.Add(new Expense()
            {
                Id = 1,
                Amount = 1,
                Frequency = EFrequency.None,
                Priority = EPriority.Need,
                UserId = Guid.Empty,
            });
            _ctx.SaveChanges();
            _ctx.ChangeTracker.Clear();

            var expected = new Expense()
            {
                Id = 1,
                Amount = 100,
                Frequency = EFrequency.None,
                Priority = EPriority.Need,
                UserId = Guid.Empty,
            };

            var actual = _expenseService.UpdateExpense(new Expense()
            {
                Id = 1,
                Amount = 100,
                Frequency = EFrequency.None,
                Priority = EPriority.Need,
                UserId = Guid.Empty,
            });
            Assert.IsNotNull(actual);

            Assert.AreEqual(actual.Id, expected.Id);
            Assert.AreEqual(actual.Amount, expected.Amount);
            Assert.AreEqual(actual.Frequency, expected.Frequency);
            Assert.AreEqual(actual.Priority, expected.Priority);
            Assert.AreEqual(actual.UserId, expected.UserId);
        }

        [TestMethod]
        public void DeleteExpense_CollectionAreEqual()
        {
            Helper_SeedDB();
            var expected = (List<Expense>)Helper_SeedExpenses();
            var toRemove = expected.First();
            expected.Remove(toRemove);

            _expenseService.DeleteExpense(toRemove.Id);

            var actual = _ctx.Expenses.ToList();

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