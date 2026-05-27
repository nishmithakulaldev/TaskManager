using TaskManager.Interfaces;
using TaskManager.Models.Enums;

namespace TaskManager.Commands
{
    public class AddCommand : BaseCommand
    {
        private readonly ITaskService _taskService;

        public AddCommand(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public override string Name => "add";

        public override void Execute(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: add \"title\" [--priority low|medium|high] [--category name]");
                return;
            }
            var title = args[1];
            Priority priority = Priority.Medium;
            var priorityFlag = GetFlag(args, "--priority");
            if (priorityFlag != null && Enum.TryParse(priorityFlag, true, out Priority parsedPriority))
                priority = parsedPriority;
            var category = GetFlag(args, "--category") ?? "General";
            _taskService.AddTask(title, priority, category);
            Console.WriteLine("Task added successfully.");
        }
    }
}
