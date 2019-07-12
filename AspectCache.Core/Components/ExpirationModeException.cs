using System;
using System.Collections.Generic;
using System.Text;

namespace AspectCache.Core.Components
{
    [Serializable]
    public class ExpirationModeException : Exception
    {
        public ExpirationModeException() { }

        public ExpirationModeException(string message)
            : base(message) { }
    }
}
