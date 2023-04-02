using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WhatIfBudget.Common.Enumerations;
using WhatIfBudget.Data.DAL;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Services.Interfaces;

namespace WhatIfBudget.Services.Test
{
    [TestClass]
    public class BudgetServiceTest
    {
        private IBudgetService _budgetService;
        private AppDbContext _ctx;

        [TestCleanup()]
        public void TestCleanUp()
        {
            var allBudgets = _ctx.Budgets.ToList();
            _ctx.Budgets.RemoveRange(allBudgets);
            _ctx.SaveChanges();
        }

        public BudgetServiceTest()
        {
            DbContextOptions<AppDbContext> options;
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("WhatIfBudget");
            options = builder.Options;
            _ctx = new AppDbContext(options);

            _budgetService = new BudgetService(_ctx);
        }

        [TestMethod]
        public void GetAllBudget_CollectionAreEqual()
        {
            Helper_SeedDB();
            var expected = (List<Budget>)Helper_SeedBudgets();

            var actual = (List<Budget>)_budgetService.GetAllBudgets();

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void AddNewBudget_CollectionAreEqual()
        {
            var expected = new List<Budget>
            {
                new Budget()
                {
                    Id = 11,
                    Name = "New",
                    UserId = Guid.Empty,
                    SavingGoalId = 1,
                    DebtGoalId = 1,
                    MortgageGoalId = 1,
                    InvestmentGoalId = 1,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                }
            };

            _budgetService.AddNewBudget(new Budget()
            {
                Id = 11,
                Name = "New",
                UserId = Guid.Empty,
                SavingGoalId = 1,
                DebtGoalId = 1,
                MortgageGoalId = 1,
                InvestmentGoalId = 1,
                CreatedOn = DateTime.MinValue,
                UpdatedOn = DateTime.MinValue
            });

            var actual = _ctx.Budgets.Where(x => x.UserId == Guid.Empty).ToList();

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void UpdateBudget_CollectionAreEqual()
        {
            _ctx.Budgets.Add(new Budget()
            {
                Id = 1,
                Name = "Test",
                UserId = Guid.Empty,
                SavingGoalId = 1,
                DebtGoalId = 1,
                MortgageGoalId = 1,
                InvestmentGoalId = 1,
            });
            _ctx.SaveChanges();
            _ctx.ChangeTracker.Clear();

            var expected = new Budget()
            {
                Id = 1,
                Name = "UpdateTest",
                UserId = Guid.Empty,
                SavingGoalId = 1,
                DebtGoalId = 1,
                MortgageGoalId = 1,
                InvestmentGoalId = 1,
            };

            var actual = _budgetService.UpdateBudget(new Budget()
            {
                Id = 1,
                Name = "UpdateTest",
                UserId = Guid.Empty,
                SavingGoalId = 1,
                DebtGoalId = 1,
                MortgageGoalId = 1,
                InvestmentGoalId = 1,
            });
            Assert.IsNotNull(actual);

            Assert.AreEqual(actual.Id, expected.Id);
            Assert.AreEqual(actual.Name, expected.Name);
            Assert.AreEqual(actual.UserId, expected.UserId);
            Assert.AreEqual(actual.SavingGoalId, expected.SavingGoalId);
            Assert.AreEqual(actual.DebtGoalId, expected.DebtGoalId);
            Assert.AreEqual(actual.MortgageGoalId, expected.MortgageGoalId);
            Assert.AreEqual(actual.InvestmentGoalId, expected.InvestmentGoalId);
        }

        [TestMethod]
        public void DeleteBudget_CollectionAreEqual()
        {
            Helper_SeedDB();
            var expected = (List<Budget>)Helper_SeedBudgets();
            var toRemove = expected.First();
            expected.Remove(toRemove);

            _budgetService.DeleteBudget(toRemove.Id);

            var actual = _ctx.Budgets.ToList();

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void GetBudget_IsEqual()
        {
            Helper_SeedDB();
            var expected = Helper_SeedBudgets().Where(x => x.Id == 2).First();

            var actual = _budgetService.GetBudget(expected.Id);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Exists_IsEqual()
        {
            Helper_SeedDB();
            var expected = true;

            var actual = _budgetService.Exists(5);

            actual.Should().Be(expected);
        }

        public void Helper_SeedDB()
        {
            _ctx.Budgets.AddRange(Helper_SeedBudgets());
            _ctx.SaveChanges();
        }

        public IList<Budget> Helper_SeedBudgets()
        {
            var budgets = new List<Budget>();
            for (var i = 1; i <= 10; i++)
            {
                budgets.Add(new Budget() { Id = i, SavingGoalId = i, DebtGoalId = i, MortgageGoalId = i, InvestmentGoalId = i, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue });
            }

            return budgets;
        }
    }
}