﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Logic.Models;
using WhatIfBudget.Services;
using WhatIfBudget.Services.Interfaces;

namespace WhatIfBudget.Logic
{
    public class InvestmentLogic : IInvestmentLogic
    {
        private readonly IInvestmentService _investmentService;
        private readonly IInvestmentGoalInvestmentService _igiService;

        public InvestmentLogic(IInvestmentService investmentService, IInvestmentGoalInvestmentService investmentGoalInvestmentService)
        {
            _investmentService = investmentService;
            _igiService = investmentGoalInvestmentService;
        }
        public IList<UserInvestment> GetUserInvestments(Guid userId)
        {
            return _investmentService.GetAllInvestments()
                                    .Where(x => x.UserId == userId)
                                    .Select(x => UserInvestment.FromInvestment(x))
                                    .ToList();
        }

        public IList<UserInvestment> GetUserInvestmentsByGoalId( int goalId)
        {
            var investments = _investmentService.GetInvestmentsByInvestmentGoalId(goalId)
                                .Select(x => UserInvestment.FromInvestment(x, goalId))
                                .ToList();
            if (investments.Any())
            {
                return investments;
            }
            else
            {
                return new List<UserInvestment>();
            }
        }

        public UserInvestment? AddUserInvestment(Guid userId, UserInvestment userInvestment)
        {
            var dbInvestment = _investmentService.AddNewInvestment(userInvestment.ToInvestment(userId));
            if (dbInvestment == null)
            {
                throw new NullReferenceException();
            }
            if(userInvestment.GoalId > 0)
            {
                var dbIGI = new InvestmentGoalInvestment()
                {
                    InvestmentGoalId = userInvestment.GoalId,
                    InvestmentId = dbInvestment.Id
                };
                dbIGI = _igiService.AddNewInvestmentGoalInvestment(dbIGI);
                if(dbIGI == null)
                {
                    throw new NullReferenceException();
                }
            }

            return UserInvestment.FromInvestment(dbInvestment);
        }

        public UserInvestment? ModifyUserInvestment(Guid userId, UserInvestment investment)
        {
            var toUpdate = investment.ToInvestment(userId);

            var dbInvestment = _investmentService.UpdateInvestment(toUpdate);
            if (dbInvestment == null) { throw new NullReferenceException(); }
            return UserInvestment.FromInvestment(dbInvestment);
        }

        public UserInvestment? DeleteInvestment(int investmentId, int investmentGoalId)
        {
            // De-associate investment from current goal
            var idToDelete = _igiService.GetAllInvestmentGoalInvestments()
                .Where(x => x.InvestmentId == investmentId && x.InvestmentGoalId == investmentGoalId)
                .Select(x => x.Id)
                .FirstOrDefault();
            var dbIGI = _igiService.DeleteInvestmentGoalInvestment(idToDelete);
            if (dbIGI == null) { throw new NullReferenceException() ; }

            // Delete investment if it has no remaining associations
            if (!_igiService.GetAllInvestmentGoalInvestments()
                    .Where(x => x.InvestmentId == investmentId)
                    .Any())
            {
                var dbDeleteInvestment = _investmentService.DeleteInvestment(dbIGI.InvestmentId);
                if (dbDeleteInvestment == null) { throw new NullReferenceException(); }
                return UserInvestment.FromInvestment(dbDeleteInvestment);
            }
            else
            {
                // Keep investment for use by other budgets
                var dbDeleteInvestment = _investmentService.GetAllInvestments()
                    .Where(x => x.Id == investmentId)
                    .FirstOrDefault();
                if (dbDeleteInvestment == null) { throw new NullReferenceException(); }
                else
                {
                    return UserInvestment.FromInvestment(dbDeleteInvestment);
                }
            }
        }
    }
}
