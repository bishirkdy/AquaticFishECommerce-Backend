namespace AquaticFishECommerce.Application.Common.Exceptions
{// Thrown when the requested resource is not found (HTTP 404 Not Found)
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
