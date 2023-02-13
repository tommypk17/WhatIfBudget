using WhatIfBudget.Common.Enumerations;
using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Logic.Models
{
    public class UserIncome
    {
        public int Id { get; set; } 
        public string Name { get; set; } = String.Empty;
        public double Amount { get; set; }
        public EFrequency Frequency { get; set; }

        public Income ToIncome(Guid? userId = null)
        {
            return new Income()
            {
                Id = Id,
                Amount = Amount,
                Frequency = Frequency,
                UserId = userId != null ? userId.Value : Guid.Empty,
                Name = Name
            };
        }

        public static UserIncome FromIncome(Income income)
        {
            return new UserIncome()
            {
                Id = income.Id,
                Amount = income.Amount,
                Frequency = income.Frequency,
                Name = income.Name
            };
        }
    }
}
