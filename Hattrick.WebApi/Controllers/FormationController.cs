using System.Collections.Generic;
using Hattrick.Dto;
using Hattrick.Manager;
using Microsoft.AspNetCore.Mvc;

namespace Hattrick.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormationController : ControllerBase
    {
        private IPlayerManager _manager;

        public FormationController(IPlayerManager manager)
        {
            this._manager = manager;
        }
        
        // GET
        [HttpGet]
        public List<PlayerDto> GetBestFormation()
        {
            return this._manager.GetAll().Result;
        }
    }
}