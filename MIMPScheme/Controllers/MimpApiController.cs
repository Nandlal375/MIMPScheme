using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIMPScheme.Models;
using MIMPScheme.Repository;

namespace MIMPScheme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MimpApiController : ControllerBase
    {
        private readonly MimpRepository _repository;

        public MimpApiController(MimpRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Mimp>> Get()
        {
            var todos = _repository.GetAllMimps();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public ActionResult<Mimp> Get(int id)
        {
            var todo = _repository.GetMimpById(id);
            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Mimp mimp)
        {
            _repository.AddMimp(mimp);
            return CreatedAtAction(nameof(Get), new { id = mimp.Id }, mimp);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Mimp updatedTodo)
        {
            var todo = _repository.GetMimpById(id);
            if (todo == null)
            {
                return NotFound();
            }

            updatedTodo.Id = id;
            _repository.UpdateMimp(updatedTodo);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _repository.GetMimpById(id);
            if (todo == null)
            {
                return NotFound();
            }

            _repository.DeleteMimp(id);

            return NoContent();
        }
    }
}
