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
    public class MonthlyIncomeControllerTest
    {
        [TestMethod]
        public void Get_BudgetMonthlyIncome()
        {
            //mock income logic
            var mockMonthlyIncomeLogic = new Mock<IMonthlyIncomeLogic>();
            mockMonthlyIncomeLogic.Setup(x => x.GetBudgetMonthlyIncome(It.IsAny<int>())).Returns(5000);

            //Setup the http context (for auth)
            var monthlyIncomeController = new MonthlyIncomeController(mockMonthlyIncomeLogic.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = 5000;

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = monthlyIncomeController.Get(1);


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