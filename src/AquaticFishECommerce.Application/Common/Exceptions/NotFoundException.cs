using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
