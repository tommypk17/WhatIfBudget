using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Logic.Models
{
    public class UserInvestment
    {
        public int Id { get; set; }
        public int GoalId { get; set; } = 0;
        public string Name { get; set; } = String.Empty;
        public double CurrentBalance { get; set; } = 0;
        public double MonthlyPersonalContribution { get; set; } = 0;
        public double MonthlyEmployerContribution { get; set; } = 0;

        public Investment ToInvestment(Guid userId)
        {
            return new Investment()
            {
                Id = Id,
                Name = Name,
                UserId = userId,
                CurrentBalance = CurrentBalance,
                MonthlyPersonalContribution = MonthlyPersonalContribution,
                MonthlyEmployerContribution = MonthlyEmployerContribution
            };
        }

        public static UserInvestment FromInvestment(Investment investment)
        {
            return new UserInvestment()
            {
                Id = investment.Id,
                Name = investment.Name,
                CurrentBalance = investment.CurrentBalance,
                MonthlyPersonalContribution = investment.MonthlyPersonalContribution,
                MonthlyEmployerContribution = investment.MonthlyEmployerContribution
            };
        }
    }
}
