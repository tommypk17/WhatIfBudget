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
    public class DebtService : IDebtService
    {
        private readonly AppDbContext _ctx;
        public DebtService(AppDbContext ctx) {
            _ctx = ctx;
        }
        
        public IList<Debt> GetAllDebts()
        {
            return _ctx.Debts.ToList();
        }

        public Debt? AddNewDebt(Debt debt)
        {
            _ctx.Debts.Add(debt);
            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return _ctx.Debts.FirstOrDefault(x => x.Id == debt.Id);
        }
        public Debt? UpdateDebt(Debt debt)
        {
            _ctx.Debts.Update(debt);
            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return _ctx.Debts.FirstOrDefault(x => x.Id == debt.Id);
        }

        public Debt? DeleteDebt(int id)
        {
            var debt = _ctx.Debts.FirstOrDefault(x => x.Id == id);
            if (debt != null)
            {
                _ctx.Debts.Remove(debt);
            }
            try
            {
                _ctx.SaveChanges();
                return debt;
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }

        public IList<Debt> GetDebtsByDebtGoalId(int id)
        {
            var debts = _ctx.DebtGoalDebts
                        .Include(x => x.Debt)
                        .Where(x => x.DebtGoalId == id && x.Debt != null)
                        .Select(x => x.Debt).ToList();
            if (debts.Any())
            {
                return debts!;
            }
            return new List<Debt>();
        }
    }
}
