using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhatIfBudget.Common.Models;
using WhatIfBudget.Logic.Interfaces;

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
    }
}
