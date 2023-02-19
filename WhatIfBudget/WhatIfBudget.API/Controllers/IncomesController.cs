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
    public class IncomesController : ControllerBase
    {
        private readonly IIncomeLogic _incomeLogic;
        public IncomesController(IIncomeLogic incomeLogic) { 
            _incomeLogic = incomeLogic;
        }

        [HttpGet]
        public IActionResult Get(int budgetId)
        {
            //pass the ID from the auth token to the logic function
            var res = _incomeLogic.GetBudgetIncomes(budgetId);
            //return a status of 200 with all the current user's income
            return StatusCode(StatusCodes.Status200OK, res);
        }

        [HttpPost]
        public IActionResult Post(UserIncome apiIncome, int budgetId)
        {
            //grab the user from the passed auth token
            var currentUser = AuthUser.Current(User);

            var res = _incomeLogic.AddUserIncome(currentUser.Id, apiIncome, budgetId);
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

        [HttpDelete("{id}")]
        public IActionResult Delete(int incomeId, int budgetId)
        {
            //grab the user from the passed auth token
            var currentUser = AuthUser.Current(User);

            var res = _incomeLogic.DeleteUserIncome(currentUser.Id, incomeId, budgetId);
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
