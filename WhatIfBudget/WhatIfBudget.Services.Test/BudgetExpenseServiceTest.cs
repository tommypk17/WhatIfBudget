using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WhatIfBudget.Common.Enumerations;
using WhatIfBudget.Data.DAL;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Services.Interfaces;

namespace WhatIfBudget.Services.Test
{
    [TestClass]
    public class BudgetExpenseServiceTest
    {
        private readonly IBudgetExpenseService _budgetExpenseService;
        private readonly AppDbContext _ctx;

        [TestCleanup()]
        public void TestCleanUp()
        {
            var allBudgetExpenses = _ctx.BudgetExpenses.ToList();
            _ctx.BudgetExpenses.RemoveRange(allBudgetExpenses);
            _ctx.SaveChanges();
        }

        public BudgetExpenseServiceTest()
        {
            DbContextOptions<AppDbContext> options;
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("WhatIfBudget");
            options = builder.Options;
            _ctx = new AppDbContext(options);

            _budgetExpenseService = new BudgetExpenseService(_ctx);
        }

        [TestMethod]
        public void GetBudgetExpense()
        {
            Helper_SeedDB();
            var expected = (List<BudgetExpense>)Helper_SeedBudgetExpenses();

            var actual = (List<BudgetExpense>)_budgetExpenseService.GetAllBudgetExpenses();

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void AddNewBudgetExpense()
        {
            var expected = new BudgetExpense()
            {
                Id = 11,
                BudgetId = 4,
                ExpenseId = 3,
                CreatedOn = DateTime.MinValue,
                UpdatedOn = DateTime.MinValue
            };

            _budgetExpenseService.AddNewBudgetExpense(new BudgetExpense()
            {
                Id = 11,
                BudgetId = 4,
                ExpenseId = 3,
                CreatedOn = DateTime.MinValue,
                UpdatedOn = DateTime.MinValue
            });

            var actual = _ctx.BudgetExpenses.Where(x => x.Id == 11).FirstOrDefault();

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void DeleteBudgetExpense_CollectionAreEqual()
        {
            Helper_SeedDB();
            var expected = (List<BudgetExpense>)Helper_SeedBudgetExpenses();
            var toRemove = expected.First();
            expected.Remove(toRemove);

            _budgetExpenseService.DeleteBudgetExpense(toRemove.Id);

            var actual = _ctx.BudgetExpenses.ToList();

            actual.Should().BeEquivalentTo(expected);
        }

        public void Helper_SeedDB()
        {
            _ctx.BudgetExpenses.AddRange(Helper_SeedBudgetExpenses());
            _ctx.SaveChanges();
        }

        public IList<BudgetExpense> Helper_SeedBudgetExpenses()
        {
            var budgetExpenses = new List<BudgetExpense>();
            for (var i = 1; i <= 10; i++)
            {
                budgetExpenses.Add(new BudgetExpense()
                {
                    Id = i,
                    BudgetId = i+1,
                    ExpenseId = i+1,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                });
            }

            return budgetExpenses;
        }
    }
}