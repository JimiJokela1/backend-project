using System;
using System.Threading.Tasks;

namespace backend_project
{
    public class GameProcessor
    {
        private IRepository _repository;

        public GameProcessor(IRepository repository) {
            _repository = repository;
        }

        public Task<Game> Get(Guid id)
        {
            return _repository.GetGame(id);
        }

        public Task<Game[]> GetAll()
        {
            return _repository.GetAllGames();
        }

        public async Task<Game> Create(NewGame game)
        {
            Game newGame = new Game();
            newGame.Player_1 = await _repository.Get(game.Player_1_ID);
            newGame.Player_1_Score = game.Player_1_Score;
            
            newGame.Player_2 = await _repository.Get(game.Player_2_ID);
            newGame.Player_2_Score = game.Player_2_Score;

            newGame.Id = Guid.NewGuid();
            newGame.CreationTime = System.DateTime.Now;

            // TODO: calculate rank changes

            return await _repository.CreateGame(newGame);
        }

        public Task<Game> Modify(Guid id, ModifiedGame game)
        {
            return _repository.ModifyGame(id, game);
        }

        public Task<Game> Delete(Guid id)
        {
            return _repository.DeleteGame(id);
        }
        
    }
}