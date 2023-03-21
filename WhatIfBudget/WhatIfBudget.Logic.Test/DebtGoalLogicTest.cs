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
    public class DebtGoalLogicTest
    {
        public DebtGoalLogicTest()
        {

        }

        [TestMethod]
        public void GetDebtGoal()
        {
            var mockDGS = new Mock<IDebtGoalService>();
            var mockDS = new Mock<IDebtService>();

            mockDGS.Setup(x => x.GetDebtGoal(It.IsAny<int>())).Returns(
                new DebtGoal()
                {
                    Id = 1,
                    AdditionalBudgetAllocation = 540.0,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue }
                );

            var debtGoalLogic = new DebtGoalLogic(mockDGS.Object, mockDS.Object);

            var expected = new UserDebtGoal() 
            {
                Id = 1,
                AdditionalBudgetAllocation = 540.0,
            };

            var actual = debtGoalLogic.GetDebtGoal(1);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void ModifyDebtGoal()
        {
            var mockIGS = new Mock<IDebtGoalService>();
            var mockIS = new Mock<IDebtService>();

            mockIGS.Setup(x => x.UpdateDebtGoal(It.IsAny<DebtGoal>())).Returns(
                    new DebtGoal()
                    {
                        Id = 1,
                        AdditionalBudgetAllocation = 540.0,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                );

            var debtGoalLogic = new DebtGoalLogic(mockIGS.Object, mockIS.Object);

            var expected = new UserDebtGoal()
            {
                Id = 1,
                AdditionalBudgetAllocation = 540.0,
            };

            var actual = debtGoalLogic.ModifyUserDebtGoal(new UserDebtGoal()
            {
                Id = 1,
                AdditionalBudgetAllocation = 540.0,
            });

            actual.Should().BeEquivalentTo(expected);
        }
    }
}