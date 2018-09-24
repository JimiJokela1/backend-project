using System;
using System.Threading.Tasks;

namespace backend_project
{
    public interface IRepository
    {
        Task<Player> GetPlayer(Guid id);
        Task<Player[]> GetAllPlayers();
        Task<Player> CreatePlayer(Player player);
        Task<Player> ModifyPlayer(Guid id, ModifiedPlayer player);
        Task<Player> DeletePlayer(Guid id);
        
        Task<Game> GetGame(Guid id);
        Task<Game[]> GetAllGames();
        Task<Game> CreateGame(Game game);
        Task<Game> ModifyGame(Guid id, ModifiedGame game);
        Task<Game> DeleteGame(Guid id);
    }
}
