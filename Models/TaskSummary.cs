namespace TaskManager.Models
{
    public class TaskSummary
    {
        public int Total { get; set; }
        public int Pending { get; set; }
        public int Done { get; set; }
        public string ByCategory { get; set; } = string.Empty;
    }
}
