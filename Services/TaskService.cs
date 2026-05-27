using TaskManager.Exceptions;
using TaskManager.Interfaces;
using TaskManager.Models;
using TaskManager.Models.Enums;

namespace TaskManager.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

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
        public void AddTask(string title, Priority priority, string category)
        {
            if(string.IsNullOrWhiteSpace(title))
                throw new InvalidTaskDataException("Title cannot be empty.");

            var newTask = new TaskItem(GenerateId(), title, priority, category);
            _taskRepository.Add(newTask);
        }

        private int GenerateId()
        {
            var tasks = _taskRepository.GetAll();
            return tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;
        }

        /// <summary>
        /// Completes the task with the specified ID by changing its status to "Done". If the task with the given ID does not exist, an appropriate error message will be returned.
        /// </summary>
        /// <param name="id">The ID of the task to complete.</param>
        /// <exception cref="TaskNotFoundException">Thrown when the task with the specified ID does not exist.</exception>
        public void CompleteTask(int id)
        {
            var task = _taskRepository.GetById(id)
                ?? throw new TaskNotFoundException(id);

            task.Status = Status.Done;
            _taskRepository.Update(task);
        }

        /// <summary>
        /// Deletes the task with the specified ID from the system. If the task with the given ID does not exist, an appropriate error message will be returned.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        /// <exception cref="TaskNotFoundException">Thrown when the task with the specified ID does not exist.</exception>
        public void DeleteTask(int id)
        {
            var task = _taskRepository.GetById(id)
                ?? throw new TaskNotFoundException(id);
            _taskRepository.Delete(id);
        }

        /// <summary>
        /// Lists all tasks in the system, optionally filtered by status and/or priority, and sorted by creation date.
        /// </summary>
        /// <param name="statusFilter">Optional filter for task status ("Pending" or "Done").</param>
        /// <param name="priorityFilter">Optional filter for task priority ("Low", "Medium", "High").</param>
        /// <param name="sortFilter">Optional sort criteria ("Priority", "Status", "CreatedDate", "Category", "Id", "Title").</param>
        /// <returns>A list of tasks matching the specified filters and sort criteria, or an empty list if no tasks match.</returns>
        public List<TaskItem> ListTasks(string? statusFilter, string? priorityFilter, string? sortFilter)
        {
            var tasks = _taskRepository.GetAll();
            if (!string.IsNullOrEmpty(statusFilter))
            {
                tasks = statusFilter.ToLower() switch
                {
                    "pending" => tasks.Where(t => t.Status == Status.Pending).ToList(),
                    "done" => tasks.Where(t => t.Status == Status.Done).ToList(),
                    _ => tasks
                };
            }
            if (!string.IsNullOrEmpty(priorityFilter)
                && Enum.TryParse(priorityFilter, true, out Priority parsedPriority))
            {
                tasks = tasks.Where(t => t.Priority == parsedPriority).ToList();
            }

            if (!string.IsNullOrEmpty(sortFilter))
            {
                tasks = sortFilter.ToLower() switch
                {
                    "priority" => tasks.OrderByDescending(t => t.Priority).ToList(),
                    "status" => tasks.OrderBy(t => t.Status).ToList(),
                    "createddate" => tasks.OrderBy(t => t.CreatedAt).ToList(),
                    "category" => tasks.OrderBy(t => t.Category).ToList(),
                    "id" => tasks.OrderBy(t => t.Id).ToList(),
                    "title" => tasks.OrderBy(t => t.Title).ToList(),
                    _ => tasks
                };
            }
            return tasks;
        }

        /// <summary>
        /// Gets a summary of the tasks in the system, including the total number of tasks, the number of pending tasks, the number of completed tasks, 
        /// and a breakdown of tasks by category.
        /// </summary>
        /// <returns>A summary of the tasks in the system.</returns>
        public TaskSummary GetSummary()
        {
            var tasks = _taskRepository.GetAll();
            var total = tasks.Count;
            var pending = tasks.Count(t => t.Status == Status.Pending);
            var done = tasks.Count(t => t.Status == Status.Done);
            var byCategory = tasks.GroupBy(t => t.Category)
                                    .Select(g => $"{g.Key} ({g.Count()})");
            TaskSummary summary = new TaskSummary()
            {
                Total = total,
                Pending = pending,
                Done = done,
                ByCategory = string.Join(" ", byCategory)
            };

            return summary;
        }
    }
}
