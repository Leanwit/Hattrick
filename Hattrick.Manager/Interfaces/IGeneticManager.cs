using Hattrick.Dto;
using Hattrick.Manager.Model;
using System.Collections.Generic;

namespace Hattrick.Manager.Interfaces
{
    public interface IGeneticManager<TEntity>
        where TEntity : class
    {
        FormationModel GetBestFormation(List<PlayerDto> players);
        FormationModel GetBestFormationRound(List<PlayerDto> players);
    }

}