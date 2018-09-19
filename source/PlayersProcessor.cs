using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend_project
{
    public class PlayersProcessor
    {
        private IRepository _repository;

        public PlayersProcessor(IRepository repository) {
            _repository = repository;
        }

        public Task<Player> Get(Guid id)
        {
            return _repository.Get(id);
        }

        public Task<Player[]> GetAll()
        {
            return _repository.GetAll();
        }

        public Task<Player> Create(NewPlayer player)
        {
            Player newPlayer = new Player();
            newPlayer.Name = player.Name;
            // set other values for new player
            newPlayer.Id = Guid.NewGuid();
            newPlayer.CreationTime = System.DateTime.Now;

            return _repository.Create(newPlayer);
        }

        public Task<Player> Modify(Guid id, ModifiedPlayer player)
        {
            return _repository.Modify(id, player);
        }

        public Task<Player> Delete(Guid id)
        {
            return _repository.Delete(id);
        }
    }
}
