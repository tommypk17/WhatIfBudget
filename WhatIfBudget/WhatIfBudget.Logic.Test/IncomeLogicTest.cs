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
        public void GetUserIncomes_CollectionAreEqual()
        {
            var mock = new Mock<IIncomeService>();
            mock.Setup(x => x.GetAllIncome()).Returns(
                (IList<Income>) new List<Income>()
                {
                    new Income() { Id = 1, Amount = 100, Frequency = 0, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Income() { Id = 2, Amount = 100, Frequency = 0, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Income() { Id = 3, Amount = 100, Frequency = 0, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Income() { Id = 4, Amount = 100, Frequency = 0, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Income() { Id = 5, Amount = 100, Frequency = 0, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Income() { Id = 6, Amount = 100, Frequency = 0, UserId = Guid.NewGuid(), CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue }
                }
                );
            var incomeLogic = new IncomeLogic(mock.Object);

            var expected = new List<UserIncome>()
            {
                new UserIncome() {Id = 1, Amount = 100, Frequency = 0},
                new UserIncome() {Id = 2, Amount = 100, Frequency = 0},
                new UserIncome() {Id = 3, Amount = 100, Frequency = 0},
                new UserIncome() {Id = 4, Amount = 100, Frequency = 0},
                new UserIncome() {Id = 5, Amount = 100, Frequency = 0}
            };

            var actual = incomeLogic.GetUserIncomes(Guid.Empty);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void AddUserIncome_CollectionAreEqual()
        {
            var mock = new Mock<IIncomeService>();

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
            var incomeLogic = new IncomeLogic(mock.Object);

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
            var incomeLogic = new IncomeLogic(mock.Object);

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

    }
}