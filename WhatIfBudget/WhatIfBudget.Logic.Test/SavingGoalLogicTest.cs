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
    public class SavingGoalLogicTest
    {
        public SavingGoalLogicTest()
        {

        }


        [TestMethod]
        public void GetSavingGoal()
        {
            var mockSGS = new Mock<ISavingGoalService>();
            mockSGS.Setup(x => x.GetSavingGoal(It.IsAny<int>())).Returns(
                new SavingGoal()
                {
                    Id = 1,
                    CurrentBalance = 1000.25,
                    TargetBalance = 20000.0,
                    AnnualReturnRate_Percent = 1.5,
                    AdditionalBudgetAllocation = 540.0,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue }
                );

            var savingGoalLogic = new SavingGoalLogic(mockSGS.Object);

            var expected = new UserSavingGoal() 
            {
                Id = 1,
                CurrentBalance = 1000.25,
                TargetBalance = 20000.0,
                AnnualReturnRate_Percent = 1.5,
                AdditionalBudgetAllocation = 540.0
            };

            var actual = savingGoalLogic.GetSavingGoal(1);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void ModifySavingGoal()
        {
            var mockSGS = new Mock<ISavingGoalService>();

            mockSGS.Setup(x => x.ModifySavingGoal(It.IsAny<SavingGoal>())).Returns(
                    new SavingGoal()
                    {
                        Id = 1,
                        CurrentBalance = 4000.25,
                        TargetBalance = 30000.0,
                        AnnualReturnRate_Percent = 1.1,
                        AdditionalBudgetAllocation = 540.0,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                );
            var savingGoalLogic = new SavingGoalLogic(mockSGS.Object);

            var expected = new UserSavingGoal()
            {
                Id = 1,
                CurrentBalance = 4000.25,
                TargetBalance = 30000.0,
                AnnualReturnRate_Percent = 1.1,
                AdditionalBudgetAllocation = 540.0
            };

            var actual = savingGoalLogic.ModifyUserSavingGoal(new UserSavingGoal()
            {
                Id = 1,
                CurrentBalance = 4000.25,
                TargetBalance = 30000.0,
                AnnualReturnRate_Percent = 1.1,
                AdditionalBudgetAllocation = 540.0,
            });

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void SavingTimeToTarget()
        {
            var mockSGS = new Mock<ISavingGoalService>();

            mockSGS.Setup(x => x.GetSavingGoal(It.IsAny<int>())).Returns(
                new SavingGoal()
                {
                    Id = 1,
                    CurrentBalance = 1000.25,
                    TargetBalance = 3000.0,
                    AnnualReturnRate_Percent = 1.5,
                    AdditionalBudgetAllocation = 540.0,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                }
                );

            var savingGoalLogic = new SavingGoalLogic(mockSGS.Object);

            var expected = 4;

            var actual = savingGoalLogic.GetTimeToTarget(1);

            actual.Should().Be(expected);
        }


    [TestMethod]
        public void SavingBalanceOverTime()
        {
            var mockSGS = new Mock<ISavingGoalService>();

            mockSGS.Setup(x => x.GetSavingGoal(It.IsAny<int>())).Returns(
                new SavingGoal()
                {
                    Id = 1,
                    CurrentBalance = 1000.25,
                    TargetBalance = 3000.0,
                    AnnualReturnRate_Percent = 1.5,
                    AdditionalBudgetAllocation = 540.0,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                }
                );

            var savingGoalLogic = new SavingGoalLogic(mockSGS.Object);

            var expected = new Dictionary<int, double>
            {
                { 0, 1000.25 },
                { 1, 1542.18 },
                { 2, 2084.78 },
                { 3, 2628.06 },
                { 4, 3000.00 }
            };

            var actualGoal = savingGoalLogic.GetSavingGoal(1);
            var actual = savingGoalLogic.GetBalanceOverTime(1);

            actual.Should().BeEquivalentTo(expected);
        }

    }
}