using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hattrick.Dto;
using Hattrick.Manager.Interfaces;
using Hattrick.Model;
using Hattrick.Repository.Interfaces;
using Hattrick.Selenium;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Hattrick.Manager
{
    public interface IPlayerManager : IAsyncManager<PlayerDto>
    {
        Task ImportPlayers();
        Task<List<PlayerDto>> GetAll(int? skip = null, int? take = null);
    }


    public class PlayerManager : IPlayerManager
    {
        private readonly IConfiguration _configuration;
        private readonly IAsyncRepository<Player> _repository;

        public PlayerManager(IAsyncRepository<Player> repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task ImportPlayers()
        {
            var importer = new Importer(_configuration);
            var players = importer.Get();

            foreach (var player in players)
            {
                var aPlayerDto = await Add(player);
            }
        }

        public async Task<PlayerDto> Add(PlayerDto dto)
        {
            var player = (await _repository.Get(p => p.Name.Equals(dto.Name))).FirstOrDefault();
            if (player != null) return MapToDto(await _repository.Update(MapFromDto(dto, player)));

            return MapToDto(await _repository.Add(MapFromDto(dto)));
        }

        public Task<PlayerDto> Get(PlayerDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PlayerDto>> GetAll(int? skip = null, int? take = null)
        {
            List<PlayerDto> players = new List<PlayerDto>();
            var allPlayers = (await this._repository.GetAll(p => p.Include(pos => pos.Positions)));

            foreach (var player in allPlayers)
            {
                players.Add(this.MapToDto(player));
            }

            return players;
        }

        public Task Delete(PlayerDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<PlayerDto> Update(PlayerDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> Count()
        {
            throw new NotImplementedException();
        }

        private PlayerDto MapToDto(Player player)
        {
            var positions = new List<PositionDto>();
            foreach (var pos in player.Positions)
            {
                positions.Add(new PositionDto()
                {
                    Name = pos.Name,
                    Value = pos.Value
                });
            }
            return new PlayerDto()
            {
                Name = player.Name,
                Age = player.Age,
                Positions = positions
            };
        }


        private Player MapFromDto(PlayerDto dto, Player player = null)
        {
            if (player == null)
                return new Player
                {
                    Name = dto.Name,
                    Age = dto.Age,
                    Positions = MapPosFromDto(dto.Positions)
                };

            player.Name = dto.Name;
            player.Age = dto.Age;
            player.Positions = MapPosFromDto(dto.Positions);
            return player;
        }

        private List<Position> MapPosFromDto(List<PositionDto> dtoPositions)
        {
            var positions = new List<Position>();
            foreach (var dto in dtoPositions)
                positions.Add(new Position
                {
                    Name = dto.Name,
                    Value = dto.Value,
                    CreatedAt = DateTime.UtcNow
                });

            return positions;
        }
    }
}