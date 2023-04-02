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
    public class DebtGoalDebtService : IDebtGoalDebtService
    {
        private readonly AppDbContext _ctx;
        public DebtGoalDebtService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public IList<DebtGoalDebt> GetAllDebtGoalDebts()
        {
            return _ctx.DebtGoalDebts.ToList();
        }

        public DebtGoalDebt? AddNewDebtGoalDebt(DebtGoalDebt debtGoalDebt)
        {
            _ctx.DebtGoalDebts.Add(debtGoalDebt);
            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return _ctx.DebtGoalDebts.FirstOrDefault(x => x.Id == debtGoalDebt.Id);
        }

        public DebtGoalDebt? DeleteDebtGoalDebt(int id)
        {
            var DebtGoalDebt = _ctx.DebtGoalDebts.FirstOrDefault(x => x.Id == id);
            if (DebtGoalDebt != null)
            {
                _ctx.DebtGoalDebts.Remove(DebtGoalDebt);
            }
            try
            {
                _ctx.SaveChanges();
                return DebtGoalDebt;
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }
    }
}