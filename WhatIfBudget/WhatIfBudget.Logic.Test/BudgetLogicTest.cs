using FluentAssertions;
using Moq;
using WhatIfBudget.Services.Interfaces;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Services;
using WhatIfBudget.Logic.Models;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Common.Enumerations;
using System.Collections.Generic;
using System;

namespace WhatIfBudget.Logic.Test
{
    [TestClass]
    public class BudgetLogicTest
    {
        public BudgetLogicTest()
        {

        }

        [TestMethod]
        public void GetAllUserBudgets_CollectionAreEqual()
        {
            var mockBS = new Mock<IBudgetService>();
            var mockIS = new Mock<IIncomeService>();
            var mockES = new Mock<IExpenseService>();
            var mockBIS = new Mock<IBudgetIncomeService>();
            var mockBES = new Mock<IBudgetExpenseService>();
            var mockIGS = new Mock<IInvestmentGoalService>();
            var mockInvS = new Mock<IInvestmentService>();
            var mockIGIS = new Mock<IInvestmentGoalInvestmentService>();
            var mockSGS = new Mock<ISavingGoalService>();
            var mockDGS = new Mock<IDebtGoalService>();
            var mockDS = new Mock<IDebtService>();
            var mockDGDS = new Mock<IDebtGoalDebtService>();
            var mockMGS = new Mock<IMortgageGoalService>();
            var mockIL = new Mock<IIncomeLogic>();
            var mockEL = new Mock<IExpenseLogic>();
            var mockSGL = new Mock<ISavingGoalLogic>();
            var mockDGL = new Mock<IDebtGoalLogic>();
            var mockMGL = new Mock<IMortgageGoalLogic>();
            var mockIGL = new Mock<IInvestmentGoalLogic>();

            mockBS.Setup(x => x.GetAllBudgets()).Returns(
                (IList<Budget>)new List<Budget>()
                {
                    new Budget()
                    {
                        Id = 1,
                        Name = "test",
                        UserId = Guid.Empty,
                        SavingGoalId = 1,
                        DebtGoalId = 1,
                        MortgageGoalId = 1,
                        InvestmentGoalId = 1,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    },
                    new Budget()
                    {
                        Id = 2,
                        Name = "test2",
                        UserId = Guid.Empty,
                        SavingGoalId = 2,
                        DebtGoalId = 2,
                        MortgageGoalId = 2,
                        InvestmentGoalId = 2,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    },
                    new Budget()
                    {
                        Id = 3,
                        Name = "test3",
                        UserId = Guid.Empty,
                        SavingGoalId = 3,
                        DebtGoalId = 3,
                        MortgageGoalId = 3,
                        InvestmentGoalId = 3,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                });

            var budgetLogic = new BudgetLogic(mockBS.Object, mockIS.Object,
                                              mockES.Object, mockBIS.Object,
                                              mockBES.Object, mockIGS.Object,
                                              mockInvS.Object, mockIGIS.Object,
                                              mockMGS.Object, mockDGS.Object,
                                              mockDS.Object, mockDGDS.Object,
                                              mockSGS.Object, mockIL.Object,
                                              mockEL.Object, mockSGL.Object,
                                              mockDGL.Object, mockMGL.Object,
                                              mockIGL.Object
                                              );

            var expected = new List<UserBudget>()
            {
                    new UserBudget()
                    {
                        Id = 1,
                        Name = "test",
                        SavingGoalId = 1,
                        DebtGoalId = 1,
                        MortgageGoalId = 1,
                        InvestmentGoalId = 1,
                    },
                    new UserBudget()
                    {
                        Id = 2,
                        Name = "test2",
                        SavingGoalId = 2,
                        DebtGoalId = 2,
                        MortgageGoalId = 2,
                        InvestmentGoalId = 2,
                    },
                    new UserBudget()
                    {
                        Id = 3,
                        Name = "test3",
                        SavingGoalId = 3,
                        DebtGoalId = 3,
                        MortgageGoalId = 3,
                        InvestmentGoalId = 3,
                    }
            };

            var actual = budgetLogic.GetUserBudgets(Guid.Empty);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void GetBudgetById()
        {
            var mockBS = new Mock<IBudgetService>();
            var mockIS = new Mock<IIncomeService>();
            var mockES = new Mock<IExpenseService>();
            var mockBIS = new Mock<IBudgetIncomeService>();
            var mockBES = new Mock<IBudgetExpenseService>();
            var mockIGS = new Mock<IInvestmentGoalService>();
            var mockInvS = new Mock<IInvestmentService>();
            var mockIGIS = new Mock<IInvestmentGoalInvestmentService>();
            var mockSGS = new Mock<ISavingGoalService>();
            var mockDGS = new Mock<IDebtGoalService>();
            var mockDS = new Mock<IDebtService>();
            var mockDGDS = new Mock<IDebtGoalDebtService>();
            var mockMGS = new Mock<IMortgageGoalService>();
            var mockIL = new Mock<IIncomeLogic>();
            var mockEL = new Mock<IExpenseLogic>();
            var mockSGL = new Mock<ISavingGoalLogic>();
            var mockDGL = new Mock<IDebtGoalLogic>();
            var mockMGL = new Mock<IMortgageGoalLogic>();
            var mockIGL = new Mock<IInvestmentGoalLogic>();


            mockBS.Setup(x => x.GetBudget(It.IsAny<int>())).Returns(
                        new Budget()
                        {
                            Id = 1,
                            Name = "test",
                            UserId = Guid.Empty,
                            SavingGoalId = 1,
                            DebtGoalId = 1,
                            MortgageGoalId = 1,
                            InvestmentGoalId = 1,
                            CreatedOn = DateTime.MinValue,
                            UpdatedOn = DateTime.MinValue
                        });

            var budgetLogic = new BudgetLogic(mockBS.Object, mockIS.Object,
                                              mockES.Object, mockBIS.Object,
                                              mockBES.Object, mockIGS.Object,
                                              mockInvS.Object, mockIGIS.Object,
                                              mockMGS.Object, mockDGS.Object,
                                              mockDS.Object, mockDGDS.Object,
                                              mockSGS.Object, mockIL.Object,
                                              mockEL.Object, mockSGL.Object,
                                              mockDGL.Object, mockMGL.Object,
                                              mockIGL.Object
                                              );
            var expected = new UserBudget()
            {
                Id = 1,
                Name = "test",
                SavingGoalId = 1,
                DebtGoalId = 1,
                MortgageGoalId = 1,
                InvestmentGoalId = 1,
            };

            var actual = budgetLogic.GetBudget(1);

            actual.Should().BeEquivalentTo(expected);

        }

        [TestMethod]
        public void CreateBudget()
        {
            var mockBS = new Mock<IBudgetService>();
            var mockIS = new Mock<IIncomeService>();
            var mockES = new Mock<IExpenseService>();
            var mockBIS = new Mock<IBudgetIncomeService>();
            var mockBES = new Mock<IBudgetExpenseService>();
            var mockIGS = new Mock<IInvestmentGoalService>();
            var mockInvS = new Mock<IInvestmentService>();
            var mockIGIS = new Mock<IInvestmentGoalInvestmentService>();
            var mockSGS = new Mock<ISavingGoalService>();
            var mockDGS = new Mock<IDebtGoalService>();
            var mockDS = new Mock<IDebtService>();
            var mockDGDS = new Mock<IDebtGoalDebtService>();
            var mockMGS = new Mock<IMortgageGoalService>();
            var mockIL = new Mock<IIncomeLogic>();
            var mockEL = new Mock<IExpenseLogic>();
            var mockSGL = new Mock<ISavingGoalLogic>();
            var mockDGL = new Mock<IDebtGoalLogic>();
            var mockMGL = new Mock<IMortgageGoalLogic>();
            var mockIGL = new Mock<IInvestmentGoalLogic>();

            mockBS.Setup(x => x.AddNewBudget(It.IsAny<Budget>())).Returns(
                new Budget()
                {
                    Id = 1,
                    Name = "AddTest",
                    UserId = Guid.Empty,
                    SavingGoalId = 1,
                    DebtGoalId = 1,
                    MortgageGoalId = 1,
                    InvestmentGoalId = 1,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                });
            mockSGS.Setup(x => x.AddSavingGoal(It.IsAny<SavingGoal>())).Returns(
                new SavingGoal()
                {
                    Id = 1,
                    CurrentBalance = 0,
                    AnnualReturnRate_Percent = 0,
                    AdditionalBudgetAllocation = 0,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                });
            mockDGS.Setup(x => x.AddDebtGoal(It.IsAny<DebtGoal>())).Returns(
                new DebtGoal()
                {
                    Id = 1,
                    AdditionalBudgetAllocation = 0,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                });
            mockMGS.Setup(x => x.AddMortgageGoal(It.IsAny<MortgageGoal>())).Returns(
                new MortgageGoal()
                {
                    Id = 1,
                    TotalBalance = 0,
                    InterestRate_Percent = 0,
                    MonthlyPayment = 0,
                    AdditionalBudgetAllocation = 0,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                });
            mockIGS.Setup(x => x.AddInvestmentGoal(It.IsAny<InvestmentGoal>())).Returns(
                new InvestmentGoal()
                {
                    Id = 1,
                    AnnualReturnRate_Percent = 0,
                    YearsToTarget = 0,
                    AdditionalBudgetAllocation = 0,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                });

            var budgetLogic = new BudgetLogic(mockBS.Object, mockIS.Object,
                                              mockES.Object, mockBIS.Object,
                                              mockBES.Object, mockIGS.Object,
                                              mockInvS.Object, mockIGIS.Object,
                                              mockMGS.Object, mockDGS.Object,
                                              mockDS.Object, mockDGDS.Object,
                                              mockSGS.Object, mockIL.Object,
                                              mockEL.Object, mockSGL.Object,
                                              mockDGL.Object, mockMGL.Object,
                                              mockIGL.Object
                                              );

            var expected = new UserBudget()
            {
                Id = 1,
                Name = "AddTest",
                SavingGoalId = 1,
                DebtGoalId = 1,
                MortgageGoalId = 1,
                InvestmentGoalId = 1,
            };

            var actual = budgetLogic.CreateUserBudget(Guid.Empty, new UserBudget()
            {
                Name = "AddTest"
            });

            actual.Should().BeEquivalentTo(expected);

        }

        [TestMethod]
        public void ModifyBudget()
        {
            var mockBS = new Mock<IBudgetService>();
            var mockIS = new Mock<IIncomeService>();
            var mockES = new Mock<IExpenseService>();
            var mockBIS = new Mock<IBudgetIncomeService>();
            var mockBES = new Mock<IBudgetExpenseService>();
            var mockIGS = new Mock<IInvestmentGoalService>();
            var mockInvS = new Mock<IInvestmentService>();
            var mockIGIS = new Mock<IInvestmentGoalInvestmentService>();
            var mockSGS = new Mock<ISavingGoalService>();
            var mockDGS = new Mock<IDebtGoalService>();
            var mockDS = new Mock<IDebtService>();
            var mockDGDS = new Mock<IDebtGoalDebtService>();
            var mockMGS = new Mock<IMortgageGoalService>();
            var mockIL = new Mock<IIncomeLogic>();
            var mockEL = new Mock<IExpenseLogic>();
            var mockSGL = new Mock<ISavingGoalLogic>();
            var mockDGL = new Mock<IDebtGoalLogic>();
            var mockMGL = new Mock<IMortgageGoalLogic>();
            var mockIGL = new Mock<IInvestmentGoalLogic>();


            mockBS.Setup(x => x.UpdateBudget(It.IsAny<Budget>())).Returns(
                new Budget()
                {
                    Id = 1,
                    Name = "ModifyTest",
                    UserId = Guid.Empty,
                    SavingGoalId = 1,
                    DebtGoalId = 1,
                    MortgageGoalId = 1,
                    InvestmentGoalId = 1,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                });

            var budgetLogic = new BudgetLogic(mockBS.Object, mockIS.Object,
                                              mockES.Object, mockBIS.Object,
                                              mockBES.Object, mockIGS.Object,
                                              mockInvS.Object, mockIGIS.Object,
                                              mockMGS.Object, mockDGS.Object,
                                              mockDS.Object, mockDGDS.Object,
                                              mockSGS.Object, mockIL.Object,
                                              mockEL.Object, mockSGL.Object,
                                              mockDGL.Object, mockMGL.Object,
                                              mockIGL.Object
                                              );
            var expected = new UserBudget()
            {
                Id = 1,
                Name = "ModifyTest",
                SavingGoalId = 1,
                DebtGoalId = 1,
                MortgageGoalId = 1,
                InvestmentGoalId = 1,
            };

            var actual = budgetLogic.ModifyUserBudget(Guid.Empty, new UserBudget()
            {
                Id = 1,
                Name = "ModifyTest",
                SavingGoalId = 1,
                DebtGoalId = 1,
                MortgageGoalId = 1,
                InvestmentGoalId = 1,
            });

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void DeleteBudget()
        {
            var mockBS = new Mock<IBudgetService>();
            var mockIS = new Mock<IIncomeService>();
            var mockES = new Mock<IExpenseService>();
            var mockBIS = new Mock<IBudgetIncomeService>();
            var mockBES = new Mock<IBudgetExpenseService>();
            var mockIGS = new Mock<IInvestmentGoalService>();
            var mockInvS = new Mock<IInvestmentService>();
            var mockIGIS = new Mock<IInvestmentGoalInvestmentService>();
            var mockSGS = new Mock<ISavingGoalService>();
            var mockDGS = new Mock<IDebtGoalService>();
            var mockDS = new Mock<IDebtService>();
            var mockDGDS = new Mock<IDebtGoalDebtService>();
            var mockMGS = new Mock<IMortgageGoalService>();
            var mockIL = new Mock<IIncomeLogic>();
            var mockEL = new Mock<IExpenseLogic>();
            var mockSGL = new Mock<ISavingGoalLogic>();
            var mockDGL = new Mock<IDebtGoalLogic>();
            var mockMGL = new Mock<IMortgageGoalLogic>();
            var mockIGL = new Mock<IInvestmentGoalLogic>();

            mockBS.Setup(x => x.GetBudget(It.IsAny<int>())).Returns(new Budget()
            {
                Id = 1,
                Name = "DeleteTest",
                UserId = Guid.Empty,
                SavingGoalId = 1,
                DebtGoalId = 1,
                MortgageGoalId = 1,
                InvestmentGoalId = 1,
                CreatedOn = DateTime.MinValue,
                UpdatedOn = DateTime.MinValue
            });

            mockBS.Setup(x => x.DeleteBudget(It.IsAny<int>())).Returns(
                new Budget()
                {
                    Id = 1,
                    Name = "DeleteTest",
                    UserId = Guid.Empty,
                    SavingGoalId = 1,
                    DebtGoalId = 1,
                    MortgageGoalId = 1,
                    InvestmentGoalId = 1,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                });

            mockIGIS.Setup(x => x.GetAllInvestmentGoalInvestments()).Returns(
                (IList<InvestmentGoalInvestment>)new List<InvestmentGoalInvestment>()
                {
                    new InvestmentGoalInvestment() { Id = 1, InvestmentGoalId = 1, InvestmentId = 1 },
                    new InvestmentGoalInvestment() { Id = 2, InvestmentGoalId = 1, InvestmentId = 2 },
                    new InvestmentGoalInvestment() { Id = 3, InvestmentGoalId = 2, InvestmentId = 3 },
                    new InvestmentGoalInvestment() { Id = 4, InvestmentGoalId = 2, InvestmentId = 2 }
            });

            mockDGDS.Setup(x => x.GetAllDebtGoalDebts()).Returns(
                (IList<DebtGoalDebt>)new List<DebtGoalDebt>()
                {
                    new DebtGoalDebt() { Id = 1, DebtGoalId = 1, DebtId = 1 },
                    new DebtGoalDebt() { Id = 2, DebtGoalId = 1, DebtId = 2 },
                    new DebtGoalDebt() { Id = 3, DebtGoalId = 2, DebtId = 3 },
                    new DebtGoalDebt() { Id = 4, DebtGoalId = 2, DebtId = 2 }
                });

            mockIGIS.Setup(x => x.DeleteInvestmentGoalInvestment(It.IsAny<int>())).Returns(
                new InvestmentGoalInvestment());
            mockDGDS.Setup(x => x.DeleteDebtGoalDebt(It.IsAny<int>())).Returns(
                new DebtGoalDebt());
            mockInvS.Setup(x => x.DeleteInvestment(It.IsAny<int>())).Returns(
                new Investment());
            mockDS.Setup(x => x.DeleteDebt(It.IsAny<int>())).Returns(
                new Debt());
            mockIGS.Setup(x => x.DeleteInvestmentGoal(It.IsAny<int>())).Returns(
                new InvestmentGoal());
            mockDGS.Setup(x => x.DeleteDebtGoal(It.IsAny<int>())).Returns(
                new DebtGoal());
            mockSGS.Setup(x => x.DeleteSavingGoal(It.IsAny<int>())).Returns(
                new SavingGoal());
            mockMGS.Setup(x => x.DeleteMortgageGoal(It.IsAny<int>())).Returns(
                new MortgageGoal());

            mockBIS.Setup(x => x.GetAllBudgetIncomes()).Returns(
                (IList<BudgetIncome>)new List<BudgetIncome>()
                {
                    new BudgetIncome() { Id = 1, BudgetId = 1, IncomeId = 1 },
                    new BudgetIncome() { Id = 2, BudgetId = 1, IncomeId = 2 },
                    new BudgetIncome() { Id = 3, BudgetId = 2, IncomeId = 3 },
                    new BudgetIncome() { Id = 4, BudgetId = 2, IncomeId = 2 },
                });
            mockBIS.Setup(x => x.DeleteBudgetIncome(It.IsAny<int>())).Returns(
                new BudgetIncome());
            mockIS.Setup(x => x.DeleteIncome(It.IsAny<int>())).Returns(
                new Income());

            mockBES.Setup(x => x.GetAllBudgetExpenses()).Returns(
                (IList<BudgetExpense>)new List<BudgetExpense>()
                {
                    new BudgetExpense() { Id = 1, BudgetId = 1, ExpenseId = 1 },
                    new BudgetExpense() { Id = 2, BudgetId = 1, ExpenseId = 2 },
                    new BudgetExpense() { Id = 3, BudgetId = 2, ExpenseId = 3 },
                    new BudgetExpense() { Id = 4, BudgetId = 2, ExpenseId = 2 },
                });
            mockBES.Setup(x => x.DeleteBudgetExpense(It.IsAny<int>())).Returns(
                new BudgetExpense());
            mockES.Setup(x => x.DeleteExpense(It.IsAny<int>())).Returns(
                new Expense());

            //==================================================================================
            var budgetLogic = new BudgetLogic(mockBS.Object, mockIS.Object,
                                              mockES.Object, mockBIS.Object,
                                              mockBES.Object, mockIGS.Object,
                                              mockInvS.Object, mockIGIS.Object,
                                              mockMGS.Object, mockDGS.Object,
                                              mockDS.Object, mockDGDS.Object,
                                              mockSGS.Object, mockIL.Object,
                                              mockEL.Object, mockSGL.Object,
                                              mockDGL.Object, mockMGL.Object,
                                              mockIGL.Object
                                              );
            var expected = new UserBudget()
            {
                Id = 1,
                Name = "DeleteTest",
                SavingGoalId = 1,
                DebtGoalId = 1,
                MortgageGoalId = 1,
                InvestmentGoalId = 1,
            };

            var actual = budgetLogic.DeleteUserBudget(It.IsAny<int>());

            actual.Should().BeEquivalentTo(expected);

        }

        [TestMethod]
        public void GET_NetAvailable()
        {
            var mockBS = new Mock<IBudgetService>();
            var mockIS = new Mock<IIncomeService>();
            var mockES = new Mock<IExpenseService>();
            var mockBIS = new Mock<IBudgetIncomeService>();
            var mockBES = new Mock<IBudgetExpenseService>();
            var mockIGS = new Mock<IInvestmentGoalService>();
            var mockInvS = new Mock<IInvestmentService>();
            var mockIGIS = new Mock<IInvestmentGoalInvestmentService>();
            var mockSGS = new Mock<ISavingGoalService>();
            var mockDGS = new Mock<IDebtGoalService>();
            var mockDS = new Mock<IDebtService>();
            var mockDGDS = new Mock<IDebtGoalDebtService>();
            var mockMGS = new Mock<IMortgageGoalService>();
            var mockIL = new Mock<IIncomeLogic>();
            var mockEL = new Mock<IExpenseLogic>();
            var mockSGL = new Mock<ISavingGoalLogic>();
            var mockDGL = new Mock<IDebtGoalLogic>();
            var mockMGL = new Mock<IMortgageGoalLogic>();
            var mockIGL = new Mock<IInvestmentGoalLogic>();


            mockIL.Setup(x => x.GetBudgetMonthlyIncome(It.IsAny<int>())).Returns(3875.50);
            mockEL.Setup(x => x.GetBudgetMonthlyExpense(It.IsAny<int>())).Returns(2416.98);
            mockBS.Setup(x => x.GetAllBudgets()).Returns(
                (IList<Budget>)new List<Budget>()
                {
                    new Budget()
                    {
                        Id = 1,
                        Name = "test",
                        UserId = Guid.Empty,
                        SavingGoalId = 1,
                        DebtGoalId = 1,
                        MortgageGoalId = 1,
                        InvestmentGoalId = 1,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    },
                    new Budget()
                    {
                        Id = 2,
                        Name = "test2",
                        UserId = Guid.Empty,
                        SavingGoalId = 2,
                        DebtGoalId = 2,
                        MortgageGoalId = 2,
                        InvestmentGoalId = 2,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    },
                    new Budget()
                    {
                        Id = 3,
                        Name = "test3",
                        UserId = Guid.Empty,
                        SavingGoalId = 3,
                        DebtGoalId = 3,
                        MortgageGoalId = 3,
                        InvestmentGoalId = 3,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                });

            mockSGS.Setup(x => x.GetSavingGoal(It.IsAny<int>())).Returns(
                new SavingGoal()
                {
                    Id = 1,
                    CurrentBalance = 500,
                    TargetBalance = 5000,
                    AnnualReturnRate_Percent = 2,
                    AdditionalBudgetAllocation = 100,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                });
            mockDGS.Setup(x => x.GetDebtGoal(It.IsAny<int>())).Returns(
                new DebtGoal()
                {
                    Id = 1,
                    AdditionalBudgetAllocation = 70,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                });
            mockMGS.Setup(x => x.GetMortgageGoal(It.IsAny<int>())).Returns(
                new MortgageGoal()
                {
                    Id = 1,
                    TotalBalance = 200000,
                    InterestRate_Percent = 5,
                    MonthlyPayment = 1645.00,
                    AdditionalBudgetAllocation = 85,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                });
            mockIGS.Setup(x => x.GetInvestmentGoal(It.IsAny<int>())).Returns(
                new InvestmentGoal()
                {
                    Id = 1,
                    AnnualReturnRate_Percent = 10,
                    YearsToTarget = 42,
                    AdditionalBudgetAllocation = 125,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                });

            //==================================================================================
            var budgetLogic = new BudgetLogic(mockBS.Object, mockIS.Object,
                                              mockES.Object, mockBIS.Object,
                                              mockBES.Object, mockIGS.Object,
                                              mockInvS.Object, mockIGIS.Object,
                                              mockMGS.Object, mockDGS.Object,
                                              mockDS.Object, mockDGDS.Object,
                                              mockSGS.Object, mockIL.Object,
                                              mockEL.Object, mockSGL.Object,
                                              mockDGL.Object, mockMGL.Object,
                                              mockIGL.Object
                                              );
            var expected = 1078.52;

            var actual = budgetLogic.GetAvailableMonthlyNet(1);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetBudgetAllocations_CollectionsAreEqual()
        {
            var mockBS = new Mock<IBudgetService>();
            var mockIS = new Mock<IIncomeService>();
            var mockES = new Mock<IExpenseService>();
            var mockBIS = new Mock<IBudgetIncomeService>();
            var mockBES = new Mock<IBudgetExpenseService>();
            var mockIGS = new Mock<IInvestmentGoalService>();
            var mockInvS = new Mock<IInvestmentService>();
            var mockIGIS = new Mock<IInvestmentGoalInvestmentService>();
            var mockSGS = new Mock<ISavingGoalService>();
            var mockDGS = new Mock<IDebtGoalService>();
            var mockDS = new Mock<IDebtService>();
            var mockDGDS = new Mock<IDebtGoalDebtService>();
            var mockMGS = new Mock<IMortgageGoalService>();
            var mockIL = new Mock<IIncomeLogic>();
            var mockEL = new Mock<IExpenseLogic>();
            var mockSGL = new Mock<ISavingGoalLogic>();
            var mockDGL = new Mock<IDebtGoalLogic>();
            var mockMGL = new Mock<IMortgageGoalLogic>();
            var mockIGL = new Mock<IInvestmentGoalLogic>();

            mockBS.Setup(x => x.GetBudget(It.IsAny<int>())).Returns(
                new Budget()
                    {
                        Id = 1,
                        Name = "test",
                        UserId = Guid.Empty,
                        SavingGoalId = 1,
                        DebtGoalId = 1,
                        MortgageGoalId = 1,
                        InvestmentGoalId = 1,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                });
            mockDGS.Setup(x => x.GetDebtGoal(It.IsAny<int>())).Returns(
                new DebtGoal()
                {
                    AdditionalBudgetAllocation = 100
                });

            mockMGS.Setup(x => x.GetMortgageGoal(It.IsAny<int>())).Returns(
                new MortgageGoal()
                {
                    AdditionalBudgetAllocation = 50
                });

            mockSGS.Setup(x => x.GetSavingGoal(It.IsAny<int>())).Returns(
                new SavingGoal()
                {
                    AdditionalBudgetAllocation = 110

                });

            mockIGS.Setup(x => x.GetInvestmentGoal(It.IsAny<int>())).Returns(
                new InvestmentGoal()
                {
                    AdditionalBudgetAllocation = 145

                });

            var budgetLogic = new BudgetLogic(mockBS.Object, mockIS.Object,
                                              mockES.Object, mockBIS.Object,
                                              mockBES.Object, mockIGS.Object,
                                              mockInvS.Object, mockIGIS.Object,
                                              mockMGS.Object, mockDGS.Object,
                                              mockDS.Object, mockDGDS.Object,
                                              mockSGS.Object, mockIL.Object,
                                              mockEL.Object, mockSGL.Object,
                                              mockDGL.Object, mockMGL.Object,
                                              mockIGL.Object
                                              );

            var expected = new UserBudgetAllocations()
            {
                    DebtGoal = 100,
                    InvestmentGoal = 145,
                    SavingGoal = 110,
                    MortgageGoal = 50
            };

            var actual = budgetLogic.GetUserBudgetAllocations(It.IsAny<int>());

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void UpdateUserBudgetAllocations_CollectionsAreEqual()
        {
            var mockBS = new Mock<IBudgetService>();
            var mockIS = new Mock<IIncomeService>();
            var mockES = new Mock<IExpenseService>();
            var mockBIS = new Mock<IBudgetIncomeService>();
            var mockBES = new Mock<IBudgetExpenseService>();
            var mockIGS = new Mock<IInvestmentGoalService>();
            var mockInvS = new Mock<IInvestmentService>();
            var mockIGIS = new Mock<IInvestmentGoalInvestmentService>();
            var mockSGS = new Mock<ISavingGoalService>();
            var mockDGS = new Mock<IDebtGoalService>();
            var mockDS = new Mock<IDebtService>();
            var mockDGDS = new Mock<IDebtGoalDebtService>();
            var mockMGS = new Mock<IMortgageGoalService>();
            var mockIL = new Mock<IIncomeLogic>();
            var mockEL = new Mock<IExpenseLogic>();
            var mockSGL = new Mock<ISavingGoalLogic>();
            var mockDGL = new Mock<IDebtGoalLogic>();
            var mockMGL = new Mock<IMortgageGoalLogic>();
            var mockIGL = new Mock<IInvestmentGoalLogic>();

            mockBS.Setup(x => x.GetBudget(It.IsAny<int>())).Returns(
                new Budget()
                {
                    Id = 1,
                    Name = "test",
                    UserId = Guid.Empty,
                    SavingGoalId = 1,
                    DebtGoalId = 1,
                    MortgageGoalId = 1,
                    InvestmentGoalId = 1,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                });
            mockDGS.Setup(x => x.UpdateDebtGoal(It.IsAny<DebtGoal>())).Returns(
                new DebtGoal()
                {
                    AdditionalBudgetAllocation = 90
                });
            mockDGS.Setup(x => x.GetDebtGoal(It.IsAny<int>())).Returns(
                new DebtGoal()
                {
                    AdditionalBudgetAllocation = 100
                });

            mockMGS.Setup(x => x.ModifyMortgageGoal(It.IsAny<MortgageGoal>())).Returns(
                new MortgageGoal()
                {
                    AdditionalBudgetAllocation = 40
                });
            mockMGS.Setup(x => x.GetMortgageGoal(It.IsAny<int>())).Returns(
                new MortgageGoal()
                {
                    AdditionalBudgetAllocation = 50
                });

            mockSGS.Setup(x => x.ModifySavingGoal(It.IsAny<SavingGoal>())).Returns(
                new SavingGoal()
                {
                    AdditionalBudgetAllocation = 105

                });
            mockSGS.Setup(x => x.GetSavingGoal(It.IsAny<int>())).Returns(
                new SavingGoal()
                {
                    AdditionalBudgetAllocation = 110

                });

            mockIGS.Setup(x => x.UpdateInvestmentGoal(It.IsAny<InvestmentGoal>())).Returns(
                new InvestmentGoal()
                {
                    AdditionalBudgetAllocation = 135

                });
            mockIGS.Setup(x => x.GetInvestmentGoal(It.IsAny<int>())).Returns(
                new InvestmentGoal()
                {
                    AdditionalBudgetAllocation = 145

                });

            var budgetLogic = new BudgetLogic(mockBS.Object, mockIS.Object,
                                              mockES.Object, mockBIS.Object,
                                              mockBES.Object, mockIGS.Object,
                                              mockInvS.Object, mockIGIS.Object,
                                              mockMGS.Object, mockDGS.Object,
                                              mockDS.Object, mockDGDS.Object,
                                              mockSGS.Object, mockIL.Object,
                                              mockEL.Object, mockSGL.Object,
                                              mockDGL.Object, mockMGL.Object,
                                              mockIGL.Object
                                              );

            var expected = new UserBudgetAllocations()
            {
                DebtGoal = 90,
                MortgageGoal = 40,
                InvestmentGoal = 135,
                SavingGoal = 105,
            };

            var actual = budgetLogic.UpdateUserBudgetAllocations(It.IsAny<int>(), expected);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void GetBudgetAvailableFreeCash_AmountsAreEqual()
        {
            var mockBS = new Mock<IBudgetService>();
            var mockIS = new Mock<IIncomeService>();
            var mockES = new Mock<IExpenseService>();
            var mockBIS = new Mock<IBudgetIncomeService>();
            var mockBES = new Mock<IBudgetExpenseService>();
            var mockIGS = new Mock<IInvestmentGoalService>();
            var mockInvS = new Mock<IInvestmentService>();
            var mockIGIS = new Mock<IInvestmentGoalInvestmentService>();
            var mockSGS = new Mock<ISavingGoalService>();
            var mockDGS = new Mock<IDebtGoalService>();
            var mockDS = new Mock<IDebtService>();
            var mockDGDS = new Mock<IDebtGoalDebtService>();
            var mockMGS = new Mock<IMortgageGoalService>();
            var mockIL = new Mock<IIncomeLogic>();
            var mockEL = new Mock<IExpenseLogic>();
            var mockSGL = new Mock<ISavingGoalLogic>();
            var mockDGL = new Mock<IDebtGoalLogic>();
            var mockMGL = new Mock<IMortgageGoalLogic>();
            var mockIGL = new Mock<IInvestmentGoalLogic>();

            mockBS.Setup(x => x.GetBudget(It.IsAny<int>())).Returns(
                new Budget()
                {
                    Id = 1,
                    Name = "test",
                    UserId = Guid.Empty,
                    SavingGoalId = 1,
                    DebtGoalId = 1,
                    MortgageGoalId = 1,
                    InvestmentGoalId = 1,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                });
            mockES.Setup(x => x.GetExpensesByBudgetId(It.IsAny<int>())).Returns(
                new List<Expense>()
                {
                    new Expense()
                    {
                        Amount = 50
                    }
                });

            mockIS.Setup(x => x.GetIncomesByBudgetId(It.IsAny<int>())).Returns(
                new List<Income>()
                {
                    new Income()
                    {
                        Amount = 100
                    }
                });

            var budgetLogic = new BudgetLogic(mockBS.Object, mockIS.Object,
                                              mockES.Object, mockBIS.Object,
                                              mockBES.Object, mockIGS.Object,
                                              mockInvS.Object, mockIGIS.Object,
                                              mockMGS.Object, mockDGS.Object,
                                              mockDS.Object, mockDGDS.Object,
                                              mockSGS.Object, mockIL.Object,
                                              mockEL.Object, mockSGL.Object,
                                              mockDGL.Object, mockMGL.Object,
                                              mockIGL.Object
                                              );

            var expected = 50.0;

            var actual = budgetLogic.GetBudgetAvailableFreeCash(It.IsAny<int>());

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GET_CurrentNetWorth()
        {
            var mockBS = new Mock<IBudgetService>();
            var mockIS = new Mock<IIncomeService>();
            var mockES = new Mock<IExpenseService>();
            var mockBIS = new Mock<IBudgetIncomeService>();
            var mockBES = new Mock<IBudgetExpenseService>();
            var mockIGS = new Mock<IInvestmentGoalService>();
            var mockInvS = new Mock<IInvestmentService>();
            var mockIGIS = new Mock<IInvestmentGoalInvestmentService>();
            var mockSGS = new Mock<ISavingGoalService>();
            var mockDGS = new Mock<IDebtGoalService>();
            var mockDS = new Mock<IDebtService>();
            var mockDGDS = new Mock<IDebtGoalDebtService>();
            var mockMGS = new Mock<IMortgageGoalService>();
            var mockIL = new Mock<IIncomeLogic>();
            var mockEL = new Mock<IExpenseLogic>();
            var mockSGL = new Mock<ISavingGoalLogic>();
            var mockDGL = new Mock<IDebtGoalLogic>();
            var mockMGL = new Mock<IMortgageGoalLogic>();
            var mockIGL = new Mock<IInvestmentGoalLogic>();


            mockBS.Setup(x => x.GetBudget(It.IsAny<int>())).Returns(
                    new Budget()
                    {
                        Id = 1,
                        Name = "test",
                        UserId = Guid.Empty,
                        SavingGoalId = 1,
                        DebtGoalId = 1,
                        MortgageGoalId = 1,
                        InvestmentGoalId = 1,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    });

            mockSGS.Setup(x => x.GetSavingGoal(It.IsAny<int>())).Returns(
                new SavingGoal()
                {
                    Id = 1,
                    CurrentBalance = 500,
                    TargetBalance = 5000,
                    AnnualReturnRate_Percent = 2,
                    AdditionalBudgetAllocation = 100,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                });
            mockDS.Setup(x => x.GetDebtsByDebtGoalId(It.IsAny<int>())).Returns(
                new List<Debt>()
                {
                    new Debt{
                        Id = 1,
                        Name = "debt1",
                        CurrentBalance = 500,
                        InterestRate = 5,
                        MinimumPayment = 100,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    },
                    new Debt{
                        Id = 1,
                        Name = "debt2",
                        CurrentBalance = 5000,
                        InterestRate = 4,
                        MinimumPayment = 100,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                });
            mockMGS.Setup(x => x.GetMortgageGoal(It.IsAny<int>())).Returns(
                new MortgageGoal()
                {
                    Id = 1,
                    TotalBalance = 200000,
                    InterestRate_Percent = 5,
                    MonthlyPayment = 1645.00,
                    EstimatedCurrentValue = 250000,
                    AdditionalBudgetAllocation = 85,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                });
            mockInvS.Setup(x => x.GetInvestmentsByInvestmentGoalId(It.IsAny<int>())).Returns(
                new List<Investment>()
                {
                    new Investment()
                    {
                    Id = 1,
                    Name = "investment1",
                    CurrentBalance = 1000,
                    MonthlyPersonalContribution = 42,
                    MonthlyEmployerContribution = 125,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                    },
                    new Investment()
                    {
                    Id = 1,
                    Name = "investment2",
                    CurrentBalance = 10000,
                    MonthlyPersonalContribution = 42,
                    MonthlyEmployerContribution = 125,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                    }
                });

            //==================================================================================
            var budgetLogic = new BudgetLogic(mockBS.Object, mockIS.Object,
                                              mockES.Object, mockBIS.Object,
                                              mockBES.Object, mockIGS.Object,
                                              mockInvS.Object, mockIGIS.Object,
                                              mockMGS.Object, mockDGS.Object,
                                              mockDS.Object, mockDGDS.Object,
                                              mockSGS.Object, mockIL.Object,
                                              mockEL.Object, mockSGL.Object,
                                              mockDGL.Object, mockMGL.Object,
                                              mockIGL.Object
                                              );
            var expected = 56000.0;

            var actual = budgetLogic.GetCurrentNetWorth(1);

            actual.Should().Be(expected);
        }
        [TestMethod]
        public void GET_NetWorthOverTime()
        {
            var mockBS = new Mock<IBudgetService>();
            var mockIS = new Mock<IIncomeService>();
            var mockES = new Mock<IExpenseService>();
            var mockBIS = new Mock<IBudgetIncomeService>();
            var mockBES = new Mock<IBudgetExpenseService>();
            var mockIGS = new Mock<IInvestmentGoalService>();
            var mockInvS = new Mock<IInvestmentService>();
            var mockIGIS = new Mock<IInvestmentGoalInvestmentService>();
            var mockSGS = new Mock<ISavingGoalService>();
            var mockDGS = new Mock<IDebtGoalService>();
            var mockDS = new Mock<IDebtService>();
            var mockDGDS = new Mock<IDebtGoalDebtService>();
            var mockMGS = new Mock<IMortgageGoalService>();
            var mockIL = new Mock<IIncomeLogic>();
            var mockEL = new Mock<IExpenseLogic>();
            var mockSGL = new Mock<ISavingGoalLogic>();
            var mockDGL = new Mock<IDebtGoalLogic>();
            var mockMGL = new Mock<IMortgageGoalLogic>();
            var mockIGL = new Mock<IInvestmentGoalLogic>();


            mockBS.Setup(x => x.GetBudget(It.IsAny<int>())).Returns(
                    new Budget()
                    {
                        Id = 1,
                        Name = "test",
                        UserId = Guid.Empty,
                        SavingGoalId = 1,
                        DebtGoalId = 1,
                        MortgageGoalId = 1,
                        InvestmentGoalId = 1,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    });
            mockIGS.Setup(x => x.GetInvestmentGoal(It.IsAny<int>())).Returns(
                new InvestmentGoal()
                {
                    Id = 1,
                    AnnualReturnRate_Percent = 10.5,
                    YearsToTarget = 1,
                    AdditionalBudgetAllocation = 540.0,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
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
            mockMGL.Setup(x => x.GetMortgageTotals(It.IsAny<int>())).Returns(
                new MortgageGoalTotals()
                {
                    MonthsToPayoff = 9
                });
            mockSGL.Setup(x => x.GetBalanceOverTime(It.IsAny<int>(), It.IsAny<int>())).Returns(
                new Dictionary<int, double>()
                {
                    {0, 1000.0 },
                    {1, 2003.33 },
                    {2, 3008.34 },
                    {3, 4015.02 },
                    {4, 4021.71 },
                    {5, 4028.41 },
                    {6, 4035.13 },
                    {7, 4041.85 },
                    {8, 4048.59 },
                    {9, 4055.34 },
                    {10, 4062.10 },
                    {11, 4068.87 },
                    {12, 4075.65 }
                });
            mockDGL.Setup(x => x.GetBalanceOverTime(It.IsAny<int>())).Returns(
                new Dictionary<int, double>()
                {
                    {0, 4500.0 },
                    {1, 3721.58 },
                    {2, 2938.63 },
                    {3, 2151.10 },
                    {4, 1358.98 },
                    {5, 562.24 },
                    {6, 0.0 }
                });
            mockMGL.Setup(x => x.GetNetValueOverTime(It.IsAny<int>(), It.IsAny<int>())).Returns(
                new Dictionary<int, double>()
                {
                    {0, 25000.0 },
                    {1, 28075.0 },
                    {2, 31162.67 },
                    {3, 34263.07 },
                    {4, 37376.25 },
                    {5, 40502.26 },
                    {6, 43641.15 },
                    {7, 46792.99 },
                    {8, 49957.81 },
                    {9, 51520.16 },
                    {10, 51691.89 },
                    {11, 51864.20 },
                    {12, 52037.08 }
                });
            mockIGL.Setup(x => x.GetBalanceOverTime(It.IsAny<int>())).Returns(
                new Dictionary<int, double>()
                {
                    {0, 12000 },
                    {1, 12952.35 },
                    {2, 13913.03 },
                    {3, 14882.12 },
                    {4, 15859.69 },
                    {5, 16845.81 },
                    {6, 17840.56 },
                    {7, 18844.02 },
                    {8, 19856.25 },
                    {9, 20877.35 },
                    {10, 21907.37 },
                    {11, 22946.41 },
                    {12, 23994.54 }
                });

            //==================================================================================
            var budgetLogic = new BudgetLogic(mockBS.Object, mockIS.Object,
                                              mockES.Object, mockBIS.Object,
                                              mockBES.Object, mockIGS.Object,
                                              mockInvS.Object, mockIGIS.Object,
                                              mockMGS.Object, mockDGS.Object,
                                              mockDS.Object, mockDGDS.Object,
                                              mockSGS.Object, mockIL.Object,
                                              mockEL.Object, mockSGL.Object,
                                              mockDGL.Object, mockMGL.Object,
                                              mockIGL.Object
                                              );
            var expected = new NetWorthTotals()
            {
                Balance = new List<KeyValuePair<int, double>>()
                    {
                        new KeyValuePair<int, double>(0, 33500.0 ),
                        new KeyValuePair<int, double>(1, 39309.10),
                        new KeyValuePair<int, double>(2, 45145.41),
                        new KeyValuePair<int, double>(3, 51009.11),
                        new KeyValuePair<int, double>(4, 55898.67),
                        new KeyValuePair<int, double>(5, 60814.24),
                        new KeyValuePair<int, double>(6, 65516.84),
                        new KeyValuePair<int, double>(7, 69678.86),
                        new KeyValuePair<int, double>(8, 73862.65),
                        new KeyValuePair<int, double>(9, 76452.85),
                        new KeyValuePair<int, double>(10, 77661.36),
                        new KeyValuePair<int, double>(11, 78879.48),
                        new KeyValuePair<int, double>(12, 80107.27),
                    },
                SavingGoalMonth = 3,
                DebtGoalMonth = 6,
                MortgageGoalMonth = 9
            };

            var actual = budgetLogic.GetNetWorthOverTime(1);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}