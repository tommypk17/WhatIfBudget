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
            var mockSGS = new Mock<ISavingGoalService>();
            var mockDGS = new Mock<IDebtGoalService>();
            var mockDS = new Mock<IDebtService>();
            var mockMGS = new Mock<IMortgageGoalService>();
            var mockSGL = new Mock<ISavingGoalLogic>();
            var mockDGL = new Mock<IDebtGoalLogic>();
            var mockMGL = new Mock<IMortgageGoalLogic>();

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

            var investmentGoalLogic = new InvestmentGoalLogic(mockIGS.Object, mockIS.Object,
                                                              mockSGS.Object, mockDGS.Object,
                                                              mockDS.Object,  mockMGS.Object,
                                                              mockSGL.Object, mockDGL.Object,
                                                              mockMGL.Object);

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
            var mockSGS = new Mock<ISavingGoalService>();
            var mockDGS = new Mock<IDebtGoalService>();
            var mockDS = new Mock<IDebtService>();
            var mockMGS = new Mock<IMortgageGoalService>();
            var mockSGL = new Mock<ISavingGoalLogic>();
            var mockDGL = new Mock<IDebtGoalLogic>();
            var mockMGL = new Mock<IMortgageGoalLogic>();

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

            var investmentGoalLogic = new InvestmentGoalLogic(mockIGS.Object, mockIS.Object,
                                                              mockSGS.Object, mockDGS.Object,
                                                              mockDS.Object, mockMGS.Object,
                                                              mockSGL.Object, mockDGL.Object,
                                                              mockMGL.Object);

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
            var mockSGS = new Mock<ISavingGoalService>();
            var mockDGS = new Mock<IDebtGoalService>();
            var mockDS = new Mock<IDebtService>();
            var mockMGS = new Mock<IMortgageGoalService>();
            var mockSGL = new Mock<ISavingGoalLogic>();
            var mockDGL = new Mock<IDebtGoalLogic>();
            var mockMGL = new Mock<IMortgageGoalLogic>();

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

            var investmentGoalLogic = new InvestmentGoalLogic(mockIGS.Object, mockIS.Object,
                                                              mockSGS.Object, mockDGS.Object,
                                                              mockDS.Object, mockMGS.Object,
                                                              mockSGL.Object, mockDGL.Object,
                                                              mockMGL.Object);

            var expected = new InvestmentGoalTotals()
            {
                BalanceAtTarget = 88336.11,
                TotalInterestAccrued = 25096.11,
                AddedDueToContribution = 43662.69
            };

            var actual = investmentGoalLogic.GetInvestmentTotals(1);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void InvestmentBalanceOverTime()
        {
            var mockIGS = new Mock<IInvestmentGoalService>();
            var mockIS = new Mock<IInvestmentService>();
            var mockSGS = new Mock<ISavingGoalService>();
            var mockDGS = new Mock<IDebtGoalService>();
            var mockDS = new Mock<IDebtService>();
            var mockMGS = new Mock<IMortgageGoalService>();
            var mockSGL = new Mock<ISavingGoalLogic>();
            var mockDGL = new Mock<IDebtGoalLogic>();
            var mockMGL = new Mock<IMortgageGoalLogic>();

            mockIGS.Setup(x => x.GetInvestmentGoal(It.IsAny<int>())).Returns(
                new InvestmentGoal()
                {
                    Id = 1,
                    AnnualReturnRate_Percent = 10.5,
                    YearsToTarget = 1,
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

            var investmentGoalLogic = new InvestmentGoalLogic(mockIGS.Object, mockIS.Object,
                                                              mockSGS.Object, mockDGS.Object,
                                                              mockDS.Object, mockMGS.Object,
                                                              mockSGL.Object, mockDGL.Object,
                                                              mockMGL.Object);

            var expected = new Dictionary<int, double>
            {
                { 0, 12000.0 },
                { 1, 12952.35 },
                { 2, 13913.03 },
                { 3, 14882.12 },
                { 4, 15859.69 },
                { 5, 16845.81 },
                { 6, 17840.56 },
                { 7, 18844.01 },
                { 8, 19856.25 },
                { 9, 20877.34 },
                { 10, 21907.37 },
                { 11, 22946.41 },
                { 12, 23994.54 }
            };

            var actual = investmentGoalLogic.GetBalanceOverTime(1);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void RolloverInvestmentBalanceOverTime()
        {
            var mockIGS = new Mock<IInvestmentGoalService>();
            var mockIS = new Mock<IInvestmentService>();
            var mockSGS = new Mock<ISavingGoalService>();
            var mockDGS = new Mock<IDebtGoalService>();
            var mockDS = new Mock<IDebtService>();
            var mockMGS = new Mock<IMortgageGoalService>();
            var mockSGL = new Mock<ISavingGoalLogic>();
            var mockDGL = new Mock<IDebtGoalLogic>();
            var mockMGL = new Mock<IMortgageGoalLogic>();

            mockIGS.Setup(x => x.GetInvestmentGoal(It.IsAny<int>())).Returns(
                new InvestmentGoal()
                {
                    Id = 1,
                    AnnualReturnRate_Percent = 10.5,
                    YearsToTarget = 1,
                    RolloverCompletedGoals = true,
                    AdditionalBudgetAllocation = 540.0,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue,
                    Budget = new Budget()
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
            mockSGS.Setup(x => x.GetSavingGoal(It.IsAny<int>())).Returns(
                new SavingGoal()
                {
                    AdditionalBudgetAllocation = 500.0
                });
            mockDGS.Setup(x => x.GetDebtGoal(It.IsAny<int>())).Returns(
                new DebtGoal()
                {
                    AdditionalBudgetAllocation = 100.0
                });
            mockMGS.Setup(x => x.GetMortgageGoal(It.IsAny<int>())).Returns(
                new MortgageGoal()
                {
                    MonthlyPayment = 1500.0,
                    AdditionalBudgetAllocation = 250.0
                });
            mockSGL.Setup(x => x.GetSavingTotals(It.IsAny<int>())).Returns(
                new SavingGoalTotals()
                {
                    MonthsToTarget = 3
                });
            mockDGL.Setup(x => x.GetDebtTotals(It.IsAny<int>())).Returns(
                new DebtGoalTotals()
                {
                    MonthsToPayoff = 6
                });
            mockDS.Setup(x => x.GetDebtsByDebtGoalId(It.IsAny<int>())).Returns(
                (IList<Debt>)new List<Debt>()
                {
                    new Debt()
                    {
                        MinimumPayment = 80
                    },
                    new Debt()
                    {
                        MinimumPayment = 100
                    }
                });
            mockMGL.Setup(x => x.GetMortgageTotals(It.IsAny<int>())).Returns(
                new MortgageGoalTotals()
                {
                    MonthsToPayoff = 9
                });


            var investmentGoalLogic = new InvestmentGoalLogic(mockIGS.Object, mockIS.Object,
                                                              mockSGS.Object, mockDGS.Object,
                                                              mockDS.Object, mockMGS.Object,
                                                              mockSGL.Object, mockDGL.Object,
                                                              mockMGL.Object);

            var expected = new Dictionary<int, double>
            {
                { 0, 12000.0 },
                { 1, 12952.35 },
                { 2, 13913.03 },
                { 3, 14882.12 },
                { 4, 15859.69 },
                { 5, 17350.19 },
                { 6, 18853.73 },
                { 7, 20370.43 },
                { 8, 22182.85 },
                { 9, 24011.12 },
                { 10, 25855.39 },
                { 11, 29481.11 },
                { 12, 33138.56 }
            };

            var actual = investmentGoalLogic.GetBalanceOverTime(1);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}