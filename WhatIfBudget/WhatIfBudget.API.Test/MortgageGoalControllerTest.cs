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
    public class MortgageGoalControllerTest
    {
        [TestMethod]
        public void Get_GetMortgageGoal()
        {
            //mock Mortgage goal logic
            var mockSGL = new Mock<IMortgageGoalLogic>();
            mockSGL.Setup(x => x.GetMortgageGoal(It.IsAny<int>())).Returns(
                new UserMortgageGoal() {
                    Id = 4,
                    TotalBalance = 100000.0,
                    InterestRate_Percent = 5.0,
                    MonthlyPayment = 2000.0,
                    EstimatedCurrentValue = 200000.0,
                    AdditionalBudgetAllocation = 500
                }
            );

            //Setup the http context (for auth)
            var MortgageController = new MortgageGoalsController(mockSGL.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new UserMortgageGoal()
                {
                Id = 4,
                TotalBalance = 100000.0,
                InterestRate_Percent = 5.0,
                MonthlyPayment = 2000.0,
                EstimatedCurrentValue = 200000.0,
                AdditionalBudgetAllocation = 500
            };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = MortgageController.Get(4);


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Put_UserMortgageGoalUpdated()
        {
            //mock Mortgage goal logic
            var mockSGL = new Mock<IMortgageGoalLogic>();
            mockSGL.Setup(x => x.ModifyUserMortgageGoal(It.IsAny<UserMortgageGoal>()))
                            .Returns(new UserMortgageGoal() {
                                Id = 4,
                                TotalBalance = 100000.0,
                                InterestRate_Percent = 5.0,
                                MonthlyPayment = 2000.0,
                                EstimatedCurrentValue = 200000.0,
                                AdditionalBudgetAllocation = 500
                            });

            //Setup the http context (for auth)
            var MortgageController = new MortgageGoalsController(mockSGL.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new UserMortgageGoal() {
                Id = 4,
                TotalBalance = 100000.0,
                InterestRate_Percent = 5.0,
                MonthlyPayment = 2000.0,
                EstimatedCurrentValue = 200000.0,
                AdditionalBudgetAllocation = 500
            };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = MortgageController.Put(new UserMortgageGoal() {
                Id = 4,
                TotalBalance = 100000.0,
                InterestRate_Percent = 5.0,
                MonthlyPayment = 2000.0,
                EstimatedCurrentValue = 200000.0,
                AdditionalBudgetAllocation = 500
            });


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Get_MortgageTotals()
        {
            //mock Mortgage goal logic
            var mockSGL = new Mock<IMortgageGoalLogic>();
            mockSGL.Setup(x => x.GetMortgageTotals(It.IsAny<int>())).Returns(
                new MortgageGoalTotals()
                {
                    MonthsToPayoff = 240,
                    TotalInterestAccrued = 50000.0,
                    TotalCostToPayoff = 250000.0,
                    AllocationSavings = 60000.0
                });

            //Setup the http context (for auth)
            var MortgageController = new MortgageGoalsController(mockSGL.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new MortgageGoalTotals()
            {
                MonthsToPayoff = 240,
                TotalInterestAccrued = 50000.0,
                TotalCostToPayoff = 250000.0,
                AllocationSavings = 60000.0
            };
            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = MortgageController.GetMortgageTotals(1);

            actual.Should().BeEquivalentTo(expected);
        }


        [TestMethod]
        public void Get_MortgageNetOverTime()
        {
            //mock Mortgage goal logic
            var mockSGL = new Mock<IMortgageGoalLogic>();
            mockSGL.Setup(x => x. GetNetValueOverTime(It.IsAny<int>())).Returns
                (new Dictionary<int, double>()
                {
                    {0, 100000.0 },
                    {1, 74291.15 },
                    {2, 47266.98 },
                    {3, 18860.20 },
                    {4, 0.0 }
                });

            //Setup the http context (for auth)
            var MortgageController = new MortgageGoalsController(mockSGL.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new Dictionary<int, double>()
                {
                    {0, 100000.0 },
                    {1, 74291.15 },
                    {2, 47266.98 },
                    {3, 18860.20 },
                    {4, 0.0 }
                };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = MortgageController.GetNetValueOverTime(1);


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Get_AmortizationTable()
        {
            //mock Mortgage goal logic
            var mockSGL = new Mock<IMortgageGoalLogic>();
            mockSGL.Setup(x => x.GetAmortizationTable(It.IsAny<int>())).Returns
                (new Dictionary<int, List<double>>()
                {
                    {0, new List<Double>() { 100000.0 , 0.0, 0.0 } },
                    {1, new List<Double>() { 74291.16 , 0.0, 3.0 } },
                    {2, new List<Double>() { 47266.99 , 0.0, 2.0 } },
                    {3, new List<Double>() { 18860.22 , 0.0, 1.0 } },
                    {4, new List<Double>() { 0.0 , 100000.0, 9123.11 } }
                });

            //Setup the http context (for auth)
            var MortgageController = new MortgageGoalsController(mockSGL.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new Dictionary<int, List<double>>()
                {
                    {0, new List<Double>() { 100000.0 , 0.0, 0.0 } },
                    {1, new List<Double>() { 74291.16 , 0.0, 3.0 } },
                    {2, new List<Double>() { 47266.99 , 0.0, 2.0 } },
                    {3, new List<Double>() { 18860.22 , 0.0, 1.0 } },
                    {4, new List<Double>() { 0.0 , 100000.0, 9123.11 } }
                };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = MortgageController.GetAmortizationTable(1);


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