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
    public class DebtGoalControllerTest
    {
        [TestMethod]
        public void Get_GetDebtGoal()
        {
            //mock debt logic
            var mockDGL = new Mock<IDebtGoalLogic>();
            mockDGL.Setup(x => x.GetDebtGoal(It.IsAny<int>())).Returns(
                new UserDebtGoal() {
                    Id = 4,
                    AdditionalBudgetAllocation = 500
                }
            );

            //Setup the http context (for auth)
            var debtController = new DebtGoalsController(mockDGL.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new UserDebtGoal()
                {
                    Id = 4,
                    AdditionalBudgetAllocation = 500
                };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = debtController.Get(4);


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Put_UserDebtGoalUpdated()
        {
            //mock income logic
            var mock = new Mock<IDebtGoalLogic>();
            mock.Setup(x => x.ModifyUserDebtGoal(It.IsAny<UserDebtGoal>()))
                            .Returns(new UserDebtGoal()
                            {
                                Id = 2,
                                AdditionalBudgetAllocation = 500
                            });

            //Setup the http context (for auth)
            var debtController = new DebtGoalsController(mock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new UserDebtGoal()
            {
                Id = 2,
                AdditionalBudgetAllocation = 500
            };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = debtController.Put(new UserDebtGoal()
            {
                Id = 2,
                AdditionalBudgetAllocation = 50.0
            });


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Get_DebtBalanceOverTime()
        {
            //mock income logic
            var mockDGL = new Mock<IDebtGoalLogic>();
            mockDGL.Setup(x => x.GetBalanceOverTime(It.IsAny<int>())).Returns
                (new Dictionary<int, double>()
                {
                    { 0,100},
                    { 1,90.08},
                    { 2,80.15},
                    { 3,70.21},
                    { 4,60.26},
                    { 5,50.3},
                    { 6,40.33},
                    { 7,30.36},
                    { 8,20.38},
                    { 9,10.39},
                    { 10,0.39},
                    { 11,0}
                });

            

            //Setup the http context (for auth)
            var debtController = new DebtGoalsController(mockDGL.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new Dictionary<int, double>()
                {
                    { 0,100},
                    { 1,90.08},
                    { 2,80.15},
                    { 3,70.21},
                    { 4,60.26},
                    { 5,50.3},
                    { 6,40.33},
                    { 7,30.36},
                    { 8,20.38},
                    { 9,10.39},
                    { 10,0.39},
                    { 11,0}
                };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = debtController.GetBalanceOverTime(1);


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Get_DebtTotals()
        {
            //mock income logic
            var mockDGL = new Mock<IDebtGoalLogic>();
            mockDGL.Setup(x => x.GetDebtTotals(It.IsAny<int>())).Returns
                (new DebtGoalTotals()
                {
                    AllocationSavings = 0,
                    MonthsToPayoff = 0,
                    TotalCostToPayoff = 0,
                    TotalInterestAccrued = 500000,
                });

            //Setup the http context (for auth)
            var debtController = new DebtGoalsController(mockDGL.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new DebtGoalTotals()
            {
                AllocationSavings = 0,
                MonthsToPayoff = 0,
                TotalCostToPayoff = 0,
                TotalInterestAccrued = 500000,
            };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = debtController.GetDebtTotals(1);

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