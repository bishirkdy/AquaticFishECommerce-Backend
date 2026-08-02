namespace AquaticFishECommerce.Application.Common.Responses
{
    public class ErrorResponse
    {
        public bool Success { get; set; } = false;
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public IEnumerable<string>? Error { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
