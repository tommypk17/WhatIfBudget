using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhatIfBudget.Common.Models;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Services.Interfaces;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Logic.Models;
using Microsoft.AspNetCore.Authorization;

namespace WhatIfBudget.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentGoalController : ControllerBase
    {
        private readonly IInvestmentGoalLogic _investmentGoalLogic;
        public InvestmentGoalController(IInvestmentGoalLogic investmentGoalLogic) {
            _investmentGoalLogic = investmentGoalLogic;
        }

        //[HttpGet("{budgetId}")]
        //public IActionResult Get([FromRoute] int budgetId)
        //{
        //    //pass the ID from the route to the logic function
        //    var res = _investmentGoalLogic.GetBudgetInvestmentGoal(budgetId);
        //    //return a status of 200 with all the current user's income
        //    if (res == null)
        //    {
        //        return StatusCode(StatusCodes.Status400BadRequest, res);
        //    }
        //    else
        //    {
        //        return StatusCode(StatusCodes.Status200OK, res);
        //    }
        //}

        [HttpGet]
        public IActionResult Get(UserBudget apiBudget)
        {
            //pass the ID from the route to the logic function
            var res = _investmentGoalLogic.GetBudgetInvestmentGoal(apiBudget);
            //return a status of 200 with all the current user's income
            if (res == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, res);
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, res);
            }
        }


        [HttpPut]
        public IActionResult Put(UserInvestmentGoal apiInvestmentGoal)
        {
            var res = _investmentGoalLogic.ModifyUserInvestmentGoal(apiInvestmentGoal);
            if (res == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, res);
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, res);
            }
        }
    }
}
