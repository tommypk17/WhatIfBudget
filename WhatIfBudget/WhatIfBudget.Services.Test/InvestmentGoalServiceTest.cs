using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WhatIfBudget.Common.Enumerations;
using WhatIfBudget.Data.DAL;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Services.Interfaces;

namespace WhatIfBudget.Services.Test
{
    [TestClass]
    public class InvestmentGoalServiceTest
    {
        private readonly IInvestmentGoalService _investmentGoalService;
        private readonly AppDbContext _ctx;

        [TestCleanup()]
        public void TestCleanUp()
        {
            var allInvestmentGoals = _ctx.InvestmentGoals.ToList();
            _ctx.InvestmentGoals.RemoveRange(allInvestmentGoals);
            _ctx.SaveChanges();
        }

        public InvestmentGoalServiceTest()
        {
            DbContextOptions<AppDbContext> options;
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("WhatIfBudget");
            options = builder.Options;
            _ctx = new AppDbContext(options);

            _investmentGoalService = new InvestmentGoalService(_ctx);
        }

        [TestMethod]
        public void GetInvestmentGoal()
        {
            Helper_SeedDB();
            var expected = Helper_SeedInvestmentGoals()[1];

            var actual = _investmentGoalService.GetInvestmentGoal(2);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void AddNewInvestmentGoal()
        {
            var expected = new InvestmentGoal()
            {
                Id = 11,
                YearsToTarget = 40,
                AnnualReturnRate_Percent = 2.0,
                AdditionalBudgetAllocation = 250.0,
                CreatedOn = DateTime.MinValue,
                UpdatedOn = DateTime.MinValue
            };

            _investmentGoalService.AddInvestmentGoal(new InvestmentGoal()
            {
                Id = 11,
                YearsToTarget = 40,
                AnnualReturnRate_Percent = 2.0,
                AdditionalBudgetAllocation = 250.0,
                CreatedOn = DateTime.MinValue,
                UpdatedOn = DateTime.MinValue
            });

            var actual = _ctx.InvestmentGoals.Where(x => x.Id == 11).FirstOrDefault();

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void UpdateInvestmentGoal()
        {
            _ctx.InvestmentGoals.Add(new InvestmentGoal()
            {
                Id = 1,
                YearsToTarget = 11,
                AnnualReturnRate_Percent = 2.0,
                AdditionalBudgetAllocation = 250.0,
            });
            _ctx.SaveChanges();
            _ctx.ChangeTracker.Clear();

            var expected = new InvestmentGoal()
            {
                Id = 1,
                YearsToTarget = 10,
                AnnualReturnRate_Percent = 8.1,
                AdditionalBudgetAllocation = 500.0,
            };

            var actual = _investmentGoalService.UpdateInvestmentGoal(new InvestmentGoal()
            {
                Id = 1,
                YearsToTarget = 10,
                AnnualReturnRate_Percent = 8.1,
                AdditionalBudgetAllocation = 500.0,
            });
            Assert.IsNotNull(actual);

            Assert.AreEqual(actual.Id, expected.Id);
            Assert.AreEqual(actual.YearsToTarget, expected.YearsToTarget);
            Assert.AreEqual(actual.AnnualReturnRate_Percent, expected.AnnualReturnRate_Percent);
            Assert.AreEqual(actual.AdditionalBudgetAllocation, expected.AdditionalBudgetAllocation);
        }

        [TestMethod]
        public void DeleteInvestmentGoal_CollectionAreEqual()
        {
            Helper_SeedDB();
            var expected = (List<InvestmentGoal>)Helper_SeedInvestmentGoals();
            var toRemove = expected.First();
            expected.Remove(toRemove);

            _investmentGoalService.DeleteInvestmentGoal(toRemove.Id);

            var actual = _ctx.InvestmentGoals.ToList();

            actual.Should().BeEquivalentTo(expected);
        }

        public void Helper_SeedDB()
        {
            _ctx.InvestmentGoals.AddRange(Helper_SeedInvestmentGoals());
            _ctx.SaveChanges();
        }

        public IList<InvestmentGoal> Helper_SeedInvestmentGoals()
        {
            var investmentGoals = new List<InvestmentGoal>();
            for (var i = 1; i <= 10; i++)
            {
                investmentGoals.Add(new InvestmentGoal()
                {
                    Id = i,
                    YearsToTarget = (ushort)(i * 5),
                    AnnualReturnRate_Percent = i,
                    AdditionalBudgetAllocation = i * 50.0,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                });
            }

            return investmentGoals;
        }
    }
}