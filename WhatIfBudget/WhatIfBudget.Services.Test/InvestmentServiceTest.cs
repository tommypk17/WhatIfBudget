using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WhatIfBudget.Common.Enumerations;
using WhatIfBudget.Data.DAL;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Services.Interfaces;

namespace WhatIfBudget.Services.Test
{
    [TestClass]
    public class InvestmentServiceTest
    {
        private IInvestmentService _investmentService;
        private AppDbContext _ctx;

        [TestCleanup()]
        public void TestCleanUp()
        {
            var allInvestments = _ctx.Investments.ToList();
            _ctx.Investments.RemoveRange(allInvestments);
            _ctx.SaveChanges();
        }

        public InvestmentServiceTest()
        {
            DbContextOptions<AppDbContext> options;
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("WhatIfBudget");
            options = builder.Options;
            _ctx = new AppDbContext(options);

            _investmentService = new InvestmentService(_ctx);
        }

        [TestMethod]
        public void GetAllInvestments_CollectionAreEqual()
        {
            Helper_SeedDB();
            var expected = (List<Investment>)Helper_SeedInvestments();

            var actual = (List<Investment>)_investmentService.GetAllInvestments();

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void AddNewInvestment_CollectionAreEqual()
        {
            var expected = new List<Investment>
            {
                new Investment()
                {
                    Id = 11,
                    Name = "Test",
                    CurrentBalance = 1,
                    MonthlyEmployerContribution = 0,
                    MonthlyPersonalContribution = 0,
                    UserId = Guid.Empty,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                }
            };

            _investmentService.AddNewInvestment(new Investment()
            {
                Id = 11,
                Name = "Test",
                CurrentBalance = 1,
                MonthlyEmployerContribution = 0,
                MonthlyPersonalContribution = 0,
                UserId = Guid.Empty,
                CreatedOn = DateTime.MinValue,
                UpdatedOn = DateTime.MinValue
            });

            var actual = _ctx.Investments.Where(x => x.UserId == Guid.Empty).ToList();

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void UpdateInvestment_ObjectsAreEqual()
        {
            _ctx.Investments.Add(new Investment()
            {
                Id = 11,
                Name = "Test",
                CurrentBalance = 1,
                MonthlyEmployerContribution = 0,
                MonthlyPersonalContribution = 0,
                UserId = Guid.Empty,
                CreatedOn = DateTime.MinValue,
                UpdatedOn = DateTime.MinValue
            });
            _ctx.SaveChanges();
            _ctx.ChangeTracker.Clear();

            var expected = new Investment()
            {
                Id = 11,
                Name = "Test1",
                CurrentBalance = 1,
                MonthlyEmployerContribution = 0,
                MonthlyPersonalContribution = 0,
                UserId = Guid.Empty,
                CreatedOn = DateTime.MinValue,
                UpdatedOn = DateTime.MinValue
            };

            var actual = _investmentService.UpdateInvestment(new Investment()
            {
                Id = 11,
                Name = "Test1",
                CurrentBalance = 1,
                MonthlyEmployerContribution = 0,
                MonthlyPersonalContribution = 0,
                UserId = Guid.Empty,
                CreatedOn = DateTime.MinValue,
                UpdatedOn = DateTime.MinValue
            });
            Assert.IsNotNull(actual);

            Assert.AreEqual(actual.Id, expected.Id);
            Assert.AreEqual(actual.Name, expected.Name);
            Assert.AreEqual(actual.UserId, expected.UserId);
            Assert.AreEqual(actual.CurrentBalance, expected.CurrentBalance);
            Assert.AreEqual(actual.MonthlyPersonalContribution, expected.MonthlyPersonalContribution);
            Assert.AreEqual(actual.MonthlyEmployerContribution, expected.MonthlyEmployerContribution);
        }

        [TestMethod]
        public void DeleteInvestment_CollectionsAreEqual()
        {
            Helper_SeedDB();
            var expected = (List<Investment>)Helper_SeedInvestments();
            var toRemove = expected.First();
            expected.Remove(toRemove);

            _investmentService.DeleteInvestment(toRemove.Id);

            var actual = _ctx.Investments.ToList();

            actual.Should().BeEquivalentTo(expected);
        }
        public void Helper_SeedDB()
        {
            _ctx.Investments.AddRange(Helper_SeedInvestments());
            _ctx.SaveChanges();
        }

        public IList<Investment> Helper_SeedInvestments()
        {
            var investments = new List<Investment>();
            for (var i = 1; i <= 10; i++)
            {
                investments.Add(
                            new Investment()
                            {
                                Id = i,
                                Name = "test" + i,
                                CurrentBalance = 0,
                                MonthlyEmployerContribution = 0,
                                MonthlyPersonalContribution = 0,
                                UserId = Guid.Empty,
                                CreatedOn = DateTime.MinValue,
                                UpdatedOn = DateTime.MinValue
                            });
            }

            return investments;
        }
    }
}