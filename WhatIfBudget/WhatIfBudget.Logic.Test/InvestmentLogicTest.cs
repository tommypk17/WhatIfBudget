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
    public class InvestmentLogicTest
    {
        public InvestmentLogicTest()
        {

        }

        [TestMethod]
        public void AddUserInvestment_CollectionAreEqual()
        {
            var mock = new Mock<IInvestmentService>();

            mock.Setup(x => x.AddNewInvestment(It.IsAny<Investment>())).Returns(
                new Investment()
                {
                    Id = 1,
                    Name = "test",
                    CurrentBalance = 0,
                    MonthlyEmployerContribution = 0,
                    MonthlyPersonalContribution = 0,
                    UserId = Guid.Empty,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                }
            );

            var investmentLogic = new InvestmentLogic(mock.Object);

            var expected = new UserInvestment()
            {
                Id = 1,
                Name = "test",
                GoalId = 0,
                CurrentBalance = 0,
                MonthlyPersonalContribution = 0,
                MonthlyEmployerContribution = 0
            };

            var actual = investmentLogic.AddUserInvestment(Guid.Empty, new UserInvestment()
            {
                Id = 1,
                Name = "test",
                GoalId = 0,
                CurrentBalance = 0,
                MonthlyPersonalContribution = 0,
                MonthlyEmployerContribution = 0
            });

            actual.Should().BeEquivalentTo(expected);
        }

    }
}