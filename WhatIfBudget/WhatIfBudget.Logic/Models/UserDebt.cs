using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Logic.Models
{
    public class UserDebt
    {
        public int Id { get; set; }
        public int GoalId { get; set; } = 0;
        public string Name { get; set; } = String.Empty;
        public double CurrentBalance { get; set; } = 0;
        public float InterestRate { get; set; } = 0;
        public double MinimumPayment { get; set; } = 0;

        public Debt ToDebt(Guid? userId = null)
        {
            return new Debt()
            {
                Id = Id,
                UserId = userId != null ? userId.Value : Guid.Empty,
                Name = Name,
                CurrentBalance= CurrentBalance,
                InterestRate = InterestRate,
                MinimumPayment = MinimumPayment,
            };
        }

        public static UserDebt FromDebt(Debt debt, int goalId = 0)
        {
            return new UserDebt()
            {
                Id = debt.Id,
                Name = debt.Name,
                GoalId = goalId,
                InterestRate= debt.InterestRate,
                MinimumPayment = debt.MinimumPayment,
                CurrentBalance = debt.CurrentBalance
            };
        }
    }
}
