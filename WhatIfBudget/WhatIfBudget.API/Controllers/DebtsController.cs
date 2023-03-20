using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhatIfBudget.Common.Models;
using WhatIfBudget.Logic;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Logic.Models;

namespace WhatIfBudget.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebtsController : ControllerBase
    {
        private readonly IDebtLogic _debtLogic;
        public DebtsController(IDebtLogic debtLogic)
        {
            _debtLogic = debtLogic;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //grab the user from the passed auth token
            var currentUser = AuthUser.Current(User);
            var res = _debtLogic.GetUserDebts(currentUser.Id);
            return StatusCode(StatusCodes.Status200OK, res);
        }

        [HttpGet("goals/{goalId}")]
        public IActionResult GetDebtsByGoalId(int goalId)
        {
            //grab the user from the passed auth token
            var currentUser = AuthUser.Current(User);
            //pass the ID from the auth token to the logic function
            var res = _debtLogic.GetUserDebtsByGoalId(currentUser.Id, goalId);
            //return a status of 200 with all the current user's budget
            return StatusCode(StatusCodes.Status200OK, res);
        }

        [HttpPost]
        public IActionResult Post(UserDebt debt)
        {
            //grab the user from the passed auth token
            var currentUser = AuthUser.Current(User);
            //pass the ID from the auth token to the logic function
            var res = _debtLogic.AddUserDebt(currentUser.Id, debt);
            //return a status of 200 with all the current user's budget
            return StatusCode(StatusCodes.Status200OK, res);
        }

        [HttpPut]
        public IActionResult Put(UserDebt apiDebt)
        {
            //grab the user from the passed auth token
            var currentUser = AuthUser.Current(User);

            var res = _debtLogic.ModifyUserDebt(currentUser.Id, apiDebt);
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
