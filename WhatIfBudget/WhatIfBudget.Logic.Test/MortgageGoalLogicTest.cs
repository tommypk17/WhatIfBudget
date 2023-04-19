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
                    TotalBalance = 25000,
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
                {0, 175000.00 },  //25,000
                {1, 178072.92 },  //22696.88
                {2, 181158.09 },  //20373.60
                {3, 184255.55 },  //18029.99
                {4, 187365.36 },  //15665.88
                {5, 190487.57 },  //13281.08
                {6, 193622.22 },  //10875.41
                {7, 196769.37 },  //8448.7
                {8, 199929.07 },  //6000.75
                {9, 203101.36 },  //3531.38
                {10, 206286.30 }, //1040.41
                {11, 207456.80 }, // 0.0
            };

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
                    TotalBalance = 25000,
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
                {0, new List<Double>() { 25000.00 , 0.0, 0.0 } },
                {1, new List<Double>() { 22593.75 , 2406.25, 93.75 } },
                {2, new List<Double>() { 20177.47 , 4822.53, 177.47 } },
                {3, new List<Double>() { 17751.13 , 7248.87, 251.13 } },
                {4, new List<Double>() { 15314.68 , 9685.32, 314.68 } },
                {5, new List<Double>() { 12868.07 , 12131.93, 368.07 } },
                {6, new List<Double>() { 10411.27 , 14588.73, 411.27 } },
                {7, new List<Double>() { 7944.23 , 17055.77, 444.23 } },
                {8, new List<Double>() { 5466.91 , 19533.09, 466.91 } },
                {9, new List<Double>() { 2979.27 , 22020.73, 479.27 } },
                {10, new List<Double>() { 481.27 , 24518.73, 481.27 } },
                {11, new List<Double>() { 0.0 , 25000, 481.27 } }
            };

            var actual = mortgageGoalLogic.GetAmortizationTable(1);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}