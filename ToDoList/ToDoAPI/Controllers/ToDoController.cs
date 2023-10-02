using Microsoft.AspNetCore.Mvc;
using ToDoListModel.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        // GET: api/<ToDoController>
        /// <summary>
        /// Haal alle todo items op!!!
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ToDoTask))]
        [ProducesDefaultResponseType(typeof(IEnumerable<ToDoTask>))]
        public async Task<IActionResult> Get()
        {
            var items = ToDoTask.ReadAll();
            return Ok(items);
        }

        // GET api/<ToDoController>/5
        /// <summary>
        /// Haal het ToDo item op op basis van het id
        /// </summary>
        /// <param name="id">Het id van het item</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID");
            }
            var item = ToDoTask.Read(id);
            if (item == null)
            {
                return NotFound($"Id {id} not found");
            }
            return Ok(item);
        }

        // POST api/<ToDoController>
        /// <summary>
        /// Maak een nieuw ToDo item aan
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ToDoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ToDoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
