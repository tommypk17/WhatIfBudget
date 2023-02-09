using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WhatIfBudget.Common.Enumerations;
using WhatIfBudget.Data.DAL;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Services.Interfaces;

namespace WhatIfBudget.Services.Test
{
    [TestClass]
    public class IncomeServiceTest
    {
        private IIncomeService _incomeService;
        private AppDbContext _ctx;

        [TestCleanup()]
        public void TestCleanUp()
        {
            var allIncomes = _ctx.Incomes.ToList();
            _ctx.Incomes.RemoveRange(allIncomes);
            _ctx.SaveChanges();
        }

        public IncomeServiceTest()
        {
            DbContextOptions<AppDbContext> options;
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("WhatIfBudget");
            options = builder.Options;
            _ctx = new AppDbContext(options);

            _incomeService = new IncomeService(_ctx);
        }

        [TestMethod]
        public void GetAllIncome_CollectionAreEqual()
        {
            Helper_SeedDB();
            var expected = (List<Income>)Helper_SeedIncomes();

            var actual = (List<Income>)_incomeService.GetAllIncome();

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void AddNewIncome_CollectionAreEqual()
        {
            var expected = new List<Income>
            {
                new Income()
                {
                    Id = 11,
                    Amount = 11 * 102,
                    Frequency = EFrequency.None,
                    UserId = Guid.Empty,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                }
            };

            _incomeService.AddNewIncome(new Income()
            {
                Id = 11,
                Amount = 11 * 102,
                Frequency = EFrequency.None,
                UserId = Guid.Empty,
                CreatedOn = DateTime.MinValue,
                UpdatedOn = DateTime.MinValue
            });

            var actual = _ctx.Incomes.Where(x => x.UserId == Guid.Empty).ToList();

            actual.Should().BeEquivalentTo(expected);
        }

        public void Helper_SeedDB()
        {
            _ctx.Incomes.AddRange(Helper_SeedIncomes());
            _ctx.SaveChanges();
        }

        public IList<Income> Helper_SeedIncomes()
        {
            var incomes = new List<Income>();
            for (var i = 1; i <= 10; i++)
            {
                incomes.Add(new Income() { Id = i, Amount = i * 102, Frequency = EFrequency.None, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue });
            }

            return incomes;
        }
    }
}