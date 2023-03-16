using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WhatIfBudget.Common.Enumerations;
using WhatIfBudget.Data.DAL;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Services.Interfaces;

namespace WhatIfBudget.Services.Test
{
    [TestClass]
    public class InvestmentGoalInvestmentServiceTest
    {
        private readonly IInvestmentGoalInvestmentService _investmentGoalInvestmentService;
        private readonly AppDbContext _ctx;

        [TestCleanup()]
        public void TestCleanUp()
        {
            var allInvestmentGoalInvestments = _ctx.InvestmentGoalInvestments.ToList();
            _ctx.InvestmentGoalInvestments.RemoveRange(allInvestmentGoalInvestments);
            _ctx.SaveChanges();
        }

        public InvestmentGoalInvestmentServiceTest()
        {
            DbContextOptions<AppDbContext> options;
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("WhatIfBudget");
            options = builder.Options;
            _ctx = new AppDbContext(options);

            _investmentGoalInvestmentService = new InvestmentGoalInvestmentService(_ctx);
        }

        [TestMethod]
        public void GetInvestmentGoalInvestment()
        {
            Helper_SeedDB();
            var expected = (List<InvestmentGoalInvestment>)Helper_SeedInvestmentGoalInvestments();

            var actual = (List<InvestmentGoalInvestment>)_investmentGoalInvestmentService.GetAllInvestmentGoalInvestments();

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void AddNewInvestmentGoalInvestment()
        {
            var expected = new InvestmentGoalInvestment()
            {
                Id = 11,
                InvestmentGoalId = 4,
                InvestmentId = 3,
                CreatedOn = DateTime.MinValue,
                UpdatedOn = DateTime.MinValue
            };

            _investmentGoalInvestmentService.AddNewInvestmentGoalInvestment(new InvestmentGoalInvestment()
            {
                Id = 11,
                InvestmentGoalId = 4,
                InvestmentId = 3,
                CreatedOn = DateTime.MinValue,
                UpdatedOn = DateTime.MinValue
            });

            var actual = _ctx.InvestmentGoalInvestments.Where(x => x.Id == 11).FirstOrDefault();

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void DeleteInvestmentGoalInvestment_CollectionAreEqual()
        {
            Helper_SeedDB();
            var expected = (List<InvestmentGoalInvestment>)Helper_SeedInvestmentGoalInvestments();
            var toRemove = expected.First();
            expected.Remove(toRemove);

            _investmentGoalInvestmentService.DeleteInvestmentGoalInvestment(toRemove.Id);

            var actual = _ctx.InvestmentGoalInvestments.ToList();

            actual.Should().BeEquivalentTo(expected);
        }

        public void Helper_SeedDB()
        {
            _ctx.InvestmentGoalInvestments.AddRange(Helper_SeedInvestmentGoalInvestments());
            _ctx.SaveChanges();
        }

        public IList<InvestmentGoalInvestment> Helper_SeedInvestmentGoalInvestments()
        {
            var investmentGoalInvestments = new List<InvestmentGoalInvestment>();
            for (var i = 1; i <= 10; i++)
            {
                investmentGoalInvestments.Add(new InvestmentGoalInvestment()
                {
                    Id = i,
                    InvestmentGoalId = i+1,
                    InvestmentId = i+1,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                });
            }

            return investmentGoalInvestments;
        }
    }
}