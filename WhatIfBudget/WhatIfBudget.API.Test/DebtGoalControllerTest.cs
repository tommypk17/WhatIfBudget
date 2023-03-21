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