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
    public class ExpenseControllerTest
    {
        [TestMethod]
        public void Get_UserHasExpenses()
        {
            //mock expense logic
            var mockExpenseLogic = new Mock<IExpenseLogic>();
            mockExpenseLogic.Setup(x => x.GetUserExpenses(Guid.Empty)).Returns(new List<UserExpense>()
                {
                    new UserExpense() { Id = 1, Amount = 100, Frequency = EFrequency.None, Priority = EPriority.Need },
                    new UserExpense() { Id = 2, Amount = 100, Frequency = EFrequency.Weekly, Priority = EPriority.Need },
                    new UserExpense() { Id = 3, Amount = 100, Frequency = EFrequency.Monthly, Priority = EPriority.Want },
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
                        new UserExpense() { Id = 1, Amount = 100, Frequency = EFrequency.None, Priority = EPriority.Need },
                        new UserExpense() { Id = 2, Amount = 100, Frequency = EFrequency.Weekly, Priority = EPriority.Need },
                        new UserExpense() { Id = 3, Amount = 100, Frequency = EFrequency.Monthly, Priority = EPriority.Want },
                    };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = expenseController.Get();


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Get_BudgetMonthlyExpense()
        {
            //mock expense logic
            var mockEL = new Mock<IExpenseLogic>();
            mockEL.Setup(x => x.GetBudgetMonthlyExpense(It.IsAny<int>())).Returns(5000);

            //Setup the http context (for auth)
            var monthlyExpenseController = new ExpensesController(mockEL.Object)
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

            var actual = monthlyExpenseController.GetMonthlyExpense(1);


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Get_BudgetNeedExpense()
        {
            //mock expense logic
            var mockEL = new Mock<IExpenseLogic>();
            mockEL.Setup(x => x.GetBudgetMonthlyNeed(It.IsAny<int>())).Returns(5000);

            //Setup the http context (for auth)
            var monthlyNeedController = new ExpensesController(mockEL.Object)
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

            var actual = monthlyNeedController.GetMonthlyNeeds(1);


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Get_BudgetWantExpense()
        {
            //mock expense logic
            var mockEL = new Mock<IExpenseLogic>();
            mockEL.Setup(x => x.GetBudgetMonthlyWant(It.IsAny<int>())).Returns(5000);

            //Setup the http context (for auth)
            var monthlyWantController = new ExpensesController(mockEL.Object)
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

            var actual = monthlyWantController.GetMonthlyWants(1);


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Post_UserExpenseCreated()
        {
            //mock expense logic
            var mockExpenseLogic = new Mock<IExpenseLogic>();
            mockExpenseLogic.Setup(x => x.AddUserExpense(Guid.Empty, It.IsAny<UserExpense>()))
                            .Returns(new UserExpense() { Id = 1, Amount = 100, Frequency = EFrequency.None, Priority = EPriority.Need });

            //Setup the http context (for auth)
            var expenseController = new ExpensesController(mockExpenseLogic.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new UserExpense() { Id = 1, Amount = 100, Frequency = EFrequency.None, Priority = EPriority.Need };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = expenseController.Post(new UserExpense() { Id = 1, Amount = 100, Frequency = EFrequency.None, Priority = EPriority.Need });


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Put_UserExpenseUpdated()
        {
            //mock expense logic
            var mockExpenseLogic = new Mock<IExpenseLogic>();
            mockExpenseLogic.Setup(x => x.ModifyUserExpense(Guid.Empty, It.IsAny<UserExpense>()))
                            .Returns(new UserExpense() { Id = 1, Amount = 101, Frequency = EFrequency.None, Priority = EPriority.Need });

            //Setup the http context (for auth)
            var expenseController = new ExpensesController(mockExpenseLogic.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new UserExpense() { Id = 1, Amount = 101, Frequency = EFrequency.None, Priority = EPriority.Need };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = expenseController.Put(new UserExpense() { Id = 1, Amount = 101, Frequency = EFrequency.None, Priority = EPriority.Need });


            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Delete_UserExpenseDeleted()
        {
            //mock expense logic
            var mockExpenseLogic = new Mock<IExpenseLogic>();
            mockExpenseLogic.Setup(x => x.DeleteBudgetExpense(1, It.IsAny<int>()))
                            .Returns(new UserExpense() { Id = 1, Amount = 100, Frequency = 0, Priority = EPriority.Need });

            //Setup the http context (for auth)
            var expenseController = new ExpensesController(mockExpenseLogic.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Helper_MockHttpContext().Object
                }
            };

            var expectedValue = new UserExpense() { Id = 1, Amount = 100, Frequency = EFrequency.None, Priority = EPriority.Need };

            var expected = new ObjectResult(expectedValue)
            {
                StatusCode = 200,
            };

            var actual = expenseController.Delete(1,1);


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