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
            var mockMGS = new Mock<IMortgageGoalService>();

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
                                              mockSGS.Object
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
        public void GetBudget()
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
            var mockMGS = new Mock<IMortgageGoalService>();

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
                                      mockSGS.Object
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
            var mockMGS = new Mock<IMortgageGoalService>();

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
                                      mockSGS.Object
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
            var mockMGS = new Mock<IMortgageGoalService>();

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
                                      mockSGS.Object
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
            var mockMGS = new Mock<IMortgageGoalService>();

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
            mockIGIS.Setup(x => x.DeleteInvestmentGoalInvestment(It.IsAny<int>())).Returns(
                new InvestmentGoalInvestment());
            mockInvS.Setup(x => x.DeleteInvestment(It.IsAny<int>())).Returns(
                new Investment());
            mockIGS.Setup(x => x.DeleteInvestmentGoal(It.IsAny<int>())).Returns(
                new InvestmentGoal());
            mockSGS.Setup(x => x.DeleteSavingGoal(It.IsAny<int>())).Returns(
                new SavingGoal());

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
                                      mockSGS.Object
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

            var actual = budgetLogic.DeleteUserBudget(new UserBudget()
            {
                Id = 1,
                Name = "DeleteTest",
                SavingGoalId = 1,
                DebtGoalId = 1,
                MortgageGoalId = 1,
                InvestmentGoalId = 1,
            });

            actual.Should().BeEquivalentTo(expected);

        }
    }
}