namespace TaskManager.Commands
{
    public class HelpCommand : BaseCommand
    {
        public HelpCommand() { }
        public override string Name => "help";
        public override void Execute(string[] args)
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("  add \"title\" [--priority low|medium|high] [--category name] - Add a new task");
            Console.WriteLine("  list [--status pending|completed] [--priority low|medium|high] [--sort title|priority] - List tasks");
            Console.WriteLine("  complete <id> - Mark a task as completed");
            Console.WriteLine("  summary - Show task summary");
            Console.WriteLine("  help - Show this help message");
        }
    }
}
