using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WhatIfBudget.Common.Enumerations;
using WhatIfBudget.Data.DAL;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Services.Interfaces;

namespace WhatIfBudget.Services.Test
{
    [TestClass]
    public class MortgageGoalServiceTest
    {
        private readonly IMortgageGoalService _mortgageGoalService;
        private readonly AppDbContext _ctx;

        [TestCleanup()]
        public void TestCleanUp()
        {
            var allMortgageGoals = _ctx.MortgageGoals.ToList();
            _ctx.MortgageGoals.RemoveRange(allMortgageGoals);
            _ctx.SaveChanges();
        }

        public MortgageGoalServiceTest()
        {
            DbContextOptions<AppDbContext> options;
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("WhatIfBudget");
            options = builder.Options;
            _ctx = new AppDbContext(options);

            _mortgageGoalService = new MortgageGoalService(_ctx);
        }

        [TestMethod]
        public void GetMortgageGoal()
        {
            Helper_SeedDB();
            var expected = Helper_SeedMortgageGoals()[1];

            var actual = _mortgageGoalService.GetMortgageGoal(2);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void AddNewMortgageGoal()
        {
            var expected = new MortgageGoal()
            {
                Id = 11,
                TotalBalance = 111111.0,
                InterestRate_Percent = 5.0,
                MonthlyPayment = 2000.0,
                EstimatedCurrentValue = 222222.0,
                AdditionalBudgetAllocation = 500,
                CreatedOn = DateTime.MinValue,
                UpdatedOn = DateTime.MinValue
            };

            _mortgageGoalService.AddMortgageGoal(new MortgageGoal()
            {
                Id = 11,
                TotalBalance = 111111.0,
                InterestRate_Percent = 5.0,
                MonthlyPayment = 2000.0,
                EstimatedCurrentValue = 222222.0,
                AdditionalBudgetAllocation = 500,
                CreatedOn = DateTime.MinValue,
                UpdatedOn = DateTime.MinValue
            });

            var actual = _ctx.MortgageGoals.Where(x => x.Id == 11).FirstOrDefault();

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void UpdateMortgageGoal()
        {
            _ctx.MortgageGoals.Add(new MortgageGoal()
            {
                Id = 11,
                TotalBalance = 111111.0,
                InterestRate_Percent = 5.0,
                MonthlyPayment = 2000.0,
                EstimatedCurrentValue = 222222.0,
                AdditionalBudgetAllocation = 500,
            });
            _ctx.SaveChanges();
            _ctx.ChangeTracker.Clear();

            var expected = new MortgageGoal()
            {
                Id = 11,
                TotalBalance = 99999.0,
                InterestRate_Percent = 5.0,
                MonthlyPayment = 2000.0,
                EstimatedCurrentValue = 200000.0,
                AdditionalBudgetAllocation = 200,
            };

            var actual = _mortgageGoalService.ModifyMortgageGoal(new MortgageGoal()
            {
                Id = 11,
                TotalBalance = 99999.0,
                InterestRate_Percent = 5.0,
                MonthlyPayment = 2000.0,
                EstimatedCurrentValue = 200000.0,
                AdditionalBudgetAllocation = 200,
            });
            Assert.IsNotNull(actual);

            Assert.AreEqual(actual.Id, expected.Id);
            Assert.AreEqual(actual.TotalBalance, expected.TotalBalance);
            Assert.AreEqual(actual.InterestRate_Percent, expected.InterestRate_Percent);
            Assert.AreEqual(actual.MonthlyPayment, expected.MonthlyPayment);
            Assert.AreEqual(actual.EstimatedCurrentValue, expected.EstimatedCurrentValue);
            Assert.AreEqual(actual.AdditionalBudgetAllocation, expected.AdditionalBudgetAllocation);
        }

        [TestMethod]
        public void DeleteMortgageGoal_CollectionAreEqual()
        {
            Helper_SeedDB();
            var expected = (List<MortgageGoal>)Helper_SeedMortgageGoals();
            var toRemove = expected.First();
            expected.Remove(toRemove);

            _mortgageGoalService.DeleteMortgageGoal(toRemove.Id);

            var actual = _ctx.MortgageGoals.ToList();

            actual.Should().BeEquivalentTo(expected);
        }

        public void Helper_SeedDB()
        {
            _ctx.MortgageGoals.AddRange(Helper_SeedMortgageGoals());
            _ctx.SaveChanges();
        }

        public IList<MortgageGoal> Helper_SeedMortgageGoals()
        {
            var mortgageGoals = new List<MortgageGoal>();
            for (var i = 1; i <= 10; i++)
            {
                mortgageGoals.Add(new MortgageGoal()
                {
                    Id = i,
                    TotalBalance = i * 100000.0,
                    InterestRate_Percent = i * 1.0,
                    MonthlyPayment = i * 1000,
                    EstimatedCurrentValue = i * 200000.0,
                    AdditionalBudgetAllocation = i * 100.0,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                });
            }

            return mortgageGoals;
        }
    }
}