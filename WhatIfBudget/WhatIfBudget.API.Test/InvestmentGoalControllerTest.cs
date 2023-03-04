using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using WhatIfBudget.API.Controllers;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Logic;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Logic.Models;

namespace WhatIfBudget.API.Test
{
    [TestClass]
    public class InvestmentGoalControllerTest
    {
        [TestMethod]
        public void Get_GetInvestmentGoal()
        {
            //mock investment logic
            var mockIGL = new Mock<IInvestmentGoalLogic>();
            mockIGL.Setup(x => x.GetInvestmentGoal(It.IsAny<int>())).Returns(
                new UserInvestmentGoal() {
                    Id = 4,
                    TotalBalance = 1000,
                    AnnualReturnRate_Percent = 5,
                    YearsToTarget = 20,
                    additionalBudgetAllocation = 500
                }
            );

            //Setup the http context (for auth)
            var investmentController = new InvestmentGoalsController(mockIGL.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new UserInvestmentGoal()
                {
                    Id = 4,
                    TotalBalance = 1000,
                    AnnualReturnRate_Percent = 5,
                    YearsToTarget = 20,
                    additionalBudgetAllocation = 500
                };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = investmentController.Get(4);


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Put_UserInvestmentGoalUpdated()
        {
            //mock income logic
            var mock = new Mock<IInvestmentGoalLogic>();
            mock.Setup(x => x.ModifyUserInvestmentGoal(It.IsAny<UserInvestmentGoal>()))
                            .Returns(new UserInvestmentGoal() {
                                Id = 2,
                                TotalBalance = 1000,
                                AnnualReturnRate_Percent = 5,
                                YearsToTarget = 20,
                                additionalBudgetAllocation = 500
                            });

            //Setup the http context (for auth)
            var investmentController = new InvestmentGoalsController(mock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new UserInvestmentGoal() {
                Id = 2,
                TotalBalance = 1000,
                AnnualReturnRate_Percent = 5,
                YearsToTarget = 20,
                additionalBudgetAllocation = 500
            };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = investmentController.Put(new UserInvestmentGoal() {
                Id = 2,
                TotalBalance = 1000.0,
                AnnualReturnRate_Percent = 5.0,
                YearsToTarget = 5,
                additionalBudgetAllocation = 50.0
            });


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Get_InvestmentBalanceOverTime()
        {
            //mock income logic
            var mockIGL = new Mock<IInvestmentGoalLogic>();
            mockIGL.Setup(x => x.GetBalanceOverTime(It.IsAny<int>())).Returns
                (new Dictionary<int, double>()
                {
                    {0, 1000.0 },
                    {1, 1680.0 },
                    {2, 2394.0 },
                    {3, 3143.7 },
                    {4, 3930.88 },
                    {5, 4757.42 }
                });

            //Setup the http context (for auth)
            var investmentController = new InvestmentGoalsController(mockIGL.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new Dictionary<int, double>()
                {
                    {0, 1000.0 },
                    {1, 1680.0 },
                    {2, 2394.0 },
                    {3, 3143.7 },
                    {4, 3930.88 },
                    {5, 4757.42 }
                };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = investmentController.GetBalanceOverTime(1);


            actual.Should().BeEquivalentTo(expected);
        }

        public Mock<HttpContext> Helper_MockHttpContext()
        {
            //Mock the principal & http context to pass in a fake user
            var mockPrincipal = new Mock<ClaimsPrincipal>();
            var fakeClaims = new List<Claim>()
            {
                new Claim("http://schemas.microsoft.com/identity/claims/objectidentifier", Guid.Empty.ToString()),
                new Claim(ClaimTypes.GivenName, "test"),
                new Claim(ClaimTypes.Surname, "test"),
                new Claim(ClaimTypes.Email, "test@test.test"),
            };
            mockPrincipal.Setup(x => x.Claims).Returns(fakeClaims);
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(x => x.User).Returns(mockPrincipal.Object);

            return mockHttpContext;
        }
    }
}