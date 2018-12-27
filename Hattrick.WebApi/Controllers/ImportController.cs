using System.Collections.Generic;
using Hattrick.Manager;
using Hattrick.Manager.Interfaces;
using Hattrick.Selenium;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Hattrick.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private IPlayerManager _manager;
        private readonly IConfiguration _configuration;

        public ImportController(IConfiguration configuration, IPlayerManager manager)
        {
            this._configuration = configuration;
            this._manager = manager;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            this._manager.ImportPlayers().Wait();

            return new string[] {"ok"};
        }
    }
}