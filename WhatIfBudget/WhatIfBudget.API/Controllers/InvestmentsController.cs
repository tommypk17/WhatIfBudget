using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhatIfBudget.Common.Models;
using WhatIfBudget.Logic;
using WhatIfBudget.Logic.Interfaces;
using WhatIfBudget.Logic.Models;

namespace WhatIfBudget.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentsController : ControllerBase
    {
        private readonly IInvestmentLogic _investmentLogic;
        public InvestmentsController(IInvestmentLogic investmentLogic)
        {
            _investmentLogic = investmentLogic;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //grab the user from the passed auth token
            var currentUser = AuthUser.Current(User);
            //pass the ID from the auth token to the logic function
            var res = _investmentLogic.GetUserInvestments(currentUser.Id);
            //return a status of 200 with all the current user's budget
            return StatusCode(StatusCodes.Status200OK, res);
        }

        [HttpPost]
        public IActionResult Post(UserInvestment investment)
        {
            //grab the user from the passed auth token
            var currentUser = AuthUser.Current(User);
            //pass the ID from the auth token to the logic function
            var res = _investmentLogic.AddUserInvestment(currentUser.Id, investment);
            //return a status of 200 with all the current user's budget
            return StatusCode(StatusCodes.Status200OK, res);
        }
    }
}
