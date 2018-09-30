using System;
using System.Threading.Tasks;

namespace backend_project
{
    public class GameProcessor
    {
        private IRepository _repository;

        public GameProcessor(IRepository repository)
        {
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
            newGame.Player_1 = await _repository.GetPlayer(game.Player_1_ID);
            newGame.Player_1_Score = game.Player_1_Score;

            newGame.Player_2 = await _repository.GetPlayer(game.Player_2_ID);
            newGame.Player_2_Score = game.Player_2_Score;

            newGame.Id = Guid.NewGuid();
            newGame.CreationTime = System.DateTime.Now;

            // calculate and apply mmr changes
            CalculateMmrChanges(newGame);

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

        /// <summary>
        /// Calculates how much mmr changes for each player in a game and applies the changes to the players' Mmrs
        /// </summary>
        /// <param name="game"></param>
        private void CalculateMmrChanges(Game game)
        {
            int mmrP1 = game.Player_1.Mmr;
            int mmrP2 = game.Player_2.Mmr;
            int scoreP1 = game.Player_1_Score;
            int scoreP2 = game.Player_2_Score;
            float mmrRel;
            float scoreRel;
            if (mmrP1 == 0)
                mmrP1 = 1;
            if (mmrP2 == 0)
                mmrP2 = 1;
            if (scoreP1 == 0)
                scoreP1 = 1;
            if (scoreP2 == 0)
                scoreP2 = 1;
            if (game.Player_1_Score < game.Player_2_Score)
            {
                mmrRel = (mmrP2 / mmrP1);
                scoreRel = scoreP2 / scoreP1; // p2mmr = 100 p1mmr = 50 p2sco = 100 p1sco = 50
                if (mmrRel > 2)
                    mmrRel = 2;
                if (scoreRel > 2)
                    scoreRel = 2;
                game.Player_2_Rank_Change += (int) (10f * scoreRel / mmrRel);
                game.Player_1_Rank_Change -= (int) (10f / scoreRel * mmrRel);
            }
            else if (game.Player_1_Score > game.Player_2_Score)
            {
                mmrRel = (mmrP1 / mmrP2);
                scoreRel = scoreP1 / scoreP2;
                if (mmrRel > 2)
                    mmrRel = 2;
                if (scoreRel > 2)
                    scoreRel = 2;
                game.Player_2_Rank_Change -= (int) (10f / scoreRel * mmrRel);
                game.Player_1_Rank_Change += (int) (10f * scoreRel / mmrRel);
            }
            else
            {
                if (mmrP1 > mmrP2)
                {
                    mmrRel = (mmrP1 / mmrP2);
                    if (mmrRel > 2)
                        mmrRel = 2;
                    game.Player_2_Rank_Change += (int) (5f * mmrRel);
                    game.Player_1_Rank_Change -= (int) (5f / mmrRel);
                }
                else if (mmrP1 < mmrP2)
                {
                    mmrRel = (mmrP2 / mmrP1);
                    if (mmrRel > 2)
                        mmrRel = 2;
                    game.Player_2_Rank_Change -= (int) (5f / mmrRel);
                    game.Player_1_Rank_Change += (int) (5f * mmrRel);
                }

            }
        }

    }
}
