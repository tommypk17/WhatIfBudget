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
    }
}
