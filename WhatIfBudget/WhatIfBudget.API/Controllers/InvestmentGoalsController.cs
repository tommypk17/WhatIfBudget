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
    public class InvestmentGoalsController : ControllerBase
    {
        private readonly IInvestmentGoalLogic _investmentGoalLogic;
        public InvestmentGoalsController(IInvestmentGoalLogic investmentGoalLogic) {
            _investmentGoalLogic = investmentGoalLogic;
        }

        [HttpGet("{investmentGoalId}")]
        public IActionResult Get(int investmentGoalId)
        {
            //pass the ID from the route to the logic function
            var res = _investmentGoalLogic.GetInvestmentGoal(investmentGoalId);
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

        [HttpGet("{investmentGoalId}/totals")]
        public IActionResult GetInvestmentTotals(int investmentGoalId)
        {
            //pass the ID from the route to the logic function
            var res = _investmentGoalLogic.GetInvestmentTotals(investmentGoalId);
            return StatusCode(StatusCodes.Status200OK, res);
        }

        [HttpGet("{investmentGoalId}/BalanceOverTime")]
        public IActionResult GetBalanceOverTime(int investmentGoalId)
        {
            //pass the ID from the route to the logic function
            var res = _investmentGoalLogic.GetBalanceOverTime(investmentGoalId);
            return StatusCode(StatusCodes.Status200OK, res.ToList());
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
