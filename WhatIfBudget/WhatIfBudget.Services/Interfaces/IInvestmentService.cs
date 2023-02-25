using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Services.Interfaces
{
    public interface IInvestmentService
    {
        public IList<Investment> GetAllInvestments();
        public Investment? AddNewInvestment(Investment newInvestment);
        public Investment? UpdateInvestment(Investment modifiedInvestment);
        public Investment? DeleteInvestment(int id);


    }
}
