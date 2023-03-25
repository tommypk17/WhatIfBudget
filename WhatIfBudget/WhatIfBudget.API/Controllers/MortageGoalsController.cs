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
    public class MortgageGoalsController : ControllerBase
    {
        private readonly IMortgageGoalLogic _MortgageGoalLogic;
        public MortgageGoalsController(IMortgageGoalLogic MortgageGoalLogic) {
            _MortgageGoalLogic = MortgageGoalLogic;
        }

        [HttpGet("{MortgageGoalId}")]
        public IActionResult Get(int MortgageGoalId)
        {
            //pass the ID from the route to the logic function
            var res = _MortgageGoalLogic.GetMortgageGoal(MortgageGoalId);
            //return a status of 200 with the requested Mortgage goal
            if (res == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, res);
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, res);
            }
        }

        [HttpGet("{MortgageGoalId}/totals")]
        public IActionResult GetMortgageTotals(int MortgageGoalId)
        {
            //pass the ID from the route to the logic function
            var res = _MortgageGoalLogic.GetMortgageTotals(MortgageGoalId);
            return StatusCode(StatusCodes.Status200OK, res);
        }

        [HttpGet("{MortgageGoalId}/NetValueOverTime")]
        public IActionResult GetNetValueOverTime(int MortgageGoalId)
        {
            //pass the ID from the route to the logic function
            var res = _MortgageGoalLogic.GetNetValueOverTime(MortgageGoalId);
            return StatusCode(StatusCodes.Status200OK, res.ToList());
        }

        [HttpGet("{MortgageGoalId}/Amortization")]
        public IActionResult GetAmortizationTable(int MortgageGoalId)
        {
            //pass the ID from the route to the logic function
            var res = _MortgageGoalLogic.GetAmortizationTable(MortgageGoalId);
            return StatusCode(StatusCodes.Status200OK, res.ToList());
        }

        [HttpPut]
        public IActionResult Put(UserMortgageGoal apiMortgageGoal)
        {
            var res = _MortgageGoalLogic.ModifyUserMortgageGoal(apiMortgageGoal);
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
