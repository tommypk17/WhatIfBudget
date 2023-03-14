using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Common.Enumerations;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Logic.Models;
using WhatIfBudget.Services.Interfaces;

namespace WhatIfBudget.Logic.Test
{
    [TestClass]
    public class DebtLogicTest
    {
        public DebtLogicTest()
        {

        }


        [TestMethod]
        public void GetUserDebts_CollectionAreEqual()
        {
            var mock = new Mock<IDebtService>();

            mock.Setup(x => x.GetAllDebts()).Returns(
                (IList<Debt>)new List<Debt>()
                {
                    new Debt() { Id = 1, Name = "Test", CurrentBalance = 100, InterestRate = 0.1f, MinimumPayment = 0, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Debt() { Id = 2, Name = "Test", CurrentBalance = 100, InterestRate = 0.1f, MinimumPayment = 0, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Debt() { Id = 3, Name = "Test", CurrentBalance = 100, InterestRate = 0.1f, MinimumPayment = 0, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Debt() { Id = 4, Name = "Test", CurrentBalance = 100, InterestRate = 0.1f, MinimumPayment = 0, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Debt() { Id = 5, Name = "Test", CurrentBalance = 100, InterestRate = 0.1f, MinimumPayment = 0, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Debt() { Id = 6, Name = "Test", CurrentBalance = 100, InterestRate = 0.1f, MinimumPayment = 0,  UserId = Guid.NewGuid(), CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                }
                );
            var DebtLogic = new DebtLogic(mock.Object);

            var expected = new List<UserDebt>()
            {
                new UserDebt() {Id = 1, Name = "Test", CurrentBalance = 100, InterestRate = 0.1f, MinimumPayment = 0},
                new UserDebt() {Id = 2, Name = "Test", CurrentBalance = 100, InterestRate = 0.1f, MinimumPayment = 0},
                new UserDebt() {Id = 3, Name = "Test", CurrentBalance = 100, InterestRate = 0.1f, MinimumPayment = 0},
                new UserDebt() {Id = 4, Name = "Test", CurrentBalance = 100, InterestRate = 0.1f, MinimumPayment = 0},
                new UserDebt() {Id = 5, Name = "Test", CurrentBalance = 100, InterestRate = 0.1f, MinimumPayment = 0},
            };

            var actual = DebtLogic.GetUserDebts(Guid.Empty);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
