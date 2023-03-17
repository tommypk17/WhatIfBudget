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
    public class MortgageGoalService : IMortgageGoalService
    {
        private readonly AppDbContext _ctx;
        public MortgageGoalService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public MortgageGoal? GetMortgageGoal(int id)
        {
            return _ctx.MortgageGoals.FirstOrDefault(x => x.Id == id);
        }

        public MortgageGoal? AddMortgageGoal(MortgageGoal mortgageGoal)
        {
            _ctx.MortgageGoals.Add(mortgageGoal);
            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return _ctx.MortgageGoals.FirstOrDefault(x => x.Id == mortgageGoal.Id);
        }

        public MortgageGoal? ModifyMortgageGoal(MortgageGoal mortgageGoal)
        {
            _ctx.MortgageGoals.Update(mortgageGoal);
            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return _ctx.MortgageGoals.FirstOrDefault(x => x.Id == mortgageGoal.Id);
        }

        public MortgageGoal? DeleteMortgageGoal(int id)
        {
            var MortgageGoal = _ctx.MortgageGoals.FirstOrDefault(x => x.Id == id);
            if (MortgageGoal != null)
            {
                _ctx.MortgageGoals.Remove(MortgageGoal);
            }
            try
            {
                _ctx.SaveChanges();
                return MortgageGoal;
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }

    }
}
