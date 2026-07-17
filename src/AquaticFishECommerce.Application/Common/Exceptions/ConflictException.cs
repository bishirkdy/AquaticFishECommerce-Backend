using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Common.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message) { }
    }
}
