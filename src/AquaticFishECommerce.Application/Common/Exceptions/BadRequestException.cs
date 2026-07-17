using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Common.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}
