using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace backend_project
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController
    {
        GameProcessor _processor;

        public GameController(GameProcessor processor)
        {
            _processor = processor;
        }

        [HttpGet("{id:Guid}")]
        public Task<Game> Get(Guid id)
        {
            return _processor.Get(id);
        }

        [HttpGet]
        public Task<Game[]> GetAll()
        {
            return _processor.GetAll();
        }

        [HttpPost]
        public Task<Game> Create([FromBody] NewGame game)
        {
            return _processor.Create(game);
        }

        [HttpPut("{id:Guid}")]
        public Task<Game> Modify(Guid id, [FromBody] ModifiedGame game)
        {
            return _processor.Modify(id, game);
        }

        [HttpDelete("{id:Guid}")]
        public Task<Game> Delete(Guid id)
        {
            return _processor.Delete(id);
        }
    }
}