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
    public class BudgetControllerTest
    {
        [TestMethod]
        public void Get_AllUserBudgets()
        {
            //mock budget logic
            var mockBL = new Mock<IBudgetLogic>();
            mockBL.Setup(x => x.GetUserBudgets(Guid.Empty)).Returns(new List<UserBudget>()
                {
                    new UserBudget() {
                                Id = 1,
                                Name = "test1",
                                SavingGoalId = 1,
                                DebtGoalId = 1,
                                MortgageGoalId = 1,
                                InvestmentGoalId = 1
                    },
                    new UserBudget() {
                                Id = 2,
                                Name = "test2",
                                SavingGoalId = 2,
                                DebtGoalId = 2,
                                MortgageGoalId = 2,
                                InvestmentGoalId = 2
                    },
                    new UserBudget() {
                                Id = 3,
                                Name = "test3",
                                SavingGoalId = 3,
                                DebtGoalId = 3,
                                MortgageGoalId = 3,
                                InvestmentGoalId = 3
                    },
                }
            );

            //Setup the http context (for auth)
            var budgetController = new BudgetsController(mockBL.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new List<UserBudget>()
                    {
                        new UserBudget() {
                                Id = 1,
                                Name = "test1",
                                SavingGoalId = 1,
                                DebtGoalId = 1,
                                MortgageGoalId = 1,
                                InvestmentGoalId = 1
                    },
                    new UserBudget() {
                                Id = 2,
                                Name = "test2",
                                SavingGoalId = 2,
                                DebtGoalId = 2,
                                MortgageGoalId = 2,
                                InvestmentGoalId = 2
                    },
                    new UserBudget() {
                                Id = 3,
                                Name = "test3",
                                SavingGoalId = 3,
                                DebtGoalId = 3,
                                MortgageGoalId = 3,
                                InvestmentGoalId = 3
                    },
                    };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = budgetController.Get();


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Get_UserBudget()
        {
            //mock budget logic
            var mockBL = new Mock<IBudgetLogic>();
            mockBL.Setup(x => x.GetBudget(It.IsAny<int>())).Returns(new UserBudget
                {
                                Id = 2,
                                Name = "test2",
                                SavingGoalId = 2,
                                DebtGoalId = 2,
                                MortgageGoalId = 2,
                                InvestmentGoalId = 2
            }
            );

            //Setup the http context (for auth)
            var budgetController = new BudgetsController(mockBL.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new UserBudget
                    {
                                Id = 2,
                                Name = "test2",
                                SavingGoalId = 2,
                                DebtGoalId = 2,
                                MortgageGoalId = 2,
                                InvestmentGoalId = 2
            };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = budgetController.Get(2);


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Get_AvailableMonthlyNet()
        {
            //mock budget logic
            var mockBL = new Mock<IBudgetLogic>();
            mockBL.Setup(x => x.GetAvailableMonthlyNet(It.IsAny<int>())).Returns(1234.56);

            //Setup the http context (for auth)
            var budgetController = new BudgetsController(mockBL.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = 1234.56;

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = budgetController.GetAvailableMonthlyNet(2);


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Get_CurrentNetWorth()
        {
            //mock budget logic
            var mockBL = new Mock<IBudgetLogic>();
            mockBL.Setup(x => x.GetCurrentNetWorth(It.IsAny<int>())).Returns(123456.78);

            //Setup the http context (for auth)
            var budgetController = new BudgetsController(mockBL.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = 123456.78;

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = budgetController.GetCurrentNetWorth(2);


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Post_UserBudgetCreated()
        {
            //mock investment logic
            var mockBL = new Mock<IBudgetLogic>();
            mockBL.Setup(x => x.CreateUserBudget(Guid.Empty, It.IsAny<UserBudget>()))
                            .Returns(new UserBudget() {
                                Id = 1,
                                Name = "test",
                                SavingGoalId = 1,
                                DebtGoalId = 1,
                                MortgageGoalId = 1,
                                InvestmentGoalId = 1
                            });

            //Setup the http context (for auth)
            var budgetController = new BudgetsController(mockBL.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new UserBudget() {
                Id = 1,
                Name = "test",
                SavingGoalId = 1,
                DebtGoalId = 1,
                MortgageGoalId = 1,
                InvestmentGoalId = 1
            };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = budgetController.Post(new UserBudget() {
                Id = 1,
                Name = "test",
                SavingGoalId = 1,
                DebtGoalId = 1,
                MortgageGoalId = 1,
                InvestmentGoalId = 1
            });

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Put_UserBudgetUpdated()
        {
            //mock budget logic
            var mockBL = new Mock<IBudgetLogic>();
            mockBL.Setup(x => x.ModifyUserBudget(Guid.Empty, It.IsAny<UserBudget>()))
                            .Returns(new UserBudget() {
                                Id = 4,
                                Name = "test4",
                                SavingGoalId = 4,
                                DebtGoalId = 4,
                                MortgageGoalId = 4,
                                InvestmentGoalId = 4
                            });

            //Setup the http context (for auth)
            var budgetController = new BudgetsController(mockBL.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new UserBudget() {
                Id = 4,
                Name = "test4",
                SavingGoalId = 4,
                DebtGoalId = 4,
                MortgageGoalId = 4,
                InvestmentGoalId = 4
            };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = budgetController.Put(new UserBudget() {
                Id = 4,
                Name = "test4",
                SavingGoalId = 4,
                DebtGoalId = 4,
                MortgageGoalId = 4,
                InvestmentGoalId = 4
            });


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Delete_UserBudgetDeleted()
        {
            //mock budget logic
            var mockBL = new Mock<IBudgetLogic>();
            mockBL.Setup(x => x.DeleteUserBudget(It.IsAny<int>()))
                            .Returns(new UserBudget() {
                                Id = 1,
                                Name = "test",
                                SavingGoalId = 1,
                                DebtGoalId = 1,
                                MortgageGoalId = 1,
                                InvestmentGoalId = 1
                            });

            //Setup the http context (for auth)
            var budgetController = new BudgetsController(mockBL.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new UserBudget()
            {
                Id = 1,
                Name = "test",
                SavingGoalId = 1,
                DebtGoalId = 1,
                MortgageGoalId = 1,
                InvestmentGoalId = 1
            };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = budgetController.Delete(It.IsAny<int>());


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Get_UserBudgetAdditionalContributions()
        {
            //mock budget logic
            var mockBL = new Mock<IBudgetLogic>();
            mockBL.Setup(x => x.GetUserBudgetAllocations(It.IsAny<int>())).Returns(new UserBudgetAllocations
            {
                DebtGoal = 100,
                InvestmentGoal = 145,
                SavingGoal = 110,
                MortgageGoal = 50
            });

            //Setup the http context (for auth)
            var budgetController = new BudgetsController(mockBL.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new UserBudgetAllocations
            {
                DebtGoal = 100,
                InvestmentGoal = 145,
                SavingGoal = 110,
                MortgageGoal = 50
            };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = budgetController.GetAdditionalContributions(It.IsAny<int>());


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Put_UserBudgetAdditionalContributions()
        {
            //mock budget logic
            var mockBL = new Mock<IBudgetLogic>();
            mockBL.Setup(x => x.UpdateUserBudgetAllocations(It.IsAny<int>(), It.IsAny<UserBudgetAllocations>())).Returns(new UserBudgetAllocations
            {
                DebtGoal = 100,
                InvestmentGoal = 145,
                SavingGoal = 110,
                MortgageGoal = 50
            });

            //Setup the http context (for auth)
            var budgetController = new BudgetsController(mockBL.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new UserBudgetAllocations
            {
                DebtGoal = 100,
                InvestmentGoal = 145,
                SavingGoal = 110,
                MortgageGoal = 50
            };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = budgetController.UpdateAdditionalContributions(It.IsAny<int>(), It.IsAny<UserBudgetAllocations>());


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Get_UserBudgetAvailableFreeCash()
        {
            //mock budget logic
            var mockBL = new Mock<IBudgetLogic>();
            mockBL.Setup(x => x.GetBudgetAvailableFreeCash(It.IsAny<int>())).Returns(50.0);

            //Setup the http context (for auth)
            var budgetController = new BudgetsController(mockBL.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = 50.0;

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = budgetController.GetAvailableFreeCash(It.IsAny<int>());


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