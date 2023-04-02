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
    public class MortgageGoalLogicTest
    {
        public MortgageGoalLogicTest()
        {

        }


        [TestMethod]
        public void GetMortgageGoal()
        {
            var mockSGS = new Mock<IMortgageGoalService>();
            mockSGS.Setup(x => x.GetMortgageGoal(It.IsAny<int>())).Returns(
                new MortgageGoal()
                {
                    Id = 4,
                    TotalBalance = 100000.0,
                    InterestRate_Percent = 5.0,
                    MonthlyPayment = 2000.0,
                    EstimatedCurrentValue = 200000.0,
                    AdditionalBudgetAllocation = 500,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue }
                );

            var mortgageGoalLogic = new MortgageGoalLogic(mockSGS.Object);

            var expected = new UserMortgageGoal() 
            {
                Id = 4,
                TotalBalance = 100000.0,
                InterestRate_Percent = 5.0,
                MonthlyPayment = 2000.0,
                EstimatedCurrentValue = 200000.0,
                AdditionalBudgetAllocation = 500
            };

            var actual = mortgageGoalLogic.GetMortgageGoal(1);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void ModifyMortgageGoal()
        {
            var mockSGS = new Mock<IMortgageGoalService>();

            mockSGS.Setup(x => x.ModifyMortgageGoal(It.IsAny<MortgageGoal>())).Returns(
                    new MortgageGoal()
                    {
                        Id = 4,
                        TotalBalance = 100000.0,
                        InterestRate_Percent = 5.0,
                        MonthlyPayment = 2000.0,
                        EstimatedCurrentValue = 200000.0,
                        AdditionalBudgetAllocation = 500,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                );
            var mortgageGoalLogic = new MortgageGoalLogic(mockSGS.Object);

            var expected = new UserMortgageGoal()
            {
                Id = 4,
                TotalBalance = 100000.0,
                InterestRate_Percent = 5.0,
                MonthlyPayment = 2000.0,
                EstimatedCurrentValue = 200000.0,
                AdditionalBudgetAllocation = 500,
            };

            var actual = mortgageGoalLogic.ModifyUserMortgageGoal(new UserMortgageGoal()
            {
                Id = 4,
                TotalBalance = 100000.0,
                InterestRate_Percent = 5.0,
                MonthlyPayment = 2000.0,
                EstimatedCurrentValue = 200000.0,
                AdditionalBudgetAllocation = 500,
            });

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Get_MortgageTotals()
        {
            var mockSGS = new Mock<IMortgageGoalService>();

            mockSGS.Setup(x => x.GetMortgageGoal(It.IsAny<int>())).Returns(
                new MortgageGoal()
                {
                    Id = 4,
                    TotalBalance = 100000.0,
                    InterestRate_Percent = 5.0,
                    MonthlyPayment = 2000.0,
                    EstimatedCurrentValue = 200000.0,
                    AdditionalBudgetAllocation = 500,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                }
                );

            var mortgageGoalLogic = new MortgageGoalLogic(mockSGS.Object);

            var expected = new MortgageGoalTotals()
            {
                MonthsToPayoff = 44,
                TotalInterestAccrued = 9123.11,
                TotalCostToPayoff= 109123.11,
                AllocationSavings = 2720.81
            };

            var actual = mortgageGoalLogic.GetMortgageTotals(1);

            actual.Should().BeEquivalentTo(expected);
        }


        [TestMethod]
        public void Get_MortgageNetOverTime()
        {
            var mockSGS = new Mock<IMortgageGoalService>();

            mockSGS.Setup(x => x.GetMortgageGoal(It.IsAny<int>())).Returns(
                new MortgageGoal()
                {
                    Id = 4,
                    TotalBalance = 100000.0,
                    InterestRate_Percent = 5.0,
                    MonthlyPayment = 2000.0,
                    EstimatedCurrentValue = 200000.0,
                    AdditionalBudgetAllocation = 500,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                }
                );

            var mortgageGoalLogic = new MortgageGoalLogic(mockSGS.Object);

            var expected = new Dictionary<int, double>
            {
                {0, 100000.0 },  //100000.00
                {1, 133857.16 }, //74291.16
                {2, 169361.63 }, //47266.99
                {3, 206594.19 }, //18860.22
                {4, 231537.13 }  // 0.0
            };

            var actualGoal = mortgageGoalLogic.GetMortgageGoal(1);
            var actual = mortgageGoalLogic.GetNetValueOverTime(1);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Get_AmortizationOverTime()
        {
            var mockSGS = new Mock<IMortgageGoalService>();

            mockSGS.Setup(x => x.GetMortgageGoal(It.IsAny<int>())).Returns(
                new MortgageGoal()
                {
                    Id = 4,
                    TotalBalance = 100000.0,
                    InterestRate_Percent = 5.0,
                    MonthlyPayment = 2000.0,
                    EstimatedCurrentValue = 200000.0,
                    AdditionalBudgetAllocation = 500,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                }
                );

            var mortgageGoalLogic = new MortgageGoalLogic(mockSGS.Object);

            var expected = new Dictionary<int, List<double>>
            {
                {0, new List<Double>() { 100000.0 , 0.0, 0.0 } },
                {1, new List<Double>() { 74291.16 , 25708.84, 4291.16 } },
                {2, new List<Double>() { 47266.99 , 52733.01, 7266.99 } },
                {3, new List<Double>() { 18860.22 , 81139.78, 8860.22 } },
                {4, new List<Double>() { 0.0 , 100000.0, 9123.11 } }
            };

            var actualGoal = mortgageGoalLogic.GetMortgageGoal(1);
            var actual = mortgageGoalLogic.GetAmortizationTable(1);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}