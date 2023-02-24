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
        public void AddUserExpense_CollectionAreEqual()
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
            var mock = new Mock<IExpenseService>();
            var mock2 = new Mock<IBudgetExpenseService>();
            var mock3 = new Mock<IBudgetService>();

            mock.Setup(x => x.DeleteExpense(It.IsAny<int>())).Returns(
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

            mock.Setup(x => x.GetAllExpenses()).Returns(
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

            mock2.Setup(x => x.DeleteBudgetExpense(It.IsAny<int>())).Returns(
                new BudgetExpense()
                {
                    Id = 1,
                    BudgetId = 1,
                    ExpenseId = 1,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                }
            );

            mock2.Setup(x => x.GetAllBudgetExpenses()).Returns(
                new List<BudgetExpense>()
                {
                    new BudgetExpense()
                    {
                        Id = 1,
                        BudgetId = 1,
                        ExpenseId = 1,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                }
            ) ;

            var expenseLogic = new ExpenseLogic(mock.Object, mock2.Object, mock3.Object);

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