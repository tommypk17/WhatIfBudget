using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using WhatIfBudget.API.Controllers;
using WhatIfBudget.Common.Enumerations;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Logic;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Logic.Models;

namespace WhatIfBudget.API.Test
{
    [TestClass]
    public class IncomeControllerTest
    {
        [TestMethod]
        public void Get_UserHasIncomes()
        {
            //mock income logic
            var mockIncomeLogic = new Mock<IIncomeLogic>();
            mockIncomeLogic.Setup(x => x.GetUserIncomes(Guid.Empty)).Returns(new List<UserIncome>()
                {
                    new UserIncome() { Id = 1, Amount = 100, Frequency = EFrequency.None },
                    new UserIncome() { Id = 2, Amount = 100, Frequency = EFrequency.Weekly },
                    new UserIncome() { Id = 3, Amount = 100, Frequency = EFrequency.Monthly },
                }
            );

            //Setup the http context (for auth)
            var incomeController = new IncomesController(mockIncomeLogic.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new List<UserIncome>()
                    {
                        new UserIncome() { Id = 1, Amount = 100, Frequency = EFrequency.None },
                        new UserIncome() { Id = 2, Amount = 100, Frequency = EFrequency.Weekly },
                        new UserIncome() { Id = 3, Amount = 100, Frequency = EFrequency.Monthly },
                    };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = incomeController.Get();


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Get_IncomesByBudgetId()
        {
            //mock income logic
            var mockIncomeLogic = new Mock<IIncomeLogic>();
            mockIncomeLogic.Setup(x => x.GetBudgetIncomes(It.IsAny<int>())).Returns(new List<UserIncome>()
                {
                    new UserIncome() { Id = 1, Amount = 100, Frequency = 0 },
                    new UserIncome() { Id = 2, Amount = 100, Frequency = 0 },
                    new UserIncome() { Id = 3, Amount = 100, Frequency = 0 },
                }
            );

            //Setup the http context (for auth)
            var incomeController = new IncomesController(mockIncomeLogic.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new List<UserIncome>()
                    {
                        new UserIncome() { Id = 1, Amount = 100, Frequency = 0},
                        new UserIncome() { Id = 2, Amount = 100, Frequency = 0 },
                        new UserIncome() { Id = 3, Amount = 100, Frequency = 0 },
                    };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = incomeController.Get(1);


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Get_BudgetMonthlyIncome()
        {
            //mock income logic
            var mockIL = new Mock<IIncomeLogic>();
            mockIL.Setup(x => x.GetBudgetMonthlyIncome(It.IsAny<int>())).Returns(5000.0);

            //Setup the http context (for auth)
            var monthlyIncomeController = new IncomesController(mockIL.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = 5000.0;

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = monthlyIncomeController.GetMonthlyIncome(1);



            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Post_UserIncomeCreated()
        {
            //mock income logic
            var mockIncomeLogic = new Mock<IIncomeLogic>();
            mockIncomeLogic.Setup(x => x.AddUserIncome(Guid.Empty, It.IsAny<UserIncome>()))
                            .Returns(new UserIncome() { Id = 1, Amount = 100, Frequency = 0 });

            //Setup the http context (for auth)
            var incomeController = new IncomesController(mockIncomeLogic.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new UserIncome() { Id = 1, Amount = 100, Frequency = 0 };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = incomeController.Post(new UserIncome() { Id = 1, Amount = 100, Frequency = 0 });


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Put_UserIncomeUpdated()
        {
            //mock income logic
            var mockIncomeLogic = new Mock<IIncomeLogic>();
            mockIncomeLogic.Setup(x => x.ModifyUserIncome(Guid.Empty, It.IsAny<UserIncome>()))
                            .Returns(new UserIncome() { Id = 1, Amount = 101, Frequency = 0 });

            //Setup the http context (for auth)
            var incomeController = new IncomesController(mockIncomeLogic.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new UserIncome() { Id = 1, Amount = 101, Frequency = 0 };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = incomeController.Put(new UserIncome() { Id = 1, Amount = 101, Frequency = 0 });


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Delete_UserIncomeDeleted()
        {
            //mock income logic
            var mockIncomeLogic = new Mock<IIncomeLogic>();
            mockIncomeLogic.Setup(x => x.DeleteBudgetIncome(1, It.IsAny<int>()))
                            .Returns(new UserIncome() { Id = 1, Amount = 100, Frequency = 0 });

            //Setup the http context (for auth)
            var incomeController = new IncomesController(mockIncomeLogic.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new UserIncome() { Id = 1, Amount = 100, Frequency = 0 };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = incomeController.Delete(1, 1);


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