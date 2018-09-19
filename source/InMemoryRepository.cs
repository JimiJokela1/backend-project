using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend_project
{
    public class InMemoryRepository : IRepository
    {
        private List<Player> players = new List<Player>();

        public async Task<Player> Create(Player player)
        {
            await Task.CompletedTask;
            players.Add(player);
            return player;
        }

        public async Task<Player> Delete(Guid id)
        {
            await Task.CompletedTask;

            Player found = GetPlayerById(id);

            if (found != null)
            {
                players.Remove(found);
                return found;
            }
            else
            {
                return null;
            }
        }

        public async Task<Player> Get(Guid id)
        {
            await Task.CompletedTask;
            return GetPlayerById(id);
        }

        public async Task<Player[]> GetAll()
        {
            await Task.CompletedTask;
            return players.ToArray();
        }

        public async Task<Player> Modify(Guid id, ModifiedPlayer player)
        {
            await Task.CompletedTask;
            Player found = GetPlayerById(id);
            if (found != null)
            {
                found.Modify(player);
            }
            return found;
        }

        private Player GetPlayerById(Guid id)
        {
            foreach (Player player in players)
            {
                if (player.Id == id)
                {
                    return player;
                }
            }

            return null;
        }

        public Task<Game[]> GetAllGames()
        {
            throw new NotImplementedException();
        }

        public Task<Game> GetGame(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Game> CreateGame(Game game)
        {
            throw new NotImplementedException();
        }

        public Task<Game> ModifyGame(Guid id, ModifiedGame game)
        {
            throw new NotImplementedException();
        }

        public Task<Game> DeleteGame(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
