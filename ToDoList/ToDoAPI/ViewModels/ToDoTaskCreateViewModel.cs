namespace ToDoAPI.ViewModels
{
    /// <summary>
    /// Model for creating a task
    /// </summary>
    public class ToDoTaskCreateViewModel
    {
        /// <summary>
        /// Description of the task
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Responsible person for the task. Empty if no person assigned
        /// </summary>
        public string? AssignedName { get; set; }
    }
}
