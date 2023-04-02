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
    public class InvestmentGoalLogicTest
    {
        public InvestmentGoalLogicTest()
        {

        }


        [TestMethod]
        public void GetInvestmentGoal()
        {
            var mockIGS = new Mock<IInvestmentGoalService>();
            var mockIS = new Mock<IInvestmentService>();

            mockIGS.Setup(x => x.GetInvestmentGoal(It.IsAny<int>())).Returns(
                new InvestmentGoal()
                {
                    Id = 1,
                    AnnualReturnRate_Percent = 10.5,
                    YearsToTarget = 40,
                    AdditionalBudgetAllocation = 540.0,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue }
                );
            mockIS.Setup(x => x.GetInvestmentsByInvestmentGoalId(It.IsAny<int>())).Returns(
    (IList<Investment>)new List<Investment>()
    {
                    new Investment()
                    {
                        Id = 1,
                        Name = "test",
                        CurrentBalance = 10000.0,
                        MonthlyEmployerContribution = 100.0,
                        MonthlyPersonalContribution = 50.0,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    },
                    new Investment()
                    {
                        Id = 2,
                        Name = "test2",
                        CurrentBalance = 2000.0,
                        MonthlyEmployerContribution = 150.0,
                        MonthlyPersonalContribution = 0.0,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
    });

            var investmentGoalLogic = new InvestmentGoalLogic(mockIGS.Object, mockIS.Object);

            var expected = new UserInvestmentGoal() 
            {
                Id = 1,
                AnnualReturnRate_Percent = 10.5,
                YearsToTarget = 40,
                AdditionalBudgetAllocation = 540.0,
            };

            var actual = investmentGoalLogic.GetInvestmentGoal(1);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void ModifyInvestmentGoal()
        {
            var mockIGS = new Mock<IInvestmentGoalService>();
            var mockIS = new Mock<IInvestmentService>();

            mockIGS.Setup(x => x.UpdateInvestmentGoal(It.IsAny<InvestmentGoal>())).Returns(
                    new InvestmentGoal()
                    {
                        Id = 1,
                        AnnualReturnRate_Percent = 10.5,
                        YearsToTarget = 40,
                        AdditionalBudgetAllocation = 540.0,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                );

            var investmentGoalLogic = new InvestmentGoalLogic(mockIGS.Object, mockIS.Object);

            var expected = new UserInvestmentGoal()
            {
                Id = 1,
                AnnualReturnRate_Percent = 10.5,
                YearsToTarget = 40,
                AdditionalBudgetAllocation = 540.0,
            };

            var actual = investmentGoalLogic.ModifyUserInvestmentGoal(new UserInvestmentGoal()
            {
                Id = 1,
                AnnualReturnRate_Percent = 10.5,
                YearsToTarget = 40,
                AdditionalBudgetAllocation = 540.0,
            });

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Get_InvestmentTotals()
        {
            var mockIGS = new Mock<IInvestmentGoalService>();
            var mockIS = new Mock<IInvestmentService>();

            mockIGS.Setup(x => x.GetInvestmentGoal(It.IsAny<int>())).Returns(
                new InvestmentGoal()
                {
                    Id = 1,
                    AnnualReturnRate_Percent = 10.5,
                    YearsToTarget = 5,
                    AdditionalBudgetAllocation = 540.0,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                }
                );
            mockIS.Setup(x => x.GetInvestmentsByInvestmentGoalId(It.IsAny<int>())).Returns(
                (IList<Investment>)new List<Investment>()
                {
                    new Investment()
                    {
                        Id = 1,
                        Name = "test",
                        CurrentBalance = 10000.0,
                        MonthlyEmployerContribution = 100.0,
                        MonthlyPersonalContribution = 50.0,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    },
                    new Investment()
                    {
                        Id = 2,
                        Name = "test2",
                        CurrentBalance = 2000.0,
                        MonthlyEmployerContribution = 150.0,
                        MonthlyPersonalContribution = 0.0,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                });

            var investmentGoalLogic = new InvestmentGoalLogic(mockIGS.Object, mockIS.Object);

            var expected = new InvestmentGoalTotals()
            {
                BalanceAtTarget = 86729.87,
                TotalInterestAccrued = 24329.87,
                AddedDueToContribution = 43985.91
            };

            var actual = investmentGoalLogic.GetInvestmentTotals(1);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void InvestmentBalanceOverTime()
        {
            var mockIGS = new Mock<IInvestmentGoalService>();
            var mockIS = new Mock<IInvestmentService>();

            mockIGS.Setup(x => x.GetInvestmentGoal(It.IsAny<int>())).Returns(
                new InvestmentGoal()
                {
                    Id = 1,
                    AnnualReturnRate_Percent = 10.5,
                    YearsToTarget = 5,
                    AdditionalBudgetAllocation = 540.0,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                }
                );
            mockIS.Setup(x => x.GetInvestmentsByInvestmentGoalId(It.IsAny<int>())).Returns(
                (IList<Investment>)new List<Investment>()
                {
                    new Investment()
                    {
                        Id = 1,
                        Name = "test",
                        CurrentBalance = 10000.0,
                        MonthlyEmployerContribution = 100.0,
                        MonthlyPersonalContribution = 50.0,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    },
                    new Investment()
                    {
                        Id = 2,
                        Name = "test2",
                        CurrentBalance = 2000.0,
                        MonthlyEmployerContribution = 150.0,
                        MonthlyPersonalContribution = 0.0,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                });

            var investmentGoalLogic = new InvestmentGoalLogic(mockIGS.Object, mockIS.Object);

            var expected = new Dictionary<int, double>
            {
                { 0, 12000.0 },
                { 1, 23994.54 },
                { 2, 37310.94 },
                { 3, 52094.83 },
                { 4, 68507.97 },
                { 5, 86729.87 }
            };

            var actual = investmentGoalLogic.GetBalanceOverTime(1);

            actual.Should().BeEquivalentTo(expected);
        }

    }
}