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

        [HttpGet]
        public IActionResult Get(int budgetId)
        {
            //pass the ID from the auth token to the logic function
            var res = _investmentGoalLogic.GetBudgetInvestmentGoal(budgetId);
            //return a status of 200 with all the current user's income
            return StatusCode(StatusCodes.Status200OK, res);
        }

        // Can we delete this POST API and instead just creat a new investment goal when a new budget is created?
        /*
        [HttpPost]
        public IActionResult Post(UserInvestmentGoal apiInvestmentGoal)
        {
            //grab the user from the passed auth token
            var currentUser = AuthUser.Current(User);

            var res = _investmentGoalLogic.AddUserInvestmentGoal(currentUser.Id, apiInvestmentGoal);
            if (res == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, res);
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, res);
            }
        }
        */
        [HttpPut]
        public IActionResult Put(UserInvestmentGoal apiInvestmentGoal)
        {
            //grab the user from the passed auth token
            var currentUser = AuthUser.Current(User);

            var res = _investmentGoalLogic.ModifyUserInvestmentGoal(currentUser.Id, apiInvestmentGoal);
            if (res == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, res);
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, res);
            }
        }
        /* SAME
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            //grab the user from the passed auth token
            var currentUser = AuthUser.Current(User);

            var res = _incomeLogic.DeleteUserIncome(currentUser.Id, id);
            if (res == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, res);
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, res);
            }
        }*/
    }
}
