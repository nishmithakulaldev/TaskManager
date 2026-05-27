using TaskManager.Interfaces;

namespace TaskManager.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public abstract string Name { get; }
        public abstract void Execute(string[] args);

        protected string? GetFlag(string[] args, string flagName)
        {
            var index = Array.IndexOf(args, flagName);
            if (index != -1 && index + 1 < args.Length)
                return args[index + 1];
            return null;
        }
    }
}
