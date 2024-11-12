namespace Common.Exceptions
{
    public class InvalidDatabaseSchemeException : Exception
    {
        public InvalidDatabaseSchemeException(string? message)
            :base(message)
        {
        }
    }
}
