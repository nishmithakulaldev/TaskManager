using TaskManager.Interfaces;

namespace TaskManager.Commands
{
    public class SummaryCommand : BaseCommand
    {
        private readonly ITaskService _taskService;
        public SummaryCommand(ITaskService taskService)
        {
            _taskService = taskService;
        }
        public override string Name => "summary";
        public override void Execute(string[] args)
        {
            var summary = _taskService.GetSummary();
            Console.WriteLine($"Total: {summary.Total} | Pending: {summary.Pending} | Done: {summary.Done}");
            Console.WriteLine($"By Category: {summary.ByCategory}");
        }
    }
}
