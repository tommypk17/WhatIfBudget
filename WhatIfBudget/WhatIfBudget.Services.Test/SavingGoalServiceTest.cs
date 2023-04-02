using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WhatIfBudget.Common.Enumerations;
using WhatIfBudget.Data.DAL;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Services.Interfaces;

namespace WhatIfBudget.Services.Test
{
    [TestClass]
    public class SavingGoalServiceTest
    {
        private readonly ISavingGoalService _savingGoalService;
        private readonly AppDbContext _ctx;

        [TestCleanup()]
        public void TestCleanUp()
        {
            var allSavingGoals = _ctx.SavingGoals.ToList();
            _ctx.SavingGoals.RemoveRange(allSavingGoals);
            _ctx.SaveChanges();
        }

        public SavingGoalServiceTest()
        {
            DbContextOptions<AppDbContext> options;
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("WhatIfBudget");
            options = builder.Options;
            _ctx = new AppDbContext(options);

            _savingGoalService = new SavingGoalService(_ctx);
        }

        [TestMethod]
        public void GetSavingGoal()
        {
            Helper_SeedDB();
            var expected = Helper_SeedSavingGoals()[1];

            var actual = _savingGoalService.GetSavingGoal(2);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void AddNewSavingGoal()
        {
            var expected = new SavingGoal()
            {
                Id = 11,
                CurrentBalance = 1111.0,
                TargetBalance = 5555.0,
                AnnualReturnRate_Percent = 2.0,
                AdditionalBudgetAllocation = 250.0,
                CreatedOn = DateTime.MinValue,
                UpdatedOn = DateTime.MinValue
            };

            _savingGoalService.AddSavingGoal(new SavingGoal()
            {
                Id = 11,
                CurrentBalance = 1111.0,
                TargetBalance = 5555.0,
                AnnualReturnRate_Percent = 2.0,
                AdditionalBudgetAllocation = 250.0,
                CreatedOn = DateTime.MinValue,
                UpdatedOn = DateTime.MinValue
            });

            var actual = _ctx.SavingGoals.Where(x => x.Id == 11).FirstOrDefault();

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void UpdateSavingGoal()
        {
            _ctx.SavingGoals.Add(new SavingGoal()
            {
                Id = 1,
                CurrentBalance = 1111.0,
                TargetBalance = 5555.0,
                AnnualReturnRate_Percent = 2.0,
                AdditionalBudgetAllocation = 250.0,
            });
            _ctx.SaveChanges();
            _ctx.ChangeTracker.Clear();

            var expected = new SavingGoal()
            {
                Id = 1,
                CurrentBalance = 1111.0,
                TargetBalance = 7777.0,
                AnnualReturnRate_Percent = 2.1,
                AdditionalBudgetAllocation = 280.0,
            };

            var actual = _savingGoalService.ModifySavingGoal(new SavingGoal()
            {
                Id = 1,
                CurrentBalance = 1111.0,
                TargetBalance = 7777.0,
                AnnualReturnRate_Percent = 2.1,
                AdditionalBudgetAllocation = 280.0,
            });
            Assert.IsNotNull(actual);

            Assert.AreEqual(actual.Id, expected.Id);
            Assert.AreEqual(actual.CurrentBalance, expected.CurrentBalance);
            Assert.AreEqual(actual.TargetBalance, expected.TargetBalance);
            Assert.AreEqual(actual.AnnualReturnRate_Percent, expected.AnnualReturnRate_Percent);
            Assert.AreEqual(actual.AdditionalBudgetAllocation, expected.AdditionalBudgetAllocation);
        }

        [TestMethod]
        public void DeleteSavingGoal_CollectionAreEqual()
        {
            Helper_SeedDB();
            var expected = (List<SavingGoal>)Helper_SeedSavingGoals();
            var toRemove = expected.First();
            expected.Remove(toRemove);

            _savingGoalService.DeleteSavingGoal(toRemove.Id);

            var actual = _ctx.SavingGoals.ToList();

            actual.Should().BeEquivalentTo(expected);
        }

        public void Helper_SeedDB()
        {
            _ctx.SavingGoals.AddRange(Helper_SeedSavingGoals());
            _ctx.SaveChanges();
        }

        public IList<SavingGoal> Helper_SeedSavingGoals()
        {
            var savingGoals = new List<SavingGoal>();
            for (var i = 1; i <= 10; i++)
            {
                savingGoals.Add(new SavingGoal()
                {
                    Id = i,
                    CurrentBalance = i * 11.0,
                    TargetBalance = i * 500.0,
                    AnnualReturnRate_Percent = i,
                    AdditionalBudgetAllocation = i * 50.0,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                });
            }

            return savingGoals;
        }
    }
}