using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Data.DAL;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Services.Interfaces;

namespace WhatIfBudget.Services
{
    public class DebtGoalService : IDebtGoalService
    {
        private readonly AppDbContext _ctx;
        public DebtGoalService(AppDbContext ctx) {
            _ctx = ctx;
        }
        public DebtGoal? GetDebtGoal(int id)
        {
            return _ctx.DebtGoals.FirstOrDefault(x => x.Id == id);
        }
        public DebtGoal? AddDebtGoal(DebtGoal debtGoal)
        {
            _ctx.DebtGoals.Add(debtGoal);
            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return _ctx.DebtGoals.FirstOrDefault(x => x.Id == debtGoal.Id);
        }
        public DebtGoal? UpdateDebtGoal(DebtGoal debtGoal)
        {
            _ctx.DebtGoals.Update(debtGoal);
            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return _ctx.DebtGoals.FirstOrDefault(x => x.Id == debtGoal.Id);
        }
    }
}
