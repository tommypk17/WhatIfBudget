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
            var allExpenses = _ctx.Expenses.ToList();
            _ctx.Expenses.RemoveRange(allExpenses);
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