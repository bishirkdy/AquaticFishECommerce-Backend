namespace AquaticFishECommerce.Application.Common.Exceptions
{
    public class DatabaseException : Exception
    {   // Thrown when a database operation fails.
        public DatabaseException(string message) : base(message) { }
        public DatabaseException(string message , Exception innerException) : base(message , innerException) { }
    }
}
