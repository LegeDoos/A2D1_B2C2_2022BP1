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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType(typeof(ToDoTask))]
        public IEnumerable<ToDoTask> Get()
        {
            return ToDoTask.ReadAll();
        }

        // GET api/<ToDoController>/5
        /// <summary>
        /// Haal het ToDo item op op basis van het id
        /// </summary>
        /// <param name="id">Het id van het item</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
