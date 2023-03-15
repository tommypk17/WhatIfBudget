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
    public class DebtServiceTest
    {
        private IDebtService _debtService;
        private AppDbContext _ctx;

        [TestCleanup()]
        public void TestCleanUp()
        {
            var allDebts = _ctx.Debts.ToList();
            _ctx.Debts.RemoveRange(allDebts);
            _ctx.SaveChanges();
        }

        public DebtServiceTest()
        {
            DbContextOptions<AppDbContext> options;
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("WhatIfBudget");
            options = builder.Options;
            _ctx = new AppDbContext(options);

            _debtService = new DebtService(_ctx);
        }

        [TestMethod]
        public void GetAllDebt_CollectionAreEqual()
        {
            Helper_SeedDB();
            var expected = (List<Debt>)Helper_SeedDebts();

            var actual = (List<Debt>)_debtService.GetAllDebts();

            actual.Should().BeEquivalentTo(expected);
        }

        public void Helper_SeedDB()
        {
            _ctx.Debts.AddRange(Helper_SeedDebts());
            _ctx.SaveChanges();
        }

        public IList<Debt> Helper_SeedDebts()
        {
            var debts = new List<Debt>();
            for (var i = 1; i <= 10; i++)
            {
                debts.Add(new Debt() { Id = i, CurrentBalance = i * 102, InterestRate = 0.1f, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue });
            }

            return debts;
        }
    }
}
