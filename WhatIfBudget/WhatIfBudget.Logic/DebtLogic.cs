using System;
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
    public class DebtLogic : IDebtLogic
    {
        private readonly IDebtService _debtService;
        private readonly IDebtGoalDebtService _dgdService;

        public DebtLogic(IDebtService debtService, IDebtGoalDebtService dgdService)
        {
            _debtService = debtService;
            _dgdService = dgdService;
        }

        public IList<UserDebt> GetUserDebts(Guid userId)
        {
            return _debtService.GetAllDebts().Where(x => x.UserId == userId)
                                       .Select(x => UserDebt.FromDebt(x))
                                       .ToList();
        }

        public IList<UserDebt> GetUserDebtsByGoalId(Guid userId, int goalId)
        {
            var debtGoalDebts = _dgdService.GetAllDebtGoalDebts().Where(x => x.DebtGoalId == goalId)
                                                                    .Select(x => x.DebtId).ToList();
            if (debtGoalDebts.Any())
            {
                return _debtService.GetAllDebts()
                                        .Where(x => debtGoalDebts.Contains(x.Id))
                                        .Select(x => UserDebt.FromDebt(x, goalId))
                                        .ToList();
            }
            else
            {
                return new List<UserDebt>();
            }
        }

        public UserDebt? AddUserDebt(Guid userId, UserDebt userDebt)
        {
            var dbDebt = _debtService.AddNewDebt(userDebt.ToDebt(userId));
            if (dbDebt == null)
            {
                throw new NullReferenceException();
            }
            if (userDebt.GoalId > 0)
            {
                var dbDGD = new DebtGoalDebt()
                {
                    DebtGoalId = userDebt.GoalId,
                    DebtId = dbDebt.Id
                };
                dbDGD = _dgdService.AddNewDebtGoalDebt(dbDGD);
                if (dbDGD == null)
                {
                    throw new NullReferenceException();
                }
            }

            return UserDebt.FromDebt(dbDebt);
        }

        public UserDebt? ModifyUserDebt(Guid userId, UserDebt debt)
        {
            var toUpdate = debt.ToDebt(userId);

            var dbDebt = _debtService.UpdateDebt(toUpdate);
            if (dbDebt == null) { throw new NullReferenceException(); }
            return UserDebt.FromDebt(dbDebt);
        }

        public UserDebt? DeleteDebt(int debtId, int debtGoalId)
        {
            // De-associate debt from current goal
            var idToDelete = _dgdService.GetAllDebtGoalDebts()
                .Where(x => x.DebtId == debtId && x.DebtGoalId == debtGoalId)
                .Select(x => x.Id)
                .FirstOrDefault();
            var dbIGI = _dgdService.DeleteDebtGoalDebt(idToDelete);
            if (dbIGI == null) { throw new NullReferenceException(); }

            // Delete debt if it has no remaining associations
            if (!_dgdService.GetAllDebtGoalDebts()
                    .Where(x => x.DebtId == debtId)
                    .Any())
            {
                var dbDeleteDebt = _debtService.DeleteDebt(dbIGI.DebtId);
                if (dbDeleteDebt == null) { throw new NullReferenceException(); }
                return UserDebt.FromDebt(dbDeleteDebt);
            }
            else
            {
                // Keep debt for use by other budgets
                var dbDeleteDebt = _debtService.GetAllDebts()
                    .Where(x => x.Id == debtId)
                    .FirstOrDefault();
                if (dbDeleteDebt == null) { throw new NullReferenceException(); }
                else
                {
                    return UserDebt.FromDebt(dbDeleteDebt);
                }
            }
        }
    }
}
