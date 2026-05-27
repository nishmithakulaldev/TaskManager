namespace TaskManager.Interfaces
{
    public interface ICommand
    {
        string Name { get; }
        void Execute(string[] args);
    }
}
