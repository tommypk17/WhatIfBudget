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

        [TestMethod]
        public void DeleteUserDebt_CollectionsAreEqual()
        {
            var mock = new Mock<IDebtService>();
            var mockDGDS = new Mock<IDebtGoalDebtService>();

            mock.Setup(x => x.DeleteDebt(It.IsAny<int>())).Returns(
                    new Debt()
                    {
                        Id = 1,
                        Name = "Test",
                        CurrentBalance = 1,
                        MinimumPayment = 1,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue,
                        InterestRate = 0
                    }
                );

            mock.Setup(x => x.GetAllDebts()).Returns(
                    new List<Debt>(){
                        new Debt()
                        {
                            Id = 1,
                            Name = "Test",
                            CurrentBalance = 1,
                            MinimumPayment = 1,
                            UserId = Guid.Empty,
                            CreatedOn = DateTime.MinValue,
                            UpdatedOn = DateTime.MinValue,
                            InterestRate = 0
                        }
                    }
                );

            mockDGDS.Setup(x => x.GetAllDebtGoalDebts()).Returns(
                new List<DebtGoalDebt>()
                {
                    new DebtGoalDebt()
                    {
                        Id = 1,
                        DebtId = 1,
                        DebtGoalId = 1
                    }
                });

            mockDGDS.Setup(x => x.DeleteDebtGoalDebt(It.IsAny<int>())).Returns(
                    new DebtGoalDebt()
                    {
                        Id = 1,
                        DebtId = 1,
                        DebtGoalId = 1,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue,
                    });


            var debtLogic = new DebtLogic(mock.Object, mockDGDS.Object);

            var expected = new UserDebt()
            {
                Id = 1,
                Name = "Test",
                GoalId = 0,
                CurrentBalance = 1,
                MinimumPayment = 1,
            };

            var actual = debtLogic.DeleteDebt(1, 1);

            actual.Should().BeEquivalentTo(expected);
        }
        [TestMethod]
        public void Get_DebtTotals()
        {
            var mockDGS = new Mock<IDebtGoalService>();
            var mockDS = new Mock<IDebtService>();

            mockDGS.Setup(x => x.GetDebtGoal(It.IsAny<int>())).Returns(
                new DebtGoal()
                {
                    Id = 1,
                    AdditionalBudgetAllocation = 540.0,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                }
                );
            mockDS.Setup(x => x.GetDebtsByDebtGoalId(It.IsAny<int>())).Returns(
                (IList<Debt>)new List<Debt>()
                {
                    new Debt()
                    {
                        Id = 1,
                        Name = "test",
                        CurrentBalance = 10000.0,
                        InterestRate = 1.0f,
                        MinimumPayment = 15.0,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    },
                    new Debt()
                    {
                        Id = 2,
                        Name = "test2",
                        CurrentBalance = 1000.0,
                        InterestRate = 1.5f,
                        MinimumPayment = 10.0,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                });

            var debtGoalLogic = new DebtGoalLogic(mockDGS.Object, mockDS.Object);

            var expected = new DebtGoalTotals()
            {
                AllocationSavings = 563.64,
                TotalCostToPayoff = 11081.82,
                MonthsToPayoff = 20,
                TotalInterestAccrued = 81.82
            };

            var actual = debtGoalLogic.GetDebtTotals(1);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void DebtBalanceOverTime()
        {
            var mockDGS = new Mock<IDebtGoalService>();
            var mockDS = new Mock<IDebtService>();

            mockDGS.Setup(x => x.GetDebtGoal(It.IsAny<int>())).Returns(
                new DebtGoal()
                {
                    Id = 1,
                    AdditionalBudgetAllocation = 540.0,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                }
                );
            mockDS.Setup(x => x.GetDebtsByDebtGoalId(It.IsAny<int>())).Returns(
                (IList<Debt>)new List<Debt>()
                {
                    new Debt()
                    {
                        Id = 1,
                        Name = "test",
                        CurrentBalance = 10000.0,
                        InterestRate = 1.0f,
                        MinimumPayment = 15.0,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    },
                    new Debt()
                    {
                        Id = 2,
                        Name = "test2",
                        CurrentBalance = 1000.0,
                        InterestRate = 1.5f,
                        MinimumPayment = 10.0,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                });

            var debtGoalLogic = new DebtGoalLogic(mockDGS.Object, mockDS.Object);

            var expected = new Dictionary<int, double>
            {
                { 0, 11000.00 },
                { 1, 9970.09 },
                { 2, 9413.13 },
                { 3, 8855.71 },
                { 4, 8297.82 },
                { 5, 7739.46 },
                { 6, 7180.64 },
                { 7, 6621.35 },
                { 8, 6061.58 },
                { 9, 5501.34 },
                { 10, 4940.63 },
                { 11, 4379.45 },
                { 12, 3817.80 },
                { 13, 3255.67 },
                { 14, 2693.07 },
                { 15, 2130.00 },
                { 16, 1566.46 },
                { 17, 1002.45 },
                { 18, 437.96 },
                { 19, 346.82 },
                { 20, 0.0 },
            };

            var actual = debtGoalLogic.GetBalanceOverTime(1);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
