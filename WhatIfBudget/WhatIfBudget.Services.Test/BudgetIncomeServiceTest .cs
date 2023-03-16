using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WhatIfBudget.Common.Enumerations;
using WhatIfBudget.Data.DAL;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Services.Interfaces;

namespace WhatIfBudget.Services.Test
{
    [TestClass]
    public class BudgetIncomeServiceTest
    {
        private readonly IBudgetIncomeService _budgetIncomeService;
        private readonly AppDbContext _ctx;

        [TestCleanup()]
        public void TestCleanUp()
        {
            var allBudgetIncomes = _ctx.BudgetIncomes.ToList();
            _ctx.BudgetIncomes.RemoveRange(allBudgetIncomes);
            _ctx.SaveChanges();
        }

        public BudgetIncomeServiceTest()
        {
            DbContextOptions<AppDbContext> options;
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("WhatIfBudget");
            options = builder.Options;
            _ctx = new AppDbContext(options);

            _budgetIncomeService = new BudgetIncomeService(_ctx);
        }

        [TestMethod]
        public void GetBudgetIncome()
        {
            Helper_SeedDB();
            var expected = (List<BudgetIncome>)Helper_SeedBudgetIncomes();

            var actual = (List<BudgetIncome>)_budgetIncomeService.GetAllBudgetIncomes();

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void AddNewBudgetIncome()
        {
            var expected = new BudgetIncome()
            {
                Id = 11,
                BudgetId = 4,
                IncomeId = 3,
                CreatedOn = DateTime.MinValue,
                UpdatedOn = DateTime.MinValue
            };

            _budgetIncomeService.AddNewBudgetIncome(new BudgetIncome()
            {
                Id = 11,
                BudgetId = 4,
                IncomeId = 3,
                CreatedOn = DateTime.MinValue,
                UpdatedOn = DateTime.MinValue
            });

            var actual = _ctx.BudgetIncomes.Where(x => x.Id == 11).FirstOrDefault();

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void DeleteBudgetIncome_CollectionAreEqual()
        {
            Helper_SeedDB();
            var expected = (List<BudgetIncome>)Helper_SeedBudgetIncomes();
            var toRemove = expected.First();
            expected.Remove(toRemove);

            _budgetIncomeService.DeleteBudgetIncome(toRemove.Id);

            var actual = _ctx.BudgetIncomes.ToList();

            actual.Should().BeEquivalentTo(expected);
        }

        public void Helper_SeedDB()
        {
            _ctx.BudgetIncomes.AddRange(Helper_SeedBudgetIncomes());
            _ctx.SaveChanges();
        }

        public IList<BudgetIncome> Helper_SeedBudgetIncomes()
        {
            var budgetIncomes = new List<BudgetIncome>();
            for (var i = 1; i <= 10; i++)
            {
                budgetIncomes.Add(new BudgetIncome()
                {
                    Id = i,
                    BudgetId = i+1,
                    IncomeId = i+1,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                });
            }

            return budgetIncomes;
        }
    }
}