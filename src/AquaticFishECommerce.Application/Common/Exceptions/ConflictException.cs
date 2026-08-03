
namespace AquaticFishECommerce.Application.Common.Exceptions
{
    // Thrown when a request conflicts with existing data (HTTP 409 Conflict).
    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message) { }
    }
}
