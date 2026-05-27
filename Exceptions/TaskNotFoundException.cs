namespace TaskManager.Exceptions
{
    public class TaskNotFoundException : Exception
    {
        public int TaskId { get; }
        public TaskNotFoundException(int taskId)
            : base($"Task with ID {taskId} not found.")
        {
            TaskId = taskId;
        }
    }
}
