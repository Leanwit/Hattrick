using System.Collections.Generic;
using Hattrick.Dto;
using Hattrick.Manager;
using Hattrick.Manager.Interfaces;
using Hattrick.Manager.Model;
using Microsoft.AspNetCore.Mvc;

namespace Hattrick.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormationController : ControllerBase
    {
        private IPlayerManager _playerManager;
        private IFormationManager<PlayerDto> _manager;

        public FormationController(IPlayerManager playerManager, IFormationManager<PlayerDto> manager)
        {
            this._playerManager = playerManager;
            this._manager = manager;
        }

        // GET
        [HttpGet("Simple/BestFormation")]
        public FormationModel GetBestFormation()
        {
            var playersDto = this._playerManager.GetAll().Result;
            return this._manager.GetBestFormation(playersDto);  
        }

        // GET
        [HttpGet("Simple/BestFormationRound")]
        public FormationModel GetBestFormationRound()
        {
            var playersDto = this._playerManager.GetAll().Result;
            return this._manager.GetBestFormationRound(playersDto);
        }
        
        // GET
        [HttpGet("Simple/GetFormationRandom")]
        public FormationModel GetFormationRandom()
        {
            var playersDto = this._playerManager.GetAll().Result;
            return this._manager.GetormationRandom(playersDto);
        }
    }
}