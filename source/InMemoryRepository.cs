using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend_project
{
    public class InMemoryRepository : IRepository
    {
        private List<Player> players = new List<Player>();
        private List<Game> games = new List<Game>();

        public async Task<Player> CreatePlayer(Player player)
        {
            await Task.CompletedTask;
            players.Add(player);
            return player;
        }

        public async Task<Player> DeletePlayer(Guid id)
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

        public async Task<Player> GetPlayer(Guid id)
        {
            await Task.CompletedTask;
            return GetPlayerById(id);
        }

        public async Task<Player[]> GetAllPlayers()
        {
            await Task.CompletedTask;
            return players.ToArray();
        }

        public async Task<Player> ModifyPlayer(Guid id, ModifiedPlayer player)
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

        private Game GetGameById(Guid id)
        {
            foreach (Game game in games)
            {
                if (game.Id == id)
                {
                    return game;
                }
            }

            return null;
        }

        public async Task<Game[]> GetAllGames()
        {
            await Task.CompletedTask;
            return games.ToArray();
        }

        public async Task<Game> GetGame(Guid id)
        {
            await Task.CompletedTask;
            return GetGameById(id);
        }

        public async Task<Game> CreateGame(Game game)
        {
            await Task.CompletedTask;
            games.Add(game);
            return game;
        }

        public async Task<Game> ModifyGame(Guid id, ModifiedGame game)
        {
            await Task.CompletedTask;
            Game found = GetGameById(id);
            if (found != null)
            {
                found.Modify(game);
            }
            return found;
        }

        public async Task<Game> DeleteGame(Guid id)
        {
            await Task.CompletedTask;

            Game found = GetGameById(id);

            if (found != null)
            {
                games.Remove(found);
                return found;
            }
            else
            {
                return null;
            }
        }

        public async Task<Player> GetNextOpponent(Guid playerId)
        {
            Player player = await GetPlayer(playerId);

            Player[] allPlayers = await GetAllPlayers();

            if (allPlayers.Length > 1)
            {
                int mmr = player.Mmr;
                Player closest = allPlayers[0];
                if (closest.Id == player.Id)
                    closest = allPlayers[1];
                int closestMmrDiff = Math.Abs(closest.Mmr - mmr);
                int nextMmrDiff;

                for (int i = 0; i < allPlayers.Length; i++)
                {
                    if (allPlayers[i].Id == player.Id)
                        continue;
                    nextMmrDiff = Math.Abs(mmr - closest.Mmr);
                    if (closestMmrDiff > nextMmrDiff)
                    {
                        closest = allPlayers[i];
                        closestMmrDiff = nextMmrDiff;
                    }
                }
                return closest;
            }
            return null;
        }

        public Task<Player[]> GetTopTenByScore()
        {
            throw new NotImplementedException();
        }

        public Task<Player[]> GetTopTenByRank()
        {
            throw new NotImplementedException();
        }

        public Task<Player> GetPlayerByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
