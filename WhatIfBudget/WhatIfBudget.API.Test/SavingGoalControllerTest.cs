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
    public class SavingGoalControllerTest
    {
        [TestMethod]
        public void Get_GetSavingGoal()
        {
            //mock saving goal logic
            var mockSGL = new Mock<ISavingGoalLogic>();
            mockSGL.Setup(x => x.GetSavingGoal(It.IsAny<int>())).Returns(
                new UserSavingGoal() {
                    Id = 4,
                    CurrentBalance = 1111.0,
                    TargetBalance = 5555.0,
                    AnnualReturnRate_Percent = 2.0,
                    AdditionalBudgetAllocation = 500
                }
            );

            //Setup the http context (for auth)
            var savingController = new SavingGoalsController(mockSGL.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new UserSavingGoal()
                {
                Id = 4,
                CurrentBalance = 1111.0,
                TargetBalance = 5555.0,
                AnnualReturnRate_Percent = 2.0,
                AdditionalBudgetAllocation = 500
            };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = savingController.Get(4);


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Put_UserSavingGoalUpdated()
        {
            //mock saving goal logic
            var mockSGL = new Mock<ISavingGoalLogic>();
            mockSGL.Setup(x => x.ModifyUserSavingGoal(It.IsAny<UserSavingGoal>()))
                            .Returns(new UserSavingGoal() {
                                Id = 2,
                                CurrentBalance = 111.0,
                                TargetBalance = 7555.0,
                                AnnualReturnRate_Percent = 1.0,
                                AdditionalBudgetAllocation = 100
                            });

            //Setup the http context (for auth)
            var savingController = new SavingGoalsController(mockSGL.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new UserSavingGoal() {
                Id = 2,
                CurrentBalance = 111.0,
                TargetBalance = 7555.0,
                AnnualReturnRate_Percent = 1.0,
                AdditionalBudgetAllocation = 100
            };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = savingController.Put(new UserSavingGoal() {
                Id = 2,
                CurrentBalance = 111.0,
                TargetBalance = 7555.0,
                AnnualReturnRate_Percent = 1.0,
                AdditionalBudgetAllocation = 100
            });


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Get_SavingTotals()
        {
            //mock saving goal logic
            var mockSGL = new Mock<ISavingGoalLogic>();
            mockSGL.Setup(x => x.GetSavingTotals(It.IsAny<int>())).Returns(
                new SavingGoalTotals()
                {
                    MonthsToTarget = 5,
                    TotalInterestAccrued = 20.0
                });

            //Setup the http context (for auth)
            var savingController = new SavingGoalsController(mockSGL.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new SavingGoalTotals()
            {
                MonthsToTarget = 5,
                TotalInterestAccrued = 20.0
            };
            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = savingController.GetSavingTotals(1);

            actual.Should().BeEquivalentTo(expected);
        }


        [TestMethod]
        public void Get_SavingBalanceOverTime()
        {
            //mock saving goal logic
            var mockSGL = new Mock<ISavingGoalLogic>();
            mockSGL.Setup(x => x.GetBalanceOverTime(It.IsAny<int>(), 0)).Returns
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
            var savingController = new SavingGoalsController(mockSGL.Object)
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

            var actual = savingController.GetBalanceOverTime(1);


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