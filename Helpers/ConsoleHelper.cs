using System.Text;
using TaskManager.Models;
using TaskManager.Models.Enums;

namespace TaskManager.Helpers
{
    public static class ConsoleHelper
    {
        public static void DisplayTasks(List<TaskItem> tasks)
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }
            int idWidth = 5;
            int titleWidth = 20;
            int priorityWidth = 10;
            int categoryWidth = 12;
            int statusWidth = 10;
            int dateWidth = 12;

            StringBuilder header = new StringBuilder();
            header.Append("ID".PadRight(idWidth));
            header.Append("Title".PadRight(titleWidth));
            header.Append("Priority".PadRight(priorityWidth));
            header.Append("Category".PadRight(categoryWidth));
            header.Append("Status".PadRight(statusWidth));
            header.Append("Created At".PadRight(dateWidth));

            Console.WriteLine(header.ToString());
            Console.WriteLine(new string('-', header.Length));


            foreach (var task in tasks)
            {
                StringBuilder row = new StringBuilder();
                row.Append(task.Id.ToString().PadRight(idWidth));
                row.Append(Truncate(task.Title, titleWidth).PadRight(titleWidth));
                row.Append(task.Priority.ToString().PadRight(priorityWidth));
                row.Append(task.Category.PadRight(categoryWidth));
                row.Append(task.Status.ToString().PadRight(statusWidth));
                row.Append(task.CreatedAt.ToString("yyyy-MM-dd").PadRight(dateWidth));
                Console.ForegroundColor = task.Status == Status.Done ? ConsoleColor.Green : ConsoleColor.Yellow;
                Console.WriteLine(row.ToString());
                Console.ResetColor();
            }
        }

        static string Truncate(string text, int maxLength)
        {
            return text.Length > maxLength - 3
                ? text[..(maxLength - 3)] + "..."
                : text;
        }
    }
}
