using System;
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
            return CreateBestFormation(players);
        }

        public FormationModel GetBestFormationRound(List<PlayerDto> players)
        {
            return CreateBestFormation(players, "not simple");
        }

        internal FormationModel CreateBestFormation(List<PlayerDto> players, string method = "Simple")
        {
            var formation = new FormationModel();

            var tactics = new List<string>() {
                PositionModel.Arquero,
                PositionModel.DefensaCentral,
                PositionModel.DefensaLateral,
                PositionModel.DefensaLateral,
                PositionModel.Mediocampista,
                PositionModel.Mediocampista,
                PositionModel.Mediocampista,
                PositionModel.Lateralhaciamedio,
                PositionModel.Delantero,
                PositionModel.DelanteroDefensivo,
                PositionModel.DelanteroDefensivo
            };

            if (method.Equals("Simple"))
            {
                foreach (var tactic in tactics)
                {
                    formation.AddPlayer(this.CreateBestPlayerInPosition(formation, tactic, players));
                }
                return formation;
            }

            return CreateBestFormationRound(tactics, players);

        }

        private FormationModel CreateBestFormationRound(List<string> tactics, List<PlayerDto> players)
        {
            var bestFormation = new FormationModel();

            for (int i = 0; i < tactics.Count; i++)
            {
                var formation = new FormationModel();
                foreach (var tactic in tactics)
                {
                    formation.AddPlayer(this.CreateBestPlayerInPosition(formation, tactic, players));
                }

                if (formation.TeamValue > bestFormation.TeamValue)
                {
                    bestFormation = (FormationModel)formation.Clone();
                }
                string aux = tactics.First();
                tactics.Remove(aux);
                tactics.Add(aux);
            }
            return bestFormation;
        }

        internal PlayerInPositionModel CreatePlayerInPositionRandom(FormationModel formation, List<string> positionsName, List<PlayerDto> players)
        {
            PlayerDto playerDto;
            do
            {
                playerDto = players.PickRandom();
            } while (!IsNotCreatedYet(playerDto, formation));

            return new PlayerInPositionModel(playerDto.Name, GetPositionByName(playerDto, positionsName));
        }

        internal PlayerInPositionModel CreatePlayerInPositionRandom(FormationModel formation, string positionName, List<PlayerDto> players)
        {
            return this.CreatePlayerInPositionRandom(formation, new List<string> { positionName }, players);
        }


        internal PlayerInPositionModel CreateBestPlayerInPosition(FormationModel formation, string positionName, List<PlayerDto> players)
        {
            PlayerDto playerDto;

            Dictionary<PlayerDto, double> values = new Dictionary<PlayerDto, double>();

            foreach (var player in players)
            {
                values.Add(player, player.Positions.FirstOrDefault(p => p.Name.Equals(positionName)).Value);
            }

            do
            {
                playerDto = values.OrderByDescending(x => x.Value).FirstOrDefault().Key;
                values.Remove(playerDto);
            } while (!IsNotCreatedYet(playerDto, formation));

            return new PlayerInPositionModel(playerDto.Name, GetPositionByName(playerDto, new List<string> { positionName }));
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

            if (formation.Defense.Exists(d => d.Name.Equals(player.Name)))
            {
                return false;
            }
            if (formation.Middfield.Exists(d => d.Name.Equals(player.Name)))
            {
                return false;
            }
            if (formation.Side.Exists(d => d.Name.Equals(player.Name)))
            {
                return false;
            }

            if (formation.Forward.Exists(d => d.Name.Equals(player.Name)))
            {
                return false;
            }

            return true;
        }
    }
}