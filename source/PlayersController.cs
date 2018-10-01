using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_project
{
    [Authorize(Roles = "Admin, User")]
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController
    {
        PlayersProcessor _processor;

        public PlayersController(PlayersProcessor processor)
        {
            _processor = processor;
        }

        [HttpGet("{id:Guid}")]
        public Task<Player> Get(Guid id)
        {
            return _processor.Get(id);
        }

        [HttpGet]
        public Task<Player[]> GetAll()
        {
            return _processor.GetAll();
        }

        [ValidateModel]
        [HttpPost]
        public Task<Player> Create([FromBody] NewPlayer player)
        {
            return _processor.Create(player);
        }

        [ValidateModel]
        [HttpPut("{id:Guid}")]
        public Task<Player> Modify(Guid id, [FromBody] ModifiedPlayer player)
        {
            return _processor.Modify(id, player);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:Guid}")]
        public Task<Player> Delete(Guid id)
        {
            return _processor.Delete(id);

        }

        [HttpGet("{playerId:Guid}/NextOpponent")]
        public Task<Player> GetNextOpponent(Guid playerId)
        {
            return _processor.GetNextOpponent(playerId);
        }
        
        [HttpGet("TopTenScore")]
        public Task<Player[]> GetTopTenByScore()
        {
            return _processor.GetTopTenByScore();
        }
        
        [HttpGet("TopTenRank")]
        public Task<Player[]> GetTopTenByRank()
        {
            return _processor.GetTopTenByRank();
        }
    }
}