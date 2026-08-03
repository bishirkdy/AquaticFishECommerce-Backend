namespace AquaticFishECommerce.Application.Common.Exceptions
{   // Thrown when request data fails validation.
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }
}
