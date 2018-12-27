using Hattrick.Model;
using Hattrick.Repository.Impl;
using Hattrick.Repository.Interfaces;

namespace Hattrick.Repository
{
    public class PlayerRepository : AsyncBaseRepository<Player>
    {
        public PlayerRepository(IUnitOfWork unitOfWorkInterface)
        {
            this.UnitOfWork = unitOfWorkInterface;
            this.Set = this.UnitOfWork.Context.Player;
        }
    }

}