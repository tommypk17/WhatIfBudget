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
    public class SavingGoalService : ISavingGoalService
    {
        private readonly AppDbContext _ctx;
        public SavingGoalService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public SavingGoal? GetSavingGoal(int id)
        {
            return _ctx.SavingGoals.FirstOrDefault(x => x.Id == id);
        }

        public SavingGoal? AddSavingGoal(SavingGoal savingGoal)
        {
            _ctx.SavingGoals.Add(savingGoal);
            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return _ctx.SavingGoals.FirstOrDefault(x => x.Id == savingGoal.Id);
        }

        public SavingGoal? ModifySavingGoal(SavingGoal savingGoal)
        {
            _ctx.SavingGoals.Update(savingGoal);
            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return _ctx.SavingGoals.FirstOrDefault(x => x.Id == savingGoal.Id);
        }

        public SavingGoal? DeleteSavingGoal(int id)
        {
            var SavingGoal = _ctx.SavingGoals.FirstOrDefault(x => x.Id == id);
            if (SavingGoal != null)
            {
                _ctx.SavingGoals.Remove(SavingGoal);
            }
            try
            {
                _ctx.SaveChanges();
                return SavingGoal;
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }
    }
}
