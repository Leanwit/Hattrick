using System.Collections.Generic;
using System.Threading.Tasks;
using Hattrick.Dto;

namespace Hattrick.Manager.Interfaces
{
    public interface IAsyncManager<TEntity>
        where TEntity : class
    {
        Task<PlayerDto> Add(TEntity entity);

        Task<TEntity> Get(TEntity entity);

        Task<List<PlayerDto>> GetAll(int? skip = null, int? take = null);

        Task Delete(TEntity entity);

        Task<TEntity> Update(TEntity entity);

        Task<int> Count();
    }

}