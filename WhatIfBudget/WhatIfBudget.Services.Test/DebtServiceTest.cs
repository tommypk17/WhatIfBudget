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

        [TestMethod]
        public void AddNewDebt_CollectionAreEqual()
        {
            var expected = new List<Debt>
            {
                new Debt()
                {
                    Id = 11,
                    Name = "Test",
                    CurrentBalance = 1,
                    InterestRate = 0,
                    MinimumPayment = 0,
                    UserId = Guid.Empty,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                }
            };

            _debtService.AddNewDebt(new Debt()
            {
                Id = 11,
                Name = "Test",
                CurrentBalance = 1,
                InterestRate = 0,
                MinimumPayment = 0,
                UserId = Guid.Empty,
                CreatedOn = DateTime.MinValue,
                UpdatedOn = DateTime.MinValue
            });

            var actual = _ctx.Debts.Where(x => x.UserId == Guid.Empty).ToList();

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void UpdateDebt_ObjectsAreEqual()
        {
            _ctx.Debts.Add(new Debt()
            {
                Id = 11,
                Name = "Test",
                CurrentBalance = 1,
                InterestRate = 0,
                MinimumPayment = 0,
                UserId = Guid.Empty,
                CreatedOn = DateTime.MinValue,
                UpdatedOn = DateTime.MinValue
            });
            _ctx.SaveChanges();
            _ctx.ChangeTracker.Clear();

            var expected = new Debt()
            {
                Id = 11,
                Name = "Test",
                CurrentBalance = 1,
                InterestRate = 0,
                MinimumPayment = 0,
                UserId = Guid.Empty,
                CreatedOn = DateTime.MinValue,
                UpdatedOn = DateTime.MinValue
            };

            var actual = _debtService.UpdateDebt(new Debt()
            {
                Id = 11,
                Name = "Test",
                CurrentBalance = 1,
                InterestRate = 0,
                MinimumPayment = 0,
                UserId = Guid.Empty,
                CreatedOn = DateTime.MinValue,
                UpdatedOn = DateTime.MinValue
            });
            Assert.IsNotNull(actual);

            Assert.AreEqual(actual.Id, expected.Id);
            Assert.AreEqual(actual.Name, expected.Name);
            Assert.AreEqual(actual.UserId, expected.UserId);
            Assert.AreEqual(actual.CurrentBalance, expected.CurrentBalance);
            Assert.AreEqual(actual.MinimumPayment, expected.MinimumPayment);
            Assert.AreEqual(actual.InterestRate, expected.InterestRate);
        }

        [TestMethod]
        public void DeleteDebt_CollectionsAreEqual()
        {
            Helper_SeedDB();
            var expected = (List<Debt>)Helper_SeedDebts();
            var toRemove = expected.First();
            expected.Remove(toRemove);

            _debtService.DeleteDebt(toRemove.Id);

            var actual = _ctx.Debts.ToList();

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
