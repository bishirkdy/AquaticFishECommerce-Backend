namespace AquaticFishECommerce.Application.Common.Exceptions
{
    // Thrown when the user is authenticated but does not have permission (HTTP 403 Forbidden).
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message) : base(message) { }
    }
}
