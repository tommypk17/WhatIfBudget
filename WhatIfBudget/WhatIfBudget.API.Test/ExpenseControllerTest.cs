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
    public class ExpenseControllerTest
    {
        [TestMethod]
        public void Get_UserHasExpenses()
        {
            //mock expense logic
            var mockExpenseLogic = new Mock<IExpenseLogic>();
            mockExpenseLogic.Setup(x => x.GetUserExpenses(Guid.Empty)).Returns(new List<UserExpense>()
                {
                    new UserExpense() { Id = 1, Amount = 100, Frequency = 0, Priority = 0 },
                    new UserExpense() { Id = 2, Amount = 100, Frequency = 0, Priority = 0  },
                    new UserExpense() { Id = 3, Amount = 100, Frequency = 0, Priority = 0 },
                }
            );

            //Setup the http context (for auth)
            var expenseController = new ExpensesController(mockExpenseLogic.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new List<UserExpense>()
                    {
                        new UserExpense() { Id = 1, Amount = 100, Frequency = 0},
                        new UserExpense() { Id = 2, Amount = 100, Frequency = 0 },
                        new UserExpense() { Id = 3, Amount = 100, Frequency = 0 },
                    };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = expenseController.Get();


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