using Microsoft.AspNetCore.Mvc;
using ToDoAPI.ViewModels;
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
        /// Haal alle todo items op
        /// </summary>
        /// <returns>Lijst met alle ToDo items</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ToDoTask))]
        [ProducesDefaultResponseType(typeof(IEnumerable<ToDoTask>))]
        public async Task<IActionResult> GetAll()
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
        [HttpGet("{id}", Name = nameof(GetTask))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTask(int id)
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
        /// <param name="toDoTaskViewModel">De taak die je wilt aanmaken</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ToDoTask))]
        public async Task<IActionResult> Post([FromBody] ToDoTaskCreateViewModel toDoTaskViewModel)
        {
            // can only add when no id is given
            if (toDoTaskViewModel == null 
                || string.IsNullOrEmpty(toDoTaskViewModel.Description))
            {
                return BadRequest();
            }

            // add the task
            ToDoTask newTask = new ToDoTask(toDoTaskViewModel.Description) {                 
                AssignedName = toDoTaskViewModel.AssignedName };
            
            newTask.Create();

            // give the correct response
            return CreatedAtRoute(
                routeName: nameof(GetTask),
                routeValues: new { id = newTask.Id },
                value: newTask);
        }

        // PUT api/<ToDoController>/5
        /// <summary>
        /// Assign a person to the task
        /// </summary>
        /// <param name="id">The task id</param>
        /// <param name="personName">The name of the person</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AssignPersonToTask(int id, [FromBody] string personName)
        {
            if (string.IsNullOrEmpty(personName))
            {
                return BadRequest();
            }
            var task = ToDoTask.Read(id);
            if (task == null)
            {
                return NotFound();
            }
            else
            {
                if (task.AssignPerson(personName))
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        // DELETE api/<ToDoController>/5
        /// <summary>
        /// Delete the task
        /// </summary>
        /// <param name="id">The id to delete</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = ToDoTask.Read(id);
            if (task == null)
            {
                return NotFound();
            }
            else
            {
                task.Delete();
                return Ok();
            }
        }
    }
}
