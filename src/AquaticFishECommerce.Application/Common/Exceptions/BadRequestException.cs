namespace AquaticFishECommerce.Application.Common.Exceptions
{
    //// Custom exception thrown when a request is invalid (HTTP 400 Bad Request).
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}
