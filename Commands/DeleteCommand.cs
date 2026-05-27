using TaskManager.Interfaces;

namespace TaskManager.Commands
{
    public class DeleteCommand : BaseCommand
    {
        private readonly ITaskService _taskService;
        public DeleteCommand(ITaskService taskService)
        {
            _taskService = taskService;
        }
        public override string Name => "delete";
        public override void Execute(string[] args)
        {
            if (args.Length < 2 || !int.TryParse(args[1], out int deleteId))
            {
                Console.WriteLine("Usage: delete <id>");
                return;
            }
            _taskService.DeleteTask(deleteId);
            Console.WriteLine("Task deleted successfully.");
        }
    }
}
