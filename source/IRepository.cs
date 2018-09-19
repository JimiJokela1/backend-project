using System;
using System.Threading.Tasks;

namespace backend_project
{
    public interface IRepository
    {
        Task<Player> Get(Guid id);
        Task<Player[]> GetAll();
        Task<Player> Create(Player player);
        Task<Player> Modify(Guid id, ModifiedPlayer player);
        Task<Player> Delete(Guid id);
        
        Task<Game> GetGame(Guid id);
        Task<Game[]> GetAllGames();
        Task<Game> CreateGame(Game game);
        Task<Game> ModifyGame(Guid id, ModifiedGame game);
        Task<Game> DeleteGame(Guid id);
    }
}
