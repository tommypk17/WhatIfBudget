using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            return StatusCode(StatusCodes.Status200OK, _incomeLogic.GetUserIncome());
        }
    }
}
