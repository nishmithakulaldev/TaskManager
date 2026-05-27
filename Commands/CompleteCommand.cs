using TaskManager.Interfaces;

namespace TaskManager.Commands
{
    public class CompleteCommand : BaseCommand
    {
        private readonly ITaskService _taskService;

        public CompleteCommand(ITaskService taskService)
        {
            _taskService = taskService;
        }
        public override string Name => "complete";
        public override void Execute(string[] args)
        {
            if (args.Length < 2 || !int.TryParse(args[1], out int completeId))
            {
                Console.WriteLine("Usage: complete <id>");
                return;
            }
            _taskService.CompleteTask(completeId);
            Console.WriteLine("Task completed successfully.");
        }
    }
}
