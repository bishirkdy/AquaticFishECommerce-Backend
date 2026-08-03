namespace AquaticFishECommerce.Application.Common.Exceptions
{   // Thrown when the user is not authenticated (HTTP 401 Unauthorized).
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message) { }
    }
}
