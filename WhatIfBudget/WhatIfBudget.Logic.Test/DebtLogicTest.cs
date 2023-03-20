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
            var mock2 = new Mock<IDebtGoalDebtService>();

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
            var DebtLogic = new DebtLogic(mock.Object, mock2.Object);

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

        [TestMethod]
        public void AddUserDebt_CollectionAreEqual()
        {
            var mock = new Mock<IDebtService>();
            var mockDGDS = new Mock<IDebtGoalDebtService>();

            mock.Setup(x => x.AddNewDebt(It.IsAny<Debt>())).Returns(
                new Debt()
                {
                    Id = 1,
                    Name = "test",
                    CurrentBalance = 0,
                    MinimumPayment= 0,
                    InterestRate = .1f,
                    UserId = Guid.Empty,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                }
            );

            var debtLogic = new DebtLogic(mock.Object, mockDGDS.Object);

            var expected = new UserDebt()
            {
                Id = 1,
                Name = "test",
                CurrentBalance = 0,
                MinimumPayment = 0,
                InterestRate = .1f,
                GoalId = 0
            };

            var actual = debtLogic.AddUserDebt(Guid.Empty, new UserDebt()
            {
                Id = 1,
                Name = "test",
                CurrentBalance = 0,
                MinimumPayment = 0,
                InterestRate = .1f,
                GoalId = 0
            });

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void ModifyUserDebt_ObjectsAreEqual()
        {
            var mock = new Mock<IDebtService>();
            var mockDGDS = new Mock<IDebtGoalDebtService>();

            mock.Setup(x => x.UpdateDebt(It.IsAny<Debt>())).Returns(
                    new Debt()
                    {
                        Id = 1,
                        Name = "test",
                        CurrentBalance = 0,
                        MinimumPayment = 0,
                        InterestRate = .1f,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                );
            var debtLogic = new DebtLogic(mock.Object, mockDGDS.Object);

            var expected = new UserDebt()
            {
                Id = 1,
                Name = "test",
                CurrentBalance = 0,
                MinimumPayment = 0,
                InterestRate = .1f,
                GoalId = 0
            };

            var actual = debtLogic.ModifyUserDebt(Guid.Empty, new UserDebt()
            {
                Id = 1,
                Name = "test",
                CurrentBalance = 0,
                MinimumPayment = 0,
                InterestRate = .1f,
                GoalId = 0
            });

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
