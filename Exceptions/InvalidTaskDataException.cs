namespace TaskManager.Exceptions
{
    public class InvalidTaskDataException : Exception
    {
        public InvalidTaskDataException(string message) : base(message)
        {
        }
    }
}
