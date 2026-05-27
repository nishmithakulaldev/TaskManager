using TaskManager.Commands;
using TaskManager.Exceptions;
using TaskManager.Helpers;
using TaskManager.Interfaces;
using TaskManager.Models.Enums;
using TaskManager.Repositories;
using TaskManager.Services;

public class Program
{
    private static string? GetFlag(string[] args, string flagName)
    {
        var index = Array.IndexOf(args, flagName);
        if (index != -1 && index + 1 < args.Length)
            return args[index + 1];
        return null;
    }

    
    static void Main(string[] args)
    {
        ITaskRepository repository = new JsonTaskRepository();
        ITaskService service = new TaskService(repository);

        var commands = new List<ICommand>
        {
            new AddCommand(service),
            new CompleteCommand(service),
            new DeleteCommand(service),
            new ListCommand(service),
            new SummaryCommand(service)
        };

        try
        {
            var commandName = args.Length > 0 ? args[0].ToLower() : "";
            var command = commands.FirstOrDefault(c => c.Name == commandName);
            if (command != null)
            {
                command.Execute(args);
                return;
            }
            else
                Console.WriteLine("Unknown command. Use 'help' for a list of commands.");
        }
        catch(TaskNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch(InvalidTaskDataException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}