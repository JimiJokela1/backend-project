using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace backend_project
{
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

        [HttpPost]
        public Task<Player> Create([FromBody] NewPlayer player)
        {
            return _processor.Create(player);
        }

        [HttpPut("{id:Guid}")]
        public Task<Player> Modify(Guid id, [FromBody] ModifiedPlayer player)
        {
            return _processor.Modify(id, player);
        }

        [HttpDelete("{id:Guid}")]
        public Task<Player> Delete(Guid id)
        {
            return _processor.Delete(id);
        }

        // [Route("api/players")]
        // [HttpPost]
        // public Task<Player> Create(NewPlayer player)
        // {
        //     return _processor.Create(player);
        // }

        // [Route("api/players/{playerName}")]
        // [HttpPost]
        // public Task<Player> Create(string playerName)
        // {
        //     NewPlayer player = new NewPlayer(playerName);
        //     return _processor.Create(player);
        // }

        // [Route("api/players/diiba")]
        // [HttpPost]
        // public Task<Player> CreateDiiba()
        // {
        //     NewPlayer player = new NewPlayer("Diiba");
        //     return _processor.Create(player);
        // }

        // [Route("api/players")]
        // [HttpPut]
        // public Task<Player> Modify(Guid id, ModifiedPlayer player)
        // {
        //     return _processor.Modify(id, player);
        // }

        
    }
}
