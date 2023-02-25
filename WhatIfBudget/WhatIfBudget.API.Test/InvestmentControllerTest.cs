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
    public class InvestmentControllerTest
    {
        [TestMethod]
        public void Get_UserHasInvestments()
        {
            //mock investment logic
            var mockInvestmentLogic = new Mock<IInvestmentLogic>();
            mockInvestmentLogic.Setup(x => x.GetUserInvestments(Guid.Empty)).Returns(new List<UserInvestment>()
                {
                    new UserInvestment() {
                                Id = 1,
                                Name = "test1",
                                GoalId = 0,
                                CurrentBalance = 0,
                                MonthlyPersonalContribution = 0,
                                MonthlyEmployerContribution = 0
                    },
                    new UserInvestment() {
                                Id = 2,
                                Name = "test3",
                                GoalId = 0,
                                CurrentBalance = 0,
                                MonthlyPersonalContribution = 0,
                                MonthlyEmployerContribution = 0
                    },
                    new UserInvestment() {
                                Id = 3,
                                Name = "test3",
                                GoalId = 0,
                                CurrentBalance = 0,
                                MonthlyPersonalContribution = 0,
                                MonthlyEmployerContribution = 0
                    },
                }
            );

            //Setup the http context (for auth)
            var investmentController = new InvestmentsController(mockInvestmentLogic.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new List<UserInvestment>()
                    {
                        new UserInvestment() {
                                Id = 1,
                                Name = "test1",
                                GoalId = 0,
                                CurrentBalance = 0,
                                MonthlyPersonalContribution = 0,
                                MonthlyEmployerContribution = 0
                    },
                    new UserInvestment() {
                                Id = 2,
                                Name = "test3",
                                GoalId = 0,
                                CurrentBalance = 0,
                                MonthlyPersonalContribution = 0,
                                MonthlyEmployerContribution = 0
                    },
                    new UserInvestment() {
                                Id = 3,
                                Name = "test3",
                                GoalId = 0,
                                CurrentBalance = 0,
                                MonthlyPersonalContribution = 0,
                                MonthlyEmployerContribution = 0
                    },
                    };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = investmentController.Get();


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Post_UserInvestmentCreated()
        {
            //mock investment logic
            var mockInvestmentLogic = new Mock<IInvestmentLogic>();
            mockInvestmentLogic.Setup(x => x.AddUserInvestment(Guid.Empty, It.IsAny<UserInvestment>()))
                            .Returns(new UserInvestment() {
                                Id = 1,
                                Name = "test",
                                GoalId = 0,
                                CurrentBalance = 0,
                                MonthlyPersonalContribution = 0,
                                MonthlyEmployerContribution = 0
                            });

            //Setup the http context (for auth)
            var investmentController = new InvestmentsController(mockInvestmentLogic.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new UserInvestment() {
                Id = 1,
                Name = "test",
                GoalId = 0,
                CurrentBalance = 0,
                MonthlyPersonalContribution = 0,
                MonthlyEmployerContribution = 0
            };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = investmentController.Post(new UserInvestment() {
                Id = 1,
                Name = "test",
                GoalId = 0,
                CurrentBalance = 0,
                MonthlyPersonalContribution = 0,
                MonthlyEmployerContribution = 0
            });


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