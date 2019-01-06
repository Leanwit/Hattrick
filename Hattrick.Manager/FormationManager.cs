using System.Collections.Generic;
using System.Linq;
using Hattrick.Dto;
using Hattrick.Manager.Helpers;
using Hattrick.Manager.Interfaces;
using Hattrick.Manager.Model;
using Hattrick.Model;
using Hattrick.Repository.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Hattrick.Manager
{
    public class FormationManager : IGeneticManager<PlayerDto>
    {
        private readonly IConfiguration _configuration;
        private readonly IAsyncRepository<Player> _repository;

        public FormationManager(IAsyncRepository<Player> repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public FormationModel GetBestFormation(List<PlayerDto> players)
        {
            var formation = new FormationModel();
            formation.Arquero = this.CreatePlayerInPosition(formation, new List<string>() { "Arquero" }, players);
            formation.Defensa.Add(this.CreatePlayerInPosition(formation, PositionModel.GetDefenseValues(), players));
            formation.Defensa.Add(this.CreatePlayerInPosition(formation, PositionModel.GetDefenseValues(), players));
            formation.UpdateTeamValue();
            return formation;
        }

        internal PlayerInPositionModel CreatePlayerInPosition(FormationModel formation, List<string> positionsName, List<PlayerDto> players)
        {
            PlayerDto playerDto;
            do
            {
                playerDto = players.PickRandom();
            } while (!IsNotCreatedYet(playerDto, formation));

            return new PlayerInPositionModel(playerDto.Name, GetPositionByName(playerDto, positionsName));
        }

        static internal PositionDto GetPositionByName(PlayerDto playerDto, List<string> positionsName)
        {
            //return playerDto.Positions.FirstOrDefault(p => p.Name.Equals(positionsName.Select(x => x)));
            return playerDto.Positions.FirstOrDefault(p => positionsName.Any(pos => pos.Equals(p.Name)));

        }

        private bool IsNotCreatedYet(PlayerDto player, FormationModel formation)
        {
            if (formation.Arquero?.Name == player.Name)
            {
                return false;
            }

            if (formation.Defensa.Exists(d => d.Name.Equals(player.Name)))
            {
                return false;
            }
            if (formation.Mediocampista.Exists(d => d.Name.Equals(player.Name)))
            {
                return false;
            }
            if (formation.Lateral.Exists(d => d.Name.Equals(player.Name)))
            {
                return false;
            }

            if (formation.Delanteros.Exists(d => d.Name.Equals(player.Name)))
            {
                return false;
            }

            return true;
        }



    }
}