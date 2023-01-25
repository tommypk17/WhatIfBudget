using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatIfBudget.App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WhatIfBudget.App.Controllers
{
    [Route(".info")]
    public class ConfigurationController : Controller
    {
        private readonly IConfiguration _config;

        public ConfigurationController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet]
        public IActionResult Index()
        {
            AADInfo res = new AADInfo()
            {
                ClientId = _config.GetSection("AzureAd:ClientId").Get<string>(),
                Authority = _config.GetSection("AzureAd:Authority").Get<string>(),
                RedirectUri = _config.GetSection("AzureAd:RedirectUri").Get<string>(),
                KnownAuthorities = _config.GetSection("AzureAd:KnownAuthorities").Get<string>(),
            };
            return StatusCode(StatusCodes.Status200OK, res);
        }
    }
}