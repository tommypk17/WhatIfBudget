using FluentAssertions;
using Moq;
using WhatIfBudget.Services.Interfaces;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Services;
using WhatIfBudget.Logic.Models;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Common.Enumerations;
using WhatIfBudget.Data.Migrations;

namespace WhatIfBudget.Logic.Test
{
    [TestClass]
    public class IncomeLogicTest
    {
        public IncomeLogicTest()
        {

        }


        [TestMethod]
        public void GetBudgetIncomes_CollectionAreEqual()
        {
            var mockIS = new Mock<IIncomeService>();
            var mockBIS = new Mock<IBudgetIncomeService>();
            var mockBS = new Mock<IBudgetService>();

            mockIS.Setup(x => x.GetIncomesByBudgetId(It.IsAny<int>())).Returns(
                (IList<Income>) new List<Income>()
                {
                    new Income() { Id = 1, Amount = 100, Frequency = EFrequency.None, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Income() { Id = 2, Amount = 100, Frequency = EFrequency.None, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Income() { Id = 3, Amount = 100, Frequency = EFrequency.None, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Income() { Id = 4, Amount = 100, Frequency = EFrequency.None, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Income() { Id = 5, Amount = 100, Frequency = EFrequency.None, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                }
                );

            var incomeLogic = new IncomeLogic(mockIS.Object, mockBIS.Object, mockBS.Object);

            var expected = new List<UserIncome>()
            {
                new UserIncome() {Id = 1, BudgetId = 1, Amount = 100, Frequency = 0},
                new UserIncome() {Id = 2, BudgetId = 1, Amount = 100, Frequency = 0},
                new UserIncome() {Id = 3, BudgetId = 1, Amount = 100, Frequency = 0},
                new UserIncome() {Id = 4, BudgetId = 1, Amount = 100, Frequency = 0},
                new UserIncome() {Id = 5, BudgetId = 1, Amount = 100, Frequency = 0}
            };

            var actual = incomeLogic.GetBudgetIncomes(1);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void GetBudgetMonthlyIncome()
        {
            var mockIS = new Mock<IIncomeService>();
            var mockBIS = new Mock<IBudgetIncomeService>();
            var mockBS = new Mock<IBudgetService>();

            mockIS.Setup(x => x.GetIncomesByBudgetId(It.IsAny<int>())).Returns(
                (IList<Income>)new List<Income>()
                {
                    new Income() { Id = 1, Amount = 100, Frequency = EFrequency.None, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Income() { Id = 2, Amount = 100, Frequency = EFrequency.None, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Income() { Id = 3, Amount = 100, Frequency = EFrequency.Weekly, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Income() { Id = 4, Amount = 100, Frequency = EFrequency.Monthly, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Income() { Id = 5, Amount = 120, Frequency = EFrequency.Quarterly, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                }
                );
            var incomeLogic = new IncomeLogic(mockIS.Object, mockBIS.Object, mockBS.Object);

            var expected = 740.0;

            var actual = incomeLogic.GetBudgetMonthlyIncome(1);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void AddExistingUserIncome_CollectionAreEqual()
        {
            var mock = new Mock<IIncomeService>();
            var mock2 = new Mock<IBudgetIncomeService>();
            var mock3 = new Mock<IBudgetService>();

            mock.Setup(x => x.GetAllIncomes()).Returns(
                new List<Income>()
                {
                    new Income()
                    {
                        Id = 1,
                        Amount = 100,
                        Frequency = EFrequency.None,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                }
            );

            mock.Setup(x => x.AddNewIncome(It.IsAny<Income>())).Returns(
                    new Income()
                    {
                        Id = 1,
                        Amount = 100,
                        Frequency = EFrequency.None,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                );

            mock2.Setup(x => x.AddNewBudgetIncome(It.IsAny<BudgetIncome>())).Returns(
                new BudgetIncome()
                {
                    Id = 1,
                    BudgetId = 1,
                    IncomeId = 1,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                }
            );

            mock3.Setup(x => x.Exists(It.IsAny<int>())).Returns(
                true
            );

            var incomeLogic = new IncomeLogic(mock.Object, mock2.Object, mock3.Object);

            var expected = new UserIncome()
            {
                Id = 1,
                Amount = 100,
                BudgetId = 1,
                Frequency = EFrequency.None,
            };

            var actual = incomeLogic.AddUserIncome(Guid.Empty, new UserIncome()
            {
                Id = 1,
                Amount = 100,
                BudgetId = 1,
                Frequency = EFrequency.None,
            });

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void AddNewUserIncome_CollectionAreEqual()
        {
            var mock = new Mock<IIncomeService>();
            var mock2 = new Mock<IBudgetIncomeService>();
            var mock3 = new Mock<IBudgetService>();

            mock.Setup(x => x.GetAllIncomes()).Returns(
                new List<Income>()
                {
                    new Income()
                    {
                        Id = 3,
                        Amount = 300,
                        Frequency = EFrequency.None,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                }
            );

            mock.Setup(x => x.AddNewIncome(It.IsAny<Income>())).Returns(
                    new Income()
                    {
                        Id = 1,
                        Amount = 100,
                        Frequency = EFrequency.None,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                );

            mock2.Setup(x => x.AddNewBudgetIncome(It.IsAny<BudgetIncome>())).Returns(
                new BudgetIncome()
                {
                    Id = 1,
                    BudgetId = 1,
                    IncomeId = 1,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                }
            );

            mock3.Setup(x => x.Exists(It.IsAny<int>())).Returns(
                true
            );

            var incomeLogic = new IncomeLogic(mock.Object, mock2.Object, mock3.Object);

            var expected = new UserIncome()
            {
                Id = 1,
                Amount = 100,
                BudgetId = 1,
                Frequency = EFrequency.None,
            };

            var actual = incomeLogic.AddUserIncome(Guid.Empty, new UserIncome()
            {
                Id = 1,
                Amount = 100,
                BudgetId = 1,
                Frequency = EFrequency.None,
            });

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void ModifyUserIncome_CollectionAreEqual()
        {
            var mock = new Mock<IIncomeService>();
            var mock2 = new Mock<IBudgetIncomeService>();
            var mock3 = new Mock<IBudgetService>();

            mock.Setup(x => x.UpdateIncome(It.IsAny<Income>())).Returns(
                    new Income()
                    {
                        Id = 1,
                        Amount = 101,
                        Frequency = EFrequency.None,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                );
            var incomeLogic = new IncomeLogic(mock.Object, mock2.Object, mock3.Object);

            var expected = new UserIncome()
            {
                Id = 1,
                Amount = 101,
                Frequency = EFrequency.None,
            };

            var actual = incomeLogic.ModifyUserIncome(Guid.Empty, new UserIncome()
            {
                Id = 1,
                Amount = 101,
                Frequency = EFrequency.None,
            });

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void DeleteUserIncome_CollectionAreEqual()
        {
            var mockIS = new Mock<IIncomeService>();
            var mockBIS = new Mock<IBudgetIncomeService>();
            var mockBS = new Mock<IBudgetService>();

            mockIS.Setup(x => x.DeleteIncome(It.IsAny<int>())).Returns(
                    new Income()
                    {
                        Id = 1,
                        Amount = 101,
                        Frequency = EFrequency.None,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                );

            mockIS.Setup(x => x.GetAllIncomes()).Returns(
                    new List<Income>(){
                        new Income()
                        {
                            Id = 1,
                            Amount = 101,
                            Frequency = EFrequency.None,
                            UserId = Guid.Empty,
                            CreatedOn = DateTime.MinValue,
                            UpdatedOn = DateTime.MinValue
                        }
                    }
                );

            mockBIS.Setup(x => x.DeleteBudgetIncome(It.IsAny<int>())).Returns(
                new BudgetIncome()
                {
                    Id = 1,
                    BudgetId = 1,
                    IncomeId = 1,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                }
            );

            mockBIS.Setup(x => x.GetAllBudgetIncomes()).Returns(
                new List<BudgetIncome>()
                {
                    new BudgetIncome()
                    {
                        Id = 1,
                        BudgetId = 1,
                        IncomeId = 1,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    },
                    new BudgetIncome()
                    {
                        Id = 1,
                        BudgetId = 2,
                        IncomeId = 2,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }

                }
            );

            var incomeLogic = new IncomeLogic(mockIS.Object, mockBIS.Object, mockBS.Object);

            var expected = new UserIncome()
            {
                Id = 1,
                Amount = 101,
                BudgetId = 1,
                Frequency = EFrequency.None,
            };

            var actual = incomeLogic.DeleteBudgetIncome(1, 1);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void DeleteBudgetIncome_CollectionAreEqual()
        {
            var mockIS = new Mock<IIncomeService>();
            var mockBIS = new Mock<IBudgetIncomeService>();
            var mockBS = new Mock<IBudgetService>();

            mockIS.Setup(x => x.DeleteIncome(It.IsAny<int>())).Returns(
                    new Income()
                    {
                        Id = 1,
                        Amount = 101,
                        Frequency = EFrequency.None,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                );

            mockIS.Setup(x => x.GetAllIncomes()).Returns(
                new List<Income>()
                {
                    new Income()
                    {
                        Id = 1,
                        Amount = 101,
                        Frequency = EFrequency.None,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                }
            );

            mockBIS.Setup(x => x.DeleteBudgetIncome(It.IsAny<int>())).Returns(
                new BudgetIncome()
                {
                    Id = 1,
                    BudgetId = 1,
                    IncomeId = 1,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                }
            );

            mockBIS.Setup(x => x.GetAllBudgetIncomes()).Returns(
                new List<BudgetIncome>()
                {
                    new BudgetIncome()
                    {
                        Id = 1,
                        BudgetId = 1,
                        IncomeId = 1,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    },
                    new BudgetIncome()
                    {
                        Id = 1,
                        BudgetId = 2,
                        IncomeId = 1,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }

                }
            );

            var incomeLogic = new IncomeLogic(mockIS.Object, mockBIS.Object, mockBS.Object);

            var expected = new UserIncome()
            {
                Id = 1,
                Amount = 101,
                BudgetId = 1,
                Frequency = EFrequency.None,
            };

            var actual = incomeLogic.DeleteBudgetIncome(1, 1);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}