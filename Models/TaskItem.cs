using TaskManager.Models.Enums;

namespace TaskManager.Models
{
    public class TaskItem
    {
        public TaskItem(int id, string title, Priority priority, string category)
        {
            Id = id;
            Title = title;
            Priority = priority;
            Category = category;
            Status = Status.Pending;
            CreatedAt = DateTime.Now;
        }
        public int Id { get; private set; }
        public string Title { get; private set; }
        public Priority Priority { get; private set; }
        public string Category { get; private set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; init; }

        public override string ToString()
        {
            return $"[{Id}] {Title} | {Priority} | {Category} | {Status} | {CreatedAt:yyyy-MM-dd}";
        }
    }
}
