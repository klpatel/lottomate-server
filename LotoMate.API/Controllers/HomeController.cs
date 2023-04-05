using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LotoMate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Route("[controller]")]
    [ApiExplorerSettings(GroupName = "LotoMate.API")]

    public class HomeController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<HomeController> logger;
                
        public HomeController(IConfiguration configuration, ILogger<HomeController> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }
                
        [HttpGet]
        public IEnumerable<LotoMateInfo> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new LotoMateInfo
            {
                Date = DateTime.Now.AddDays(index),
                Message = "Welcome to LotoMate 2.0"
            })
            .ToArray();
        }

    }
}