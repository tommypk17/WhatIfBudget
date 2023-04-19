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
            var mockIGIS = new Mock<IInvestmentGoalInvestmentService>();

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
            

            var investmentLogic = new InvestmentLogic(mockIS.Object, mockIGIS.Object);

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
        public void GetInvestmentsByGoalId_CollectionAreEqual()
        {
            var mockIS = new Mock<IInvestmentService>();
            var mockIGIS = new Mock<IInvestmentGoalInvestmentService>();

            mockIS.Setup(x => x.GetInvestmentsByInvestmentGoalId(It.IsAny<int>())).Returns(
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
                    }
                });


            var investmentLogic = new InvestmentLogic(mockIS.Object, mockIGIS.Object);

            var expected = new List<UserInvestment>()
            {
                new UserInvestment() {
                    Id = 1,
                    Name = "test",
                    GoalId = 1,
                    CurrentBalance = 0,
                    MonthlyPersonalContribution = 0,
                    MonthlyEmployerContribution = 0
                },
                new UserInvestment() {
                    Id = 2,
                    Name = "test2",
                    GoalId = 1,
                    CurrentBalance = 0,
                    MonthlyPersonalContribution = 0,
                    MonthlyEmployerContribution = 0
                }
            };

            var actual = investmentLogic.GetUserInvestmentsByGoalId(1);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void AddExistingUserInvestment_CollectionAreEqual()
        {
            var mock = new Mock<IInvestmentService>();
            var mockIGIS = new Mock<IInvestmentGoalInvestmentService>();

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

            var investmentLogic = new InvestmentLogic(mock.Object, mockIGIS.Object);

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
        public void AddNewUserInvestment_CollectionAreEqual()
        {
            var mockIS = new Mock<IInvestmentService>();
            var mockIGIS = new Mock<IInvestmentGoalInvestmentService>();

            mockIS.Setup(x => x.AddNewInvestment(It.IsAny<Investment>())).Returns(
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

            mockIGIS.Setup(x => x.AddNewInvestmentGoalInvestment(It.IsAny<InvestmentGoalInvestment>())).Returns(
                new InvestmentGoalInvestment()
                {
                    Id = 2,
                    InvestmentId = 1,
                    InvestmentGoalId = 2
                });

            var investmentLogic = new InvestmentLogic(mockIS.Object, mockIGIS.Object);

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
                GoalId = 2,
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
            var mockIGIS = new Mock<IInvestmentGoalInvestmentService>();

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
            var investmentLogic = new InvestmentLogic(mock.Object, mockIGIS.Object);

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
            var mockIGIS = new Mock<IInvestmentGoalInvestmentService>();

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
                        },
                        new Investment()
                        {
                            Id = 2,
                            Name = "Test2",
                            CurrentBalance = 20,
                            MonthlyEmployerContribution = 2,
                            MonthlyPersonalContribution = 2,
                            UserId = Guid.Empty,
                            CreatedOn = DateTime.MinValue,
                            UpdatedOn = DateTime.MinValue
                        }
                    }
                );

            mockIGIS.Setup(x => x.GetAllInvestmentGoalInvestments()).Returns(
                new List<InvestmentGoalInvestment>()
                {
                    new InvestmentGoalInvestment()
                    {
                        Id = 1,
                        InvestmentId = 1,
                        InvestmentGoalId = 1
                    },
                    new InvestmentGoalInvestment()
                    {
                        Id = 1,
                        InvestmentId = 2,
                        InvestmentGoalId = 2
                    }

                });

            mockIGIS.Setup(x => x.DeleteInvestmentGoalInvestment(It.IsAny<int>())).Returns(
                    new InvestmentGoalInvestment()
                    {
                        Id = 1,
                        InvestmentId = 1,
                        InvestmentGoalId = 1
                    });


            var investmentLogic = new InvestmentLogic(mock.Object, mockIGIS.Object);

            var expected = new UserInvestment()
            {
                Id = 1,
                Name = "Test",
                GoalId = 0,
                CurrentBalance = 1,
                MonthlyPersonalContribution = 0,
                MonthlyEmployerContribution = 0
            };

            var actual = investmentLogic.DeleteInvestment(1, 1);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void DeleteGoalInvestment_CollectionsAreEqual()
        {
            var mock = new Mock<IInvestmentService>();
            var mockIGIS = new Mock<IInvestmentGoalInvestmentService>();

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
                        },
                        new Investment()
                        {
                            Id = 2,
                            Name = "Test2",
                            CurrentBalance = 20,
                            MonthlyEmployerContribution = 2,
                            MonthlyPersonalContribution = 2,
                            UserId = Guid.Empty,
                            CreatedOn = DateTime.MinValue,
                            UpdatedOn = DateTime.MinValue
                        }
                    }
                );

            mockIGIS.Setup(x => x.GetAllInvestmentGoalInvestments()).Returns(
                new List<InvestmentGoalInvestment>()
                {
                    new InvestmentGoalInvestment()
                    {
                        Id = 1,
                        InvestmentId = 1,
                        InvestmentGoalId = 1
                    },
                    new InvestmentGoalInvestment()
                    {
                        Id = 1,
                        InvestmentId = 1,
                        InvestmentGoalId = 2
                    }

                });

            mockIGIS.Setup(x => x.DeleteInvestmentGoalInvestment(It.IsAny<int>())).Returns(
                    new InvestmentGoalInvestment()
                    {
                        Id = 1,
                        InvestmentId = 1,
                        InvestmentGoalId = 1
                    });


            var investmentLogic = new InvestmentLogic(mock.Object, mockIGIS.Object);

            var expected = new UserInvestment()
            {
                Id = 1,
                Name = "Test",
                GoalId = 0,
                CurrentBalance = 1,
                MonthlyPersonalContribution = 0,
                MonthlyEmployerContribution = 0
            };

            var actual = investmentLogic.DeleteInvestment(1, 1);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}