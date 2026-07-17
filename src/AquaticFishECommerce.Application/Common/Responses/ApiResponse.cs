using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Common.Responses
{
    internal class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty; //This prevents Message from being null.
        public T? Data { get; set; }
    }
}
