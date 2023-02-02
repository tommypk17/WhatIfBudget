using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhatIfBudget.Common.Models;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Services.Interfaces;

namespace WhatIfBudget.API.Controllers
{
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
            //pass the ID from the auth token to the logic function
            var res = _incomeLogic.GetUserIncome(currentUser.Id);
            //return a status of 200 with all the current user's income
            return StatusCode(StatusCodes.Status200OK, res);
        }
    }
}
