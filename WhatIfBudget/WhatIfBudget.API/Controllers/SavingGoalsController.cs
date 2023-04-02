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
    public class SavingGoalsController : ControllerBase
    {
        private readonly ISavingGoalLogic _savingGoalLogic;
        public SavingGoalsController(ISavingGoalLogic savingGoalLogic) {
            _savingGoalLogic = savingGoalLogic;
        }

        [HttpGet("{savingGoalId}")]
        public IActionResult Get(int savingGoalId)
        {
            //pass the ID from the route to the logic function
            var res = _savingGoalLogic.GetSavingGoal(savingGoalId);
            //return a status of 200 with the requested saving goal
            if (res == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, res);
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, res);
            }
        }

        [HttpGet("{savingGoalId}/totals")]
        public IActionResult GetSavingTotals(int savingGoalId)
        {
            //pass the ID from the route to the logic function
            var res = _savingGoalLogic.GetSavingTotals(savingGoalId);
            return StatusCode(StatusCodes.Status200OK, res);
        }

        [HttpGet("{savingGoalId}/BalanceOverTime")]
        public IActionResult GetBalanceOverTime(int savingGoalId)
        {
            //pass the ID from the route to the logic function
            var res = _savingGoalLogic.GetBalanceOverTime(savingGoalId);
            return StatusCode(StatusCodes.Status200OK, res.ToList());
        }

        [HttpPut]
        public IActionResult Put(UserSavingGoal apiSavingGoal)
        {
            var res = _savingGoalLogic.ModifyUserSavingGoal(apiSavingGoal);
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
