namespace TaskCli.exceptions
{
    internal class TaskNotFoundException : Exception
    {
        public TaskNotFoundException()
        {

        }

        public TaskNotFoundException(string message): base(message)
        {

        }
    }
}
