using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoListModel.Models;

namespace ToDoListModel.DataLayer
{
    /// <summary>
    /// Data access layer storing data in a json file
    /// </summary>
    public class JsonDAL : IDataAccessLayer
    {
        /// <summary>
        /// In memory list for the tasks
        /// </summary>
        List<ToDoTask> tasks = new();
        /// <summary>
        /// Name of the json file
        /// </summary>
        readonly string tasksFileName = "todotasks.json";

        /// <summary>
        /// Constructor
        /// </summary>
        public JsonDAL()
        {
            this.GetFromFile();
            if (tasks.Count == 0)
            {
                // create dummydata
                this.CreateDummyData();
            }
        }

        /// <summary>
        /// Get the data from file
        /// </summary>
        private void GetFromFile()
        {
            try
            {
                string file = File.ReadAllText(tasksFileName);
                if (!string.IsNullOrEmpty(file))
                {
                    tasks = JsonSerializer.Deserialize<List<ToDoTask>>(file);
                }
                else
                { 
                    tasks = new();
                }
            }
            catch (Exception)
            {
                tasks?.Clear();
            }
        }

        /// <summary>
        /// Save the data to file
        /// </summary>
        private void SaveToFile()
        {
            File.WriteAllText(tasksFileName, JsonSerializer.Serialize(tasks));

        }

        /// <summary>
        /// Create dummy data
        /// </summary>
        private void CreateDummyData()
        {
            CreateToDoTask(new ToDoTask("Boodschappen doen"));
            CreateToDoTask(new ToDoTask("Terras vegen"));
            CreateToDoTask(new ToDoTask("Vaatwasser uitruimen"));
            CreateToDoTask(new ToDoTask("Hond uitlaten"));
            CreateToDoTask(new ToDoTask("Bier drinken"));            
        }

        #region Implemented interface methods (for comments see interface)

        /// <summary>
        /// Create the todo task async
        /// </summary>
        /// <param name="toDoTask">The task to create</param>
        /// <returns>The created task</returns>
        public async Task<ToDoTask> CreateToDoTask(ToDoTask toDoTask)
        {
            // create ids
            int maxId = tasks.Count == 0 ? 1 : tasks.Max(l => l.Id) + 1;
            toDoTask.Id = maxId++;

            tasks.Add(toDoTask);
            this.SaveToFile();
            return await Task.FromResult(toDoTask);
        }

        /// <summary>
        /// Delete the task async
        /// </summary>
        /// <param name="ToDoTask">The task</param>
        /// <returns>true on success</returns>
        public async Task<bool> DeleteToDoTask(ToDoTask ToDoTask)
        {
            try
            {
                tasks.Remove(ToDoTask);
                this.SaveToFile();
                return await Task.FromResult(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Read the task async
        /// </summary>
        /// <param name="id">The id of the task to read</param>
        /// <returns></returns>
        public async Task<ToDoTask?> ReadToDoTask(int id)
        {
            return await Task.FromResult(tasks.Find(x => x.Id == id));
        }

        /// <summary>
        /// Read all tasks async
        /// </summary>
        /// <returns>The list of tasks</returns>
        public async Task<List<ToDoTask>> ReadToDoTasks()
        {
            return await Task.FromResult(tasks);
        }

        public async Task<ToDoTask> UpdateToDoTask(ToDoTask toDoTask)
        {
            // easiest is remove and add again
            var toDelete = tasks.Find(x => x.Id == toDoTask.Id);
            if (toDelete != null)
            {
                tasks.Remove(toDelete);
                tasks.Add(toDoTask);
            }
            this.SaveToFile();
            return await Task.FromResult(toDoTask);            
        }

        #endregion
    }
}
