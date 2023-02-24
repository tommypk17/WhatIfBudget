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
    }
}
