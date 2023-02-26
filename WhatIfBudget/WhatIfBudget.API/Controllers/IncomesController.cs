using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhatIfBudget.Common.Models;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Services.Interfaces;
using WhatIfBudget.Data.Models;
using WhatIfBudget.Logic.Models;
using Microsoft.AspNetCore.Authorization;
using WhatIfBudget.Logic;

namespace WhatIfBudget.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IncomesController : ControllerBase
    {
        private readonly IIncomeLogic _incomeLogic;
        public IncomesController(IIncomeLogic incomeLogic) { 
            _incomeLogic = incomeLogic;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //grab the user from the passed auth token
            var currentUser = AuthUser.Current(User);
            var res = _incomeLogic.GetUserIncomes(currentUser.Id);
            //return a status of 200 with all the current user's income
            return StatusCode(StatusCodes.Status200OK, res);
        }

        [HttpGet("budgets/{budgetId}")]
        public IActionResult Get([FromRoute] int budgetId)
        {
            //pass the ID from the route to the logic function
            var res = _incomeLogic.GetBudgetIncomes(budgetId);
            //return a status of 200 with all the current user's income
            return StatusCode(StatusCodes.Status200OK, res);
        }

        [HttpGet("budgets/{budgetId}/monthlyIncome")]
        public IActionResult GetMonthlyIncome([FromRoute] int budgetId)
        {
            //pass the ID from the route to the logic function
            var res = _incomeLogic.GetBudgetMonthlyIncome(budgetId);
            //return a status of 200 with all the current user's income
            return StatusCode(StatusCodes.Status200OK, res);
        }

        [HttpPost]
        public IActionResult Post(UserIncome apiIncome)
        {
            //grab the user from the passed auth token
            var currentUser = AuthUser.Current(User);

            var res = _incomeLogic.AddUserIncome(currentUser.Id, apiIncome);
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
        public IActionResult Put(UserIncome apiIncome)
        {
            //grab the user from the passed auth token
            var currentUser = AuthUser.Current(User);

            var res = _incomeLogic.ModifyUserIncome(currentUser.Id, apiIncome);
            if (res == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, res);
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, res);
            }
        }

        [HttpDelete("{incomeId}/{budgetId}")]
        public IActionResult Delete([FromRoute] int incomeId, [FromRoute] int budgetId)
        {
            var res = _incomeLogic.DeleteBudgetIncome(incomeId, budgetId);
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
