namespace TaskCli.exceptions
{
    internal class EmptyRequiredFieldException : Exception
    {
        public EmptyRequiredFieldException()
        {

        }

        public EmptyRequiredFieldException(string fieldName): base($"field '{fieldName}' should not be null")
        {

        }
    }
}
