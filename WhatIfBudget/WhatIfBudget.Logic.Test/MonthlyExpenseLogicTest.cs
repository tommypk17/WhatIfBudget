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
    public class MonthlyExpenseLogicTest
    {
        public MonthlyExpenseLogicTest()
        {

        }

        [TestMethod]
        public void GetBudgetMonthlyExpense()
        {
            var mockIS = new Mock<IExpenseService>();
            mockIS.Setup(x => x.GetAllExpenses()).Returns(
                (IList<Expense>) new List<Expense>()
                {
                    new Expense() { Id = 1, Amount = 100, Frequency = EFrequency.None, Priority = EPriority.Need, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Expense() { Id = 2, Amount = 100, Frequency = EFrequency.None, Priority = EPriority.Want, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Expense() { Id = 3, Amount = 100, Frequency = EFrequency.Weekly, Priority = EPriority.Want, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Expense() { Id = 4, Amount = 100, Frequency = EFrequency.Monthly, Priority = EPriority.Need, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Expense() { Id = 5, Amount = 120, Frequency = EFrequency.Quarterly, Priority = EPriority.Need, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Expense() { Id = 6, Amount = 100, Frequency = EFrequency.Yearly, Priority = EPriority.Want, UserId = Guid.NewGuid(), CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Expense() { Id = 7, Amount = 100, Frequency = EFrequency.None, Priority = EPriority.Need, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Expense() { Id = 8, Amount = 100, Frequency = EFrequency.None, Priority = EPriority.Need, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue }
                }
                );
            var mockBIS = new Mock<IBudgetExpenseService>();
            mockBIS.Setup(x => x.GetAllBudgetExpenses()).Returns(
                (IList<BudgetExpense>)new List<BudgetExpense>()
                {
                    new BudgetExpense() { Id = 1, BudgetId = 1, ExpenseId =  1},
                    new BudgetExpense() { Id = 2, BudgetId = 1, ExpenseId =  2},
                    new BudgetExpense() { Id = 3, BudgetId = 1, ExpenseId =  3},
                    new BudgetExpense() { Id = 4, BudgetId = 1, ExpenseId =  4},
                    new BudgetExpense() { Id = 5, BudgetId = 1, ExpenseId =  5},
                    new BudgetExpense() { Id = 6, BudgetId = 2, ExpenseId =  6},
                    new BudgetExpense() { Id = 7, BudgetId = 2, ExpenseId =  7},
                    new BudgetExpense() { Id = 8, BudgetId = 2, ExpenseId =  8},
                    new BudgetExpense() { Id = 9, BudgetId = 2, ExpenseId =  1}
                }
                );
            var monthlyExpenseLogic = new MonthlyExpenseLogic(mockIS.Object, mockBIS.Object);

            var expected = 740.0;

            var actual = monthlyExpenseLogic.GetBudgetMonthlyExpense(1);

            actual.Should().Be(expected);
        }
    }
}