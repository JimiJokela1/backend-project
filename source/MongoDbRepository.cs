using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace backend_project
{
    public class MongoDbRepository : IRepository
    {
        private readonly IMongoCollection<Player> _playerCollection;
        private readonly IMongoCollection<Game> _gameCollection;

        public MongoDbRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = mongoClient.GetDatabase("game");
            _playerCollection = database.GetCollection<Player>("players");
            _gameCollection = database.GetCollection<Game>("games");
        }

        public async Task<Player> CreatePlayer(Player player)
        {
            await _playerCollection.InsertOneAsync(player);
            return player;
        }

        public async Task<Player> DeletePlayer(Guid id)
        {
            Player player = await GetPlayer(id);
            var filter = Builders<Player>.Filter.Eq("Id", player.Id);
            await _playerCollection.DeleteOneAsync(filter);

            return player;
        }

        public async Task<Player> GetPlayer(Guid id)
        {
            var filter = Builders<Player>.Filter.Eq("Id", id);
            return await _playerCollection.Find(filter).FirstAsync();
        }

        public async Task<Player[]> GetAllPlayers()
        {
            var filter = Builders<Player>.Filter.Empty;

            List<Player> players = await _playerCollection.Find(filter).ToListAsync();
            return players.ToArray();
        }

        public async Task<Player> ModifyPlayer(Guid id, ModifiedPlayer modiplayer)
        {
            Player player = await GetPlayer(id);
            player.Modify(modiplayer);

            var filter = Builders<Player>.Filter.Eq("Id", player.Id);
            await _playerCollection.ReplaceOneAsync(filter, player);
            return player;
        }

        public async Task<Game> GetGame(Guid id)
        {
            var filter = Builders<Game>.Filter.Eq("Id", id);
            return await _gameCollection.Find(filter).FirstAsync();
        }

        public async Task<Game[]> GetAllGames()
        {
            var filter = Builders<Game>.Filter.Empty;

            List<Game> games = await _gameCollection.Find(filter).ToListAsync();
            return games.ToArray();
        }

        public async Task<Game> CreateGame(Game game)
        {
            await _gameCollection.InsertOneAsync(game);

            // Apply mmr changes and possible new high score to players
            ModifiedPlayer player1mod = new ModifiedPlayer();
            player1mod.HighestScore = game.Player_1_Score;
            player1mod.Mmr =game.Player_1.Mmr + game.Player_1_Rank_Change;
            
            ModifiedPlayer player2mod = new ModifiedPlayer();
            player2mod.HighestScore = game.Player_2_Score;
            player2mod.Mmr =game.Player_2.Mmr + game.Player_2_Rank_Change;

            await ModifyPlayer(game.Player_1.Id, player1mod);
            await ModifyPlayer(game.Player_2.Id, player2mod);

            return game;
        }

        public async Task<Game> ModifyGame(Guid id, ModifiedGame modiGame)
        {
            Game game = await GetGame(id);
            game.Modify(modiGame);

            var filter = Builders<Game>.Filter.Eq("Id", game.Id);
            await _gameCollection.ReplaceOneAsync(filter, game);
            return game;
        }

        public async Task<Game> DeleteGame(Guid id)
        {
            Game game = await GetGame(id);
            var filter = Builders<Game>.Filter.Eq("Id", game.Id);
            await _gameCollection.DeleteOneAsync(filter);

            return game;
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
                // 20 14 30 10 40
                // 14
                for (int i = 1; i < allPlayers.Length; i++)
                {
                    if (allPlayers[i].Id == player.Id)
                        continue;
                    nextMmrDiff = Math.Abs(mmr - allPlayers[i].Mmr);
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

        public async Task<Player[]> GetTopTenByScore()
        {
            var result = await _playerCollection.Aggregate()
                        .SortByDescending(x => x.HighestScore)
                        .Limit(10)
                        .ToListAsync();

            return result.ToArray();
        }

        public async Task<Player[]> GetTopTenByRank()
        {
            var result = await _playerCollection.Aggregate()
                        .SortByDescending(x => x.Mmr)
                        .Limit(10)
                        .ToListAsync();

            return result.ToArray();
        }
    }
}