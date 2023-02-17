using System.Xml.Linq;
using WhatIfBudget.Common.Enumerations;
using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Logic.Models
{
    public class UserInvestmentGoal
    {
        public int Id { get; set; }
        public double TotalBalance { get; set; } = 0;
        public double AnnualReturnRate_Percent { get; set; } = 0;
        public UInt16 YearsToMaturation { get; set; } = 30;
        public double AnnualRaiseFactor_Percent { get; set; } = 0;
        public double additionalPersonalAllocation { get; set; } = 0;

        public InvestmentGoal ToInvestmentGoal(Guid? userId = null)
        {
            return new InvestmentGoal()
            {
                Id = Id,
                UserId = userId != null ? userId.Value : Guid.Empty,
                TotalBalance = TotalBalance,
                AnnualReturnRate_Percent = AnnualReturnRate_Percent,
                YearsToMaturation = YearsToMaturation,
                AnnualRaiseFactor_Percent = AnnualRaiseFactor_Percent,
                additionalPersonalAllocation = additionalPersonalAllocation
            };
        }

        public static UserInvestmentGoal FromInvestmentGoal(InvestmentGoal investmentGoal)
        {
            return new UserInvestmentGoal()
            {
                Id = investmentGoal.Id,
                TotalBalance = investmentGoal.TotalBalance,
                AnnualReturnRate_Percent = investmentGoal.AnnualReturnRate_Percent,
                YearsToMaturation = investmentGoal.YearsToMaturation,
                AnnualRaiseFactor_Percent = investmentGoal.AnnualRaiseFactor_Percent,
                additionalPersonalAllocation = investmentGoal.additionalPersonalAllocation
            };
        }
    }
}
