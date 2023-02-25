using FluentAssertions;
using Moq;
using WhatIfBudget.Services.Interfaces;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Services;
using WhatIfBudget.Logic.Models;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Common.Enumerations;

namespace WhatIfBudget.Logic.Test
{
    [TestClass]
    public class InvestmentLogicTest
    {
        public InvestmentLogicTest()
        {

        }

        [TestMethod]
        public void GetAllInvestments_CollectionAreEqual()
        {
            var mockIS = new Mock<IInvestmentService>();
            mockIS.Setup(x => x.GetAllInvestments()).Returns(
                (IList<Investment>)new List<Investment>()
                {
                    new Investment()
                    {
                        Id = 1,
                        Name = "test",
                        CurrentBalance = 0,
                        MonthlyEmployerContribution = 0,
                        MonthlyPersonalContribution = 0,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    },
                    new Investment()
                    {
                        Id = 2,
                        Name = "test2",
                        CurrentBalance = 0,
                        MonthlyEmployerContribution = 0,
                        MonthlyPersonalContribution = 0,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    },
                    new Investment()
                    {
                        Id = 3,
                        Name = "test3",
                        CurrentBalance = 0,
                        MonthlyEmployerContribution = 0,
                        MonthlyPersonalContribution = 0,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                });
            

            var investmentLogic = new InvestmentLogic(mockIS.Object);

            var expected = new List<UserInvestment>()
            {
                new UserInvestment() {
                    Id = 1,
                    Name = "test",
                    GoalId = 0,
                    CurrentBalance = 0,
                    MonthlyPersonalContribution = 0,
                    MonthlyEmployerContribution = 0
                },
                new UserInvestment() {
                    Id = 2,
                    Name = "test2",
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

            var actual = investmentLogic.GetUserInvestments(Guid.Empty);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void AddUserInvestment_CollectionAreEqual()
        {
            var mock = new Mock<IInvestmentService>();

            mock.Setup(x => x.AddNewInvestment(It.IsAny<Investment>())).Returns(
                new Investment()
                {
                    Id = 1,
                    Name = "test",
                    CurrentBalance = 0,
                    MonthlyEmployerContribution = 0,
                    MonthlyPersonalContribution = 0,
                    UserId = Guid.Empty,
                    CreatedOn = DateTime.MinValue,
                    UpdatedOn = DateTime.MinValue
                }
            );

            var investmentLogic = new InvestmentLogic(mock.Object);

            var expected = new UserInvestment()
            {
                Id = 1,
                Name = "test",
                GoalId = 0,
                CurrentBalance = 0,
                MonthlyPersonalContribution = 0,
                MonthlyEmployerContribution = 0
            };

            var actual = investmentLogic.AddUserInvestment(Guid.Empty, new UserInvestment()
            {
                Id = 1,
                Name = "test",
                GoalId = 0,
                CurrentBalance = 0,
                MonthlyPersonalContribution = 0,
                MonthlyEmployerContribution = 0
            });

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void ModifyUserInvestment_ObjectsAreEqual()
        {
            var mock = new Mock<IInvestmentService>();

            mock.Setup(x => x.UpdateInvestment(It.IsAny<Investment>())).Returns(
                    new Investment()
                    {
                        Id = 11,
                        Name = "Test",
                        CurrentBalance = 1,
                        MonthlyEmployerContribution = 0,
                        MonthlyPersonalContribution = 0,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                );
            var investmentLogic = new InvestmentLogic(mock.Object);

            var expected = new UserInvestment()
            {
                Id = 11,
                Name = "Test",
                GoalId = 0,
                CurrentBalance = 1,
                MonthlyPersonalContribution = 0,
                MonthlyEmployerContribution = 0
            };

            var actual = investmentLogic.ModifyUserInvestment(Guid.Empty, new UserInvestment()
            {
                Id = 11,
                Name = "Test",
                GoalId = 0,
                CurrentBalance = 1,
                MonthlyPersonalContribution = 0,
                MonthlyEmployerContribution = 0
            });

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void DeleteUserInvestment_CollectionsAreEqual()
        {
            var mock = new Mock<IInvestmentService>();

            mock.Setup(x => x.DeleteInvestment(It.IsAny<int>())).Returns(
                    new Investment()
                    {
                        Id = 1,
                        Name = "Test",
                        CurrentBalance = 1,
                        MonthlyEmployerContribution = 0,
                        MonthlyPersonalContribution = 0,
                        UserId = Guid.Empty,
                        CreatedOn = DateTime.MinValue,
                        UpdatedOn = DateTime.MinValue
                    }
                );

            mock.Setup(x => x.GetAllInvestments()).Returns(
                    new List<Investment>(){
                        new Investment()
                        {
                            Id = 1,
                            Name = "Test",
                            CurrentBalance = 1,
                            MonthlyEmployerContribution = 0,
                            MonthlyPersonalContribution = 0,
                            UserId = Guid.Empty,
                            CreatedOn = DateTime.MinValue,
                            UpdatedOn = DateTime.MinValue
                        }
                    }
                );

            var investmentLogic = new InvestmentLogic(mock.Object);

            var expected = new UserInvestment()
            {
                Id = 1,
                Name = "Test",
                GoalId = 0,
                CurrentBalance = 1,
                MonthlyPersonalContribution = 0,
                MonthlyEmployerContribution = 0
            };

            var actual = investmentLogic.DeleteInvestment(1);

            actual.Should().BeEquivalentTo(expected);
        }

    }
}