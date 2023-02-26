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
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseLogic _expenseLogic;
        public ExpensesController(IExpenseLogic expenseLogic) {
            _expenseLogic = expenseLogic;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //grab the user from the passed auth token
            var currentUser = AuthUser.Current(User);
            var res = _expenseLogic.GetUserExpenses(currentUser.Id);
            return StatusCode(StatusCodes.Status200OK, res);
        }

        [HttpGet("budgets/{budgetId}")]
        public IActionResult Get([FromRoute] int budgetId)
        {
            //pass the ID from the route to the logic function
            var res = _expenseLogic.GetBudgetExpenses(budgetId);
            return StatusCode(StatusCodes.Status200OK, res);
        }

        [HttpGet("budgets/{budgetId}/monthlyExpense")]
        public IActionResult GetMonthlyExpense([FromRoute] int budgetId)
        {
            //pass the ID from the route to the logic function
            var res = _expenseLogic.GetBudgetMonthlyExpense(budgetId);
            //return a status of 200 with all the current user's income
            return StatusCode(StatusCodes.Status200OK, res);
        }

        [HttpGet("budgets/{budgetId}/monthlyNeeds")]
        public IActionResult GetMonthlyNeeds([FromRoute] int budgetId)
        {
            //pass the ID from the route to the logic function
            var res = _expenseLogic.GetBudgetMonthlyNeed(budgetId);
            //return a status of 200 with all the current user's income
            return StatusCode(StatusCodes.Status200OK, res);
        }

        [HttpGet("budgets/{budgetId}/monthlyWants")]
        public IActionResult GetMonthlyWants([FromRoute] int budgetId)
        {
            //pass the ID from the route to the logic function
            var res = _expenseLogic.GetBudgetMonthlyWant(budgetId);
            //return a status of 200 with all the current user's income
            return StatusCode(StatusCodes.Status200OK, res);
        }

        [HttpPost]
        public IActionResult Post(UserExpense apiExpense)
        {
            //grab the user from the passed auth token
            var currentUser = AuthUser.Current(User);

            var res = _expenseLogic.AddUserExpense(currentUser.Id, apiExpense);
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
        public IActionResult Put(UserExpense apiExpense)
        {
            //grab the user from the passed auth token
            var currentUser = AuthUser.Current(User);

            var res = _expenseLogic.ModifyUserExpense(currentUser.Id, apiExpense);
            if (res == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, res);
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, res);
            }
        }

        [HttpDelete("{expenseId}/{budgetId}")]
        public IActionResult Delete([FromRoute] int expenseId, [FromRoute] int budgetId)
        {
            var res = _expenseLogic.DeleteBudgetExpense(expenseId, budgetId);
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
