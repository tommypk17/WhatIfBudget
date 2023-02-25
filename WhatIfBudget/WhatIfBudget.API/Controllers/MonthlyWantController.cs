﻿using Microsoft.AspNetCore.Http;
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
    public class MonthlyWantController : ControllerBase
    {
        private readonly IMonthlyWantLogic _monthlyWantLogic;
        public MonthlyWantController(IMonthlyWantLogic monthlyWantLogic) {
            _monthlyWantLogic = monthlyWantLogic;
        }

        [HttpGet("{budgetId}")]
        public IActionResult Get([FromRoute] int budgetId)
        {
            //pass the ID from the route to the logic function
            var res = _monthlyWantLogic.GetBudgetMonthlyWant(budgetId);
            //return a status of 200 with all the current user's income
            return StatusCode(StatusCodes.Status200OK, res);
        }
    }
}