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
    public class ExpenseLogicTest
    {
        public ExpenseLogicTest()
        {

        }


        [TestMethod]
        public void GetUserExpenses_CollectionAreEqual()
        {
            var mock = new Mock<IExpenseService>();
            var mock2 = new Mock<IBudgetExpenseService>();
            var mock3 = new Mock<IBudgetService>();

            mock.Setup(x => x.GetAllExpenses()).Returns(
                (IList<Expense>) new List<Expense>()
                {
                    new Expense() { Id = 1, Amount = 100, Frequency = EFrequency.None, Priority = EPriority.Need, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Expense() { Id = 2, Amount = 100, Frequency = EFrequency.None, Priority = EPriority.Need, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Expense() { Id = 3, Amount = 100, Frequency = EFrequency.None, Priority = EPriority.Need, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Expense() { Id = 4, Amount = 100, Frequency = EFrequency.None, Priority = EPriority.Need, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Expense() { Id = 5, Amount = 100, Frequency = EFrequency.None, Priority = EPriority.Need, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Expense() { Id = 6, Amount = 100, Frequency = EFrequency.None, Priority = EPriority.Need, UserId = Guid.NewGuid(), CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue }
                }
                );
            var expenseLogic = new ExpenseLogic(mock.Object, mock2.Object, mock3.Object);

            var expected = new List<UserExpense>()
            {
                new UserExpense() {Id = 1, Amount = 100, Frequency = EFrequency.None, Priority = EPriority.Need},
                new UserExpense() {Id = 2, Amount = 100, Frequency = EFrequency.None, Priority = EPriority.Need},
                new UserExpense() {Id = 3, Amount = 100, Frequency = EFrequency.None, Priority = EPriority.Need},
                new UserExpense() {Id = 4, Amount = 100, Frequency = EFrequency.None, Priority = EPriority.Need},
                new UserExpense() {Id = 5, Amount = 100, Frequency = EFrequency.None, Priority = EPriority.Need}
            };

            var actual = expenseLogic.GetUserExpenses(Guid.Empty);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void GetBudgetMonthlyExpense()
        {
            var mockES = new Mock<IExpenseService>();
            var mockBES = new Mock<IBudgetExpenseService>();
            var mockBS = new Mock <IBudgetService>();

            mockES.Setup(x => x.GetExpensesByBudgetId(It.IsAny<int>())).Returns(
                (IList<Expense>)new List<Expense>()
                {
                    new Expense() { Id = 1, Amount = 100, Frequency = EFrequency.None, Priority = EPriority.Need, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Expense() { Id = 2, Amount = 100, Frequency = EFrequency.None, Priority = EPriority.Want, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Expense() { Id = 3, Amount = 100, Frequency = EFrequency.Weekly, Priority = EPriority.Want, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Expense() { Id = 4, Amount = 100, Frequency = EFrequency.Monthly, Priority = EPriority.Need, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Expense() { Id = 5, Amount = 120, Frequency = EFrequency.Quarterly, Priority = EPriority.Need, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                }
                );

            var expenseLogic = new ExpenseLogic(mockES.Object, mockBES.Object, mockBS.Object);

            var expected = 740.0;

            var actual = expenseLogic.GetBudgetMonthlyExpense(1);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetBudgetMonthlyNeed()
        {
            var mockES = new Mock<IExpenseService>();
            var mockBES = new Mock<IBudgetExpenseService>();
            var mockBS = new Mock<IBudgetService>();

            mockES.Setup(x => x.GetExpensesByBudgetId(It.IsAny<int>())).Returns(
                (IList<Expense>)new List<Expense>()
                {
                    new Expense() { Id = 1, Amount = 100, Frequency = EFrequency.None, Priority = EPriority.Need, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Expense() { Id = 2, Amount = 100, Frequency = EFrequency.None, Priority = EPriority.Want, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Expense() { Id = 3, Amount = 100, Frequency = EFrequency.Weekly, Priority = EPriority.Want, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Expense() { Id = 4, Amount = 100, Frequency = EFrequency.Monthly, Priority = EPriority.Need, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Expense() { Id = 5, Amount = 120, Frequency = EFrequency.Quarterly, Priority = EPriority.Need, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                }
                );

            var monthlyExpenseLogic = new ExpenseLogic(mockES.Object, mockBES.Object, mockBS.Object);

            var expected = 240.0;

            var actual = monthlyExpenseLogic.GetBudgetMonthlyNeed(1);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetBudgetMonthlyWant()
        {
            var mockES = new Mock<IExpenseService>();
            var mockBES = new Mock<IBudgetExpenseService>();
            var mockBS = new Mock<IBudgetService>();

            mockES.Setup(x => x.GetExpensesByBudgetId(It.IsAny<int>())).Returns(
                (IList<Expense>)new List<Expense>()
                {
                    new Expense() { Id = 1, Amount = 100, Frequency = EFrequency.None, Priority = EPriority.Need, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Expense() { Id = 2, Amount = 100, Frequency = EFrequency.None, Priority = EPriority.Want, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Expense() { Id = 3, Amount = 100, Frequency = EFrequency.Weekly, Priority = EPriority.Want, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Expense() { Id = 4, Amount = 100, Frequency = EFrequency.Monthly, Priority = EPriority.Need, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Expense() { Id = 5, Amount = 120, Frequency = EFrequency.Quarterly, Priority = EPriority.Need, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                }
                );

            var monthlyExpenseLogic = new ExpenseLogic(mockES.Object, mockBES.Object, mockBS.Object);

            var expected = 500.0;

            var actual = monthlyExpenseLogic.GetBudgetMonthlyWant(1);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void AddExistingUserExpense_CollectionAreEqual()
        {
            var mock = new Mock<IExpenseService>();
            var mock2 = new Mock<IBudgetExpenseService>();
            var mock3 = new Mock<IBudgetService>();

            mock.Setup(x => x.GetAllExpenses()).Returns(
                    new List<Expense>(){ 
                        new Expense()
                        {
                            Id = 1,
                            Amount = 100,
                            Frequency = EFrequency.None,
                            Priority = EPriority.Need,
                            UserId = Guid.Empty,
                            CreatedOn = DateTime.MinValue,
                            UpdatedOn = DateTime.MinValue
                        } 
                }
            );

            mock.Setup(x => x.AddNewExpense(It.IsAny<Expense>())).Returns(
                    new Expense()
                    {
                        Id = 1,
                        Amount = 100,
                        Frequency = EFrequency.None,
                        Priority = EPriority.Need,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                );
            mock2.Setup(x => x.AddNewBudgetExpense(It.IsAny<BudgetExpense>())).Returns(
                    new BudgetExpense()
                    {
                        Id = 1,
                        BudgetId= 1,
                        ExpenseId = 1,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                );

            mock3.Setup(x => x.Exists(It.IsAny<int>())).Returns(
                true
            );

            var expenseLogic = new ExpenseLogic(mock.Object, mock2.Object, mock3.Object);

            var expected = new UserExpense()
            {
                Id = 1,
                Amount = 100,
                BudgetId = 1,
                Frequency = EFrequency.None,
                Priority = EPriority.Need
            };

            var actual = expenseLogic.AddUserExpense(Guid.Empty, new UserExpense()
            {
                Id = 1,
                Amount = 100,
                BudgetId = 1,
                Frequency = EFrequency.None,
                Priority = EPriority.Need
            });

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void AddNewUserExpense_CollectionAreEqual()
        {
            var mock = new Mock<IExpenseService>();
            var mock2 = new Mock<IBudgetExpenseService>();
            var mock3 = new Mock<IBudgetService>();

            mock.Setup(x => x.GetAllExpenses()).Returns(
                    new List<Expense>(){
                        new Expense()
                        {
                            Id = 3,
                            Amount = 300,
                            Frequency = EFrequency.None,
                            Priority = EPriority.Need,
                            UserId = Guid.Empty,
                            CreatedOn = DateTime.MinValue,
                            UpdatedOn = DateTime.MinValue
                        }
                }
            );

            mock.Setup(x => x.AddNewExpense(It.IsAny<Expense>())).Returns(
                    new Expense()
                    {
                        Id = 1,
                        Amount = 100,
                        Frequency = EFrequency.None,
                        Priority = EPriority.Need,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                );
            mock2.Setup(x => x.AddNewBudgetExpense(It.IsAny<BudgetExpense>())).Returns(
                    new BudgetExpense()
                    {
                        Id = 1,
                        BudgetId = 1,
                        ExpenseId = 1,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                );

            mock3.Setup(x => x.Exists(It.IsAny<int>())).Returns(
                true
            );

            var expenseLogic = new ExpenseLogic(mock.Object, mock2.Object, mock3.Object);

            var expected = new UserExpense()
            {
                Id = 1,
                Amount = 100,
                BudgetId = 1,
                Frequency = EFrequency.None,
                Priority = EPriority.Need
            };

            var actual = expenseLogic.AddUserExpense(Guid.Empty, new UserExpense()
            {
                Id = 1,
                Amount = 100,
                BudgetId = 1,
                Frequency = EFrequency.None,
                Priority = EPriority.Need
            });

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void ModifyUserExpense_CollectionAreEqual()
        {
            var mock = new Mock<IExpenseService>();
            var mock2 = new Mock<IBudgetExpenseService>();
            var mock3 = new Mock<IBudgetService>();


            mock.Setup(x => x.UpdateExpense(It.IsAny<Expense>())).Returns(
                    new Expense()
                    {
                        Id = 1,
                        Amount = 101,
                        Frequency = EFrequency.None,
                        Priority = EPriority.Need,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                );
            var expenseLogic = new ExpenseLogic(mock.Object, mock2.Object, mock3.Object);

            var expected = new UserExpense()
            {
                Id = 1,
                Amount = 101,
                Frequency = EFrequency.None,
                Priority = EPriority.Need
            };

            var actual = expenseLogic.ModifyUserExpense(Guid.Empty, new UserExpense()
            {
                Id = 1,
                Amount = 101,
                Frequency = EFrequency.None,
                Priority = EPriority.Need
            });

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void DeleteUserExpense_CollectionAreEqual()
        {
            var mockES = new Mock<IExpenseService>();
            var mockBES = new Mock<IBudgetExpenseService>();
            var mockBS = new Mock<IBudgetService>();

            mockES.Setup(x => x.DeleteExpense(It.IsAny<int>())).Returns(
                    new Expense()
                    {
                        Id = 1,
                        Amount = 101,
                        Frequency = EFrequency.None,
                        Priority = EPriority.Need,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                );

            mockES.Setup(x => x.GetAllExpenses()).Returns(
                new List<Expense>()
                {
                    new Expense()
                    {
                        Id = 1,
                        Amount = 101,
                        Frequency = EFrequency.None,
                        Priority = EPriority.Need,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                }
            );

            mockBES.Setup(x => x.DeleteBudgetExpense(It.IsAny<int>())).Returns(
                new BudgetExpense()
                {
                    Id = 1,
                    BudgetId = 1,
                    ExpenseId = 1,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                }
            );

            mockBES.Setup(x => x.GetAllBudgetExpenses()).Returns(
                new List<BudgetExpense>()
                {
                    new BudgetExpense()
                    {
                        Id = 1,
                        BudgetId = 1,
                        ExpenseId = 1,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    },
                    new BudgetExpense()
                    {
                        Id = 1,
                        BudgetId = 2,
                        ExpenseId = 2,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                }
            ) ;

            var expenseLogic = new ExpenseLogic(mockES.Object, mockBES.Object, mockBS.Object);

            var expected = new UserExpense()
            {
                Id = 1,
                Amount = 101,
                BudgetId = 1,
                Frequency = EFrequency.None,
                Priority = EPriority.Need
            };

            var actual = expenseLogic.DeleteBudgetExpense(1, 1);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void DeleteBudgetExpense_CollectionAreEqual()
        {
            var mockES = new Mock<IExpenseService>();
            var mockBES = new Mock<IBudgetExpenseService>();
            var mockBS = new Mock<IBudgetService>();

            mockES.Setup(x => x.DeleteExpense(It.IsAny<int>())).Returns(
                    new Expense()
                    {
                        Id = 1,
                        Amount = 101,
                        Frequency = EFrequency.None,
                        Priority = EPriority.Need,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                );

            mockES.Setup(x => x.GetAllExpenses()).Returns(
                new List<Expense>()
                {
                    new Expense()
                    {
                        Id = 1,
                        Amount = 101,
                        Frequency = EFrequency.None,
                        Priority = EPriority.Need,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                }
            );

            mockBES.Setup(x => x.DeleteBudgetExpense(It.IsAny<int>())).Returns(
                new BudgetExpense()
                {
                    Id = 1,
                    BudgetId = 1,
                    ExpenseId = 1,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                }
            );

            mockBES.Setup(x => x.GetAllBudgetExpenses()).Returns(
                new List<BudgetExpense>()
                {
                    new BudgetExpense()
                    {
                        Id = 1,
                        BudgetId = 1,
                        ExpenseId = 1,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    },
                    new BudgetExpense()
                    {
                        Id = 1,
                        BudgetId = 2,
                        ExpenseId = 1,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }

                }
            );

            var expenseLogic = new ExpenseLogic(mockES.Object, mockBES.Object, mockBS.Object);

            var expected = new UserExpense()
            {
                Id = 1,
                Amount = 101,
                BudgetId = 1,
                Frequency = EFrequency.None,
                Priority = EPriority.Need
            };

            var actual = expenseLogic.DeleteBudgetExpense(1, 1);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}