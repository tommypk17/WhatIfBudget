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
                    YearsToTarget = 5,
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
                    MonthsToTarget = 6
                });
            mockDGL.Setup(x => x.GetDebtTotals(It.IsAny<int>())).Returns(
                new DebtGoalTotals()
                {
                    MonthsToPayoff = 12
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
                    MonthsToPayoff = 48
                });


            var investmentGoalLogic = new InvestmentGoalLogic(mockIGS.Object, mockIS.Object,
                                                              mockSGS.Object, mockDGS.Object,
                                                              mockDS.Object, mockMGS.Object,
                                                              mockSGL.Object, mockDGL.Object,
                                                              mockMGL.Object);

            var expected = new Dictionary<int, double>
            {
                { 0, 12000.0 },
                { 1, 26560.93 },
                { 2, 49759.10 },
                { 3, 75824.63 },
                { 4, 104762.68 },
                { 5, 157180.49 }
            };

            var actual = investmentGoalLogic.GetBalanceOverTime(1);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}