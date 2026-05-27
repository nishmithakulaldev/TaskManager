using TaskManager.Interfaces;
using System.Text.Json;
using TaskManager.Models;
using System.Text.Json.Serialization;

namespace TaskManager.Repositories
{
    public class JsonTaskRepository : ITaskRepository
    {
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };

        /// <summary>
        /// Get all tasks from the JSON file. If the file doesn't exist, return an empty list.
        /// </summary>
        /// <returns>A list of all task items.</returns>
        public List<TaskItem> GetAll()
        {
            if (!File.Exists("tasks.json"))
                return new List<TaskItem>();
            var json = File.ReadAllText("tasks.json");
            return JsonSerializer.Deserialize<List<TaskItem>>(json, _options) ?? new List<TaskItem>();
        }

        /// <summary>
        /// Get a task by its ID from the JSON file. If the task doesn't exist, return null.
        /// </summary>
        /// <param name="id">The ID of the task to retrieve.</param>
        /// <returns>The task item with the specified ID, or null if not found.</returns>
        public TaskItem? GetById(int id)
        {
            var tasks = GetAll();
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        /// <summary>
        /// Add a new task to the JSON file. The task will be appended to the existing list of tasks. If the file doesn't exist, it will be created.
        /// </summary>
        /// <param name="task">The task item to add.</param>
        public void Add(TaskItem task)
        {
            var tasks = GetAll();
            tasks.Add(task);
            SaveChanges(tasks);
        }

        /// <summary>
        /// Update an existing task in the JSON file. The task will be replaced with the new values. If the task doesn't exist, no changes will be made.
        /// </summary>
        /// <param name="task">The task item to update.</param>
        public void Update(TaskItem task)
        {
            var tasks = GetAll();
            var index = tasks.FindIndex(t => t.Id == task.Id);
            if (index != -1)
            {
                tasks[index] = task;
                SaveChanges(tasks);
            }
        }

        /// <summary>
        /// Delete a task from the JSON file by its ID. If the task doesn't exist, no changes will be made.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        public void Delete(int id)
        {
            var tasks = GetAll();
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                tasks.Remove(task);
                SaveChanges(tasks);
            }
        }

        private void SaveChanges(List<TaskItem> tasks)
        {
            var json = JsonSerializer.Serialize(tasks, _options);
            File.WriteAllText("tasks.json", json);
        }
    }
}