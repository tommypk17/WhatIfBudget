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
    public class MonthlyIncomeLogicTest
    {
        public MonthlyIncomeLogicTest()
        {

        }

        [TestMethod]
        public void GetBudgetMonthlyIncome()
        {
            var mockIS = new Mock<IIncomeService>();
            mockIS.Setup(x => x.GetAllIncomes()).Returns(
                (IList<Income>) new List<Income>()
                {
                    new Income() { Id = 1, Amount = 100, Frequency = EFrequency.None, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Income() { Id = 2, Amount = 100, Frequency = EFrequency.None, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Income() { Id = 3, Amount = 100, Frequency = EFrequency.Weekly, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Income() { Id = 4, Amount = 100, Frequency = EFrequency.Monthly, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Income() { Id = 5, Amount = 120, Frequency = EFrequency.Quarterly, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Income() { Id = 6, Amount = 100, Frequency = EFrequency.Yearly, UserId = Guid.NewGuid(), CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Income() { Id = 7, Amount = 100, Frequency = EFrequency.None, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue },
                    new Income() { Id = 8, Amount = 100, Frequency = EFrequency.None, UserId = Guid.Empty, CreatedOn = DateTime.MinValue, UpdatedOn = DateTime.MinValue }
                }
                );
            var mockBIS = new Mock<IBudgetIncomeService>();
            mockBIS.Setup(x => x.GetAllBudgetIncomes()).Returns(
                (IList<BudgetIncome>)new List<BudgetIncome>()
                {
                    new BudgetIncome() { Id = 1, BudgetId = 1, IncomeId =  1},
                    new BudgetIncome() { Id = 2, BudgetId = 1, IncomeId =  2},
                    new BudgetIncome() { Id = 3, BudgetId = 1, IncomeId =  3},
                    new BudgetIncome() { Id = 4, BudgetId = 1, IncomeId =  4},
                    new BudgetIncome() { Id = 5, BudgetId = 1, IncomeId =  5},
                    new BudgetIncome() { Id = 6, BudgetId = 2, IncomeId =  6},
                    new BudgetIncome() { Id = 7, BudgetId = 2, IncomeId =  7},
                    new BudgetIncome() { Id = 8, BudgetId = 2, IncomeId =  8},
                    new BudgetIncome() { Id = 9, BudgetId = 2, IncomeId =  1}
                }
                );
            var monthlyIncomeLogic = new MonthlyIncomeLogic(mockIS.Object, mockBIS.Object);

            var expected = 740.0;

            var actual = monthlyIncomeLogic.GetBudgetMonthlyIncome(1);

            actual.Should().Be(expected);
        }
    }
}