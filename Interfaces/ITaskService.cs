using TaskManager.Models;
using TaskManager.Models.Enums;

namespace TaskManager.Interfaces
{
    public interface ITaskService
    {
        /// <summary>
        /// Adds a new task to the system with the specified title, priority, and category. The task will be assigned a unique ID and marked as "Pending" by default, and 
        /// the creation date will be set to the current date and time. 
        /// The title must not be empty or consist solely of whitespace, and the priority must be a valid value from the Priority enum. 
        /// The category can be any string, but it is recommended to use meaningful categories for better organization. 
        /// If the input parameters are valid, the task will be added successfully; otherwise, an appropriate error message will be returned.
        /// </summary>
        /// <param name="title">The title of the task. Cannot be empty or whitespace.</param>
        /// <param name="priority">The priority of the task.</param>
        /// <param name="category">The category of the task.</param>
        /// <exception cref="InvalidTaskDataException">Thrown when the task data is invalid.</exception>
        void AddTask(string title, Priority priority, string category);

        /// <summary>
        /// Completes the task with the specified ID by changing its status to "Done". If the task with the given ID does not exist, an appropriate error message will be returned.
        /// </summary>
        /// <param name="id">The ID of the task to complete.</param>
        /// <exception cref="TaskNotFoundException">Thrown when the task with the specified ID does not exist.</exception>
        void CompleteTask(int id);

        /// <summary>
        /// Deletes the task with the specified ID from the system. If the task with the given ID does not exist, an appropriate error message will be returned.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        /// <exception cref="TaskNotFoundException">Thrown when the task with the specified ID does not exist.</exception>
        void DeleteTask(int id);

        /// <summary>
        /// Lists all tasks in the system, optionally filtered by status and/or priority, and sorted by creation date.
        /// </summary>
        /// <param name="statusFilter">Optional filter for task status ("Pending" or "Done").</param>
        /// <param name="priorityFilter">Optional filter for task priority ("Low", "Medium", "High").</param>
        /// <param name="sortFilter">Optional sort criteria ("Priority", "Status", "CreatedDate", "Category", "Id", "Title").</param>
        /// <returns>A list of tasks matching the specified filters and sort criteria, or an empty list if no tasks match.</returns>
        List<TaskItem> ListTasks(string? statusFilter, string? priorityFilter, string? sortFilter);

        /// <summary>
        /// Gets a summary of the tasks in the system, including the total number of tasks, the number of pending tasks, the number of completed tasks, 
        /// and a breakdown of tasks by category.
        /// </summary>
        /// <returns>A summary of the tasks in the system.</returns>
        TaskSummary GetSummary();

    }
}
