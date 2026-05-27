using TaskManager.Helpers;
using TaskManager.Interfaces;

namespace TaskManager.Commands
{
    public class ListCommand : BaseCommand
    {
        private readonly ITaskService _taskService;
        public ListCommand(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public override string Name => "list";
        public override void Execute(string[] args)
        {
            var statusFilter = GetFlag(args, "--status");
            var priorityFilter = GetFlag(args, "--priority");
            var sortFilter = GetFlag(args, "--sort");
            var tasks = _taskService.ListTasks(statusFilter, priorityFilter, sortFilter);   
            ConsoleHelper.DisplayTasks(tasks);
        }
    }
}
