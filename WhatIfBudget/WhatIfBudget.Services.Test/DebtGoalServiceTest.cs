using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Common.Enumerations;
using WhatIfBudget.Data.DAL;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Services.Interfaces;

namespace WhatIfBudget.Services.Test
{
    [TestClass]
    public class DebtGoalServiceTest
    {
        private IDebtGoalService _debtGoalService;
        private AppDbContext _ctx;

        [TestCleanup()]
        public void TestCleanUp()
        {
            var allDebtGoals = _ctx.DebtGoals.ToList();
            _ctx.DebtGoals.RemoveRange(allDebtGoals);
            _ctx.SaveChanges();
        }

        public DebtGoalServiceTest()
        {
            DbContextOptions<AppDbContext> options;
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("WhatIfBudget");
            options = builder.Options;
            _ctx = new AppDbContext(options);

            _debtGoalService = new DebtGoalService(_ctx);
        }

        [TestMethod]
        public void GetDebtGoal()
        {
            Helper_SeedDB();
            var expected = Helper_SeedDebtGoals()[1];

            var actual = _debtGoalService.GetDebtGoal(2);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void UpdateDebtGoal()
        {
            _ctx.DebtGoals.Add(new DebtGoal()
            {
                Id = 1,
                CreatedOn = DateTime.MinValue,
                UpdatedOn = DateTime.MinValue,
                AdditionalBudgetAllocation = 250.0,
            });
            _ctx.SaveChanges();
            _ctx.ChangeTracker.Clear();

            var expected = new DebtGoal()
            {
                Id = 1,
                CreatedOn= DateTime.MinValue,
                UpdatedOn= DateTime.MinValue,
                AdditionalBudgetAllocation = 500.0,
            };

            var actual = _debtGoalService.UpdateDebtGoal(new DebtGoal()
            {
                Id = 1,
                CreatedOn= DateTime.MinValue,
                UpdatedOn= DateTime.MinValue,
                AdditionalBudgetAllocation = 500.0,
            });
            Assert.IsNotNull(actual);

            Assert.AreEqual(actual.Id, expected.Id);
            Assert.AreEqual(actual.CreatedOn, expected.CreatedOn);
            Assert.AreEqual(actual.UpdatedOn, expected.UpdatedOn);
            Assert.AreEqual(actual.AdditionalBudgetAllocation, expected.AdditionalBudgetAllocation);
        }

        public void Helper_SeedDB()
        {
            _ctx.DebtGoals.AddRange(Helper_SeedDebtGoals());
            _ctx.SaveChanges();
        }

        public IList<DebtGoal> Helper_SeedDebtGoals()
        {
            var debtGoals = new List<DebtGoal>();
            for (var i = 1; i <= 10; i++)
            {
                debtGoals.Add(new DebtGoal() { Id = i, AdditionalBudgetAllocation = 10, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue });
            }

            return debtGoals;
        }
    }
}
