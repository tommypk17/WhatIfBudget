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
    public class BudgetsController : ControllerBase
    {
        private readonly IBudgetLogic _budgetLogic;
        public BudgetsController(IBudgetLogic budgetLogic) { 
            _budgetLogic = budgetLogic;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //grab the user from the passed auth token
            var currentUser = AuthUser.Current(User);
            //pass the ID from the auth token to the logic function
            var res = _budgetLogic.GetUserBudgets(currentUser.Id);
            //return a status of 200 with all the current user's budget
            return StatusCode(StatusCodes.Status200OK, res);
        }

        [HttpGet("{budgetId}")]
        public IActionResult Get([FromRoute] int budgetId)
        {
            //pass the budget ID from the route to the logic function
            var res = _budgetLogic.GetBudget(budgetId);
            //return a status of 200 with all the current user's budget
            return StatusCode(StatusCodes.Status200OK, res);
        }

        [HttpGet("{budgetId}/netAvailable")]
        public IActionResult GetAvailableMonthlyNet([FromRoute] int budgetId)
        {
            //pass the budget ID from the route to the logic function
            var res = _budgetLogic.GetAvailableMonthlyNet(budgetId);
            //return a status of 200 with all the current user's budget
            return StatusCode(StatusCodes.Status200OK, res);
        }

        [HttpGet("{budgetId}/additionalContributions")]
        public IActionResult GetAdditionalContributions([FromRoute] int budgetId)
        {
            var res = _budgetLogic.GetUserBudgetAllocations(budgetId);
            if (res == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, res);
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, res);
            }
        }

        [HttpPut("{budgetId}/additionalContributions")]
        public IActionResult UpdateAdditionalContributions([FromRoute] int budgetId, [FromBody] UserBudgetAllocations budgetAllocations)
        {
            var res = _budgetLogic.UpdateUserBudgetAllocations(budgetId, budgetAllocations);
            if (res == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, res);
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, res);
            }
        }

        [HttpGet("{budgetId}/availableFreeCash")]
        public IActionResult GetAvailableFreeCash([FromRoute] int budgetId)
        {
            var res = _budgetLogic.GetBudgetAvailableFreeCash(budgetId);
            return StatusCode(StatusCodes.Status200OK, res);
        }

        [HttpPost]
        public IActionResult Post(UserBudget apiBudget)
        {
            //grab the user from the passed auth token
            var currentUser = AuthUser.Current(User);

            var res = _budgetLogic.CreateUserBudget(currentUser.Id, apiBudget);
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
        public IActionResult Put(UserBudget apiBudget)
        {
            //grab the user from the passed auth token
            var currentUser = AuthUser.Current(User);

            var res = _budgetLogic.ModifyUserBudget(currentUser.Id, apiBudget);
            if (res == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, res);
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, res);
            }
        }

        [HttpDelete]
        public IActionResult Delete(UserBudget apiBudget)
        {
            //grab the user from the passed auth token
            var currentUser = AuthUser.Current(User);

            var res = _budgetLogic.DeleteUserBudget(apiBudget);
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
