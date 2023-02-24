using FluentAssertions;
using Moq;
using WhatIfBudget.Services.Interfaces;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Services;
using WhatIfBudget.Logic.Models;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Common.Enumerations;

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
            mockIS.Setup(x => x.GetAllIncomes()).Returns(
                (IList<Income>) new List<Income>()
                {
                    new Income() { Id = 1, Amount = 100, Frequency = EFrequency.None, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Income() { Id = 2, Amount = 100, Frequency = EFrequency.None, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Income() { Id = 3, Amount = 100, Frequency = EFrequency.None, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Income() { Id = 4, Amount = 100, Frequency = EFrequency.None, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Income() { Id = 5, Amount = 100, Frequency = EFrequency.None, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Income() { Id = 6, Amount = 100, Frequency = EFrequency.None, UserId = Guid.NewGuid(), CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue }
                }
                );
            var mockBIS = new Mock<IBudgetIncomeService>();
            mockBIS.Setup(x => x.GetAllBudgetIncomes()).Returns(
                (IList<BudgetIncome>)new List<BudgetIncome>()
                {
                    new BudgetIncome() { Id = 1, BudgetId = 1, IncomeId =  1},
                    new BudgetIncome() { Id = 2, BudgetId = 1, IncomeId =  2},
                    new BudgetIncome() { Id = 3, BudgetId = 1, IncomeId =  3},
                    new BudgetIncome() { Id = 4, BudgetId = 1, IncomeId =  4},
                    new BudgetIncome() { Id = 5, BudgetId = 1, IncomeId =  5},
                    new BudgetIncome() { Id = 6, BudgetId = 2, IncomeId =  6},
                    new BudgetIncome() { Id = 7, BudgetId = 2, IncomeId =  6}
                }
                );
            var incomeLogic = new IncomeLogic(mockIS.Object, mockBIS.Object);

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
        public void AddUserIncome_CollectionAreEqual()
        {
            var mock = new Mock<IIncomeService>();
            var mock2 = new Mock<IBudgetIncomeService>();

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

            var incomeLogic = new IncomeLogic(mock.Object, mock2.Object);

            var expected = new UserIncome()
            {
                Id = 1,
                Amount = 100,
                Frequency = EFrequency.None,
            };

            var actual = incomeLogic.AddUserIncome(Guid.Empty, new UserIncome()
            {
                Id = 1,
                Amount = 100,
                Frequency = EFrequency.None,
            });

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void ModifyUserIncome_CollectionAreEqual()
        {
            var mock = new Mock<IIncomeService>();
            var mock2 = new Mock<IBudgetIncomeService>();

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
            var incomeLogic = new IncomeLogic(mock.Object, mock2.Object);

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
            var mock = new Mock<IIncomeService>();
            var mock2 = new Mock<IBudgetIncomeService>();

            mock.Setup(x => x.DeleteIncome(It.IsAny<int>())).Returns(
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

            mock.Setup(x => x.GetAllIncomes()).Returns(
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

            mock2.Setup(x => x.DeleteBudgetIncome(It.IsAny<int>())).Returns(
                new BudgetIncome()
                {
                    Id = 1,
                    BudgetId = 1,
                    IncomeId = 1,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                }
            );

            mock2.Setup(x => x.GetAllBudgetIncomes()).Returns(
                new List<BudgetIncome>()
                {
                    new BudgetIncome()
                    {
                        Id = 1,
                        BudgetId = 1,
                        IncomeId = 1,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                }
            );

            var incomeLogic = new IncomeLogic(mock.Object, mock2.Object);

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