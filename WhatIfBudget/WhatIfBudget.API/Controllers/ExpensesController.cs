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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //grab the user from the passed auth token
            var currentUser = AuthUser.Current(User);

            var res = _expenseLogic.DeleteUserExpense(currentUser.Id, id);
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
