using TaskManager.Models;

namespace TaskManager.Interfaces
{
    public interface ITaskRepository
    {
        /// <summary>
        /// Gets all task items from the repository.
        /// </summary>
        /// <returns>A list of all task items.</returns>
        List<TaskItem> GetAll();
        /// <summary>
        /// Gets a task item by its ID.
        /// </summary>
        /// <param name="id">The ID of the task item.</param>
        /// <returns>The task item with the specified ID, or null if not found.</returns>
        TaskItem? GetById(int id);
        /// <summary>
        /// Adds a new task item to the repository.
        /// </summary>
        /// <param name="task">The task item to add.</param>
        void Add(TaskItem task);
        /// <summary>
        /// Updates an existing task item in the repository.
        /// </summary>
        /// <param name="task">The task item to update.</param>
        void Update(TaskItem task);
        /// <summary>
        /// Deletes a task item from the repository by its ID.
        /// </summary>
        /// <param name="id">The ID of the task item to delete.</param>
        void Delete(int id);
    }
}
