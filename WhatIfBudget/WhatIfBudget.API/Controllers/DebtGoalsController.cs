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
    public class DebtGoalsController : ControllerBase
    {
        private readonly IDebtGoalLogic _debtGoalLogic;
        public DebtGoalsController(IDebtGoalLogic debtGoalLogic) {
            _debtGoalLogic = debtGoalLogic;
        }

        [HttpGet("{debtGoalId}")]
        public IActionResult Get(int debtGoalId)
        {
            //pass the ID from the route to the logic function
            var res = _debtGoalLogic.GetDebtGoal(debtGoalId);
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

    }
}
