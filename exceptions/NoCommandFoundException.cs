namespace TaskCli.exceptions
{
    internal class NoCommandFoundException : Exception
    {
        public NoCommandFoundException()
        {

        }

        public NoCommandFoundException(string message): base(message)
        {

        }
    }
}
