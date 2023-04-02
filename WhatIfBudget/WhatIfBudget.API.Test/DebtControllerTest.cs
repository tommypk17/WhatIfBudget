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
using WhatIfBudget.Common.Enumerations;

namespace WhatIfBudget.API.Test
{
    [TestClass]
    public class DebtControllerTest
    {
        [TestMethod]
        public void Get_UserHasDebts()
        {
            //mock expense logic
            var mockDebtLogic = new Mock<IDebtLogic>();
            mockDebtLogic.Setup(x => x.GetUserDebts(Guid.Empty)).Returns(new List<UserDebt>()
                {
                    new UserDebt() {Id = 1, Name = "Test", CurrentBalance = 100, InterestRate = 0.1f, MinimumPayment = 0},
                    new UserDebt() {Id = 2, Name = "Test", CurrentBalance = 100, InterestRate = 0.1f, MinimumPayment = 0},
                    new UserDebt() {Id = 3, Name = "Test", CurrentBalance = 100, InterestRate = 0.1f, MinimumPayment = 0},
                }
            );

            //Setup the http context (for auth)
            var debtController = new DebtsController(mockDebtLogic.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new List<UserDebt>()
                    {
                        new UserDebt() {Id = 1, Name = "Test", CurrentBalance = 100, InterestRate = 0.1f, MinimumPayment = 0},
                        new UserDebt() {Id = 2, Name = "Test", CurrentBalance = 100, InterestRate = 0.1f, MinimumPayment = 0},
                        new UserDebt() {Id = 3, Name = "Test", CurrentBalance = 100, InterestRate = 0.1f, MinimumPayment = 0},
                    };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = debtController.Get();


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Post_UserDebtCreated()
        {
            //mock debt logic
            var mockDebtLogic = new Mock<IDebtLogic>();
            mockDebtLogic.Setup(x => x.AddUserDebt(Guid.Empty, It.IsAny<UserDebt>()))
                            .Returns(new UserDebt()
                            {
                                Id = 1,
                                Name = "test",
                                GoalId = 0,
                                CurrentBalance = 0,
                                InterestRate = 0.1f,
                                MinimumPayment = 0
                            });

            //Setup the http context (for auth)
            var debtController = new DebtsController(mockDebtLogic.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new UserDebt()
            {
                Id = 1,
                Name = "test",
                GoalId = 0,
                CurrentBalance = 0,
                InterestRate = 0.1f,
                MinimumPayment = 0
            };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = debtController.Post(new UserDebt()
            {
                Id = 1,
                Name = "test",
                GoalId = 0,
                CurrentBalance = 0,
                InterestRate = 0.1f,
                MinimumPayment = 0
            });


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Put_UserDebtUpdated()
        {
            //mock income logic
            var mock = new Mock<IDebtLogic>();
            mock.Setup(x => x.ModifyUserDebt(Guid.Empty, It.IsAny<UserDebt>()))
                            .Returns(new UserDebt()
                            {
                                Id = 1,
                                Name = "test",
                                GoalId = 0,
                                CurrentBalance = 0,
                                InterestRate = 0.1f,
                                MinimumPayment = 0
                            });

            //Setup the http context (for auth)
            var debtController = new DebtsController(mock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new UserDebt()
            {
                Id = 1,
                Name = "test",
                GoalId = 0,
                CurrentBalance = 0,
                InterestRate = 0.1f,
                MinimumPayment = 0
            };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = debtController.Put(new UserDebt()
            {
                Id = 1,
                Name = "test",
                GoalId = 0,
                CurrentBalance = 0,
                InterestRate = 0.1f,
                MinimumPayment = 0
            });


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Delete_UserDebtDeleted()
        {
            //mock income logic
            var mock = new Mock<IDebtLogic>();
            mock.Setup(x => x.DeleteDebt(It.IsAny<int>(), It.IsAny<int>()))
                            .Returns(new UserDebt()
                            {
                                Id = 1,
                                Name = "test",
                                GoalId = 0,
                                CurrentBalance = 0,
                                InterestRate= 0,
                                MinimumPayment = 0
                            });

            //Setup the http context (for auth)
            var debtController = new DebtsController(mock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new UserDebt()
            {
                Id = 1,
                Name = "test",
                GoalId = 0,
                CurrentBalance = 0,
                InterestRate= 0,
                MinimumPayment = 0
            };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = debtController.Delete(1, 1);


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
