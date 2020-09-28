using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Blog.Api.Controllers
{
    [Route("env"), AllowAnonymous]
    public class EnvironmentTestController : BaseApiController
    {
        private readonly IConfiguration _configuration;

        public EnvironmentTestController(IConfiguration configuration)
            => _configuration = configuration; // change this to singleton options object

        [HttpGet]
        public IActionResult GetEnvironmentSettings()
            => Ok(new
            {
                Message = $"MySetting: {_configuration.GetValue<string>("MySetting")}",
                Db = $"Database: {_configuration.GetConnectionString("Blog")}"
            });
    }
}
