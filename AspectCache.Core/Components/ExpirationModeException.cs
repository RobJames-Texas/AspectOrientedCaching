using System;
using System.Runtime.Serialization;

namespace AspectCache.Core.Components
{
    [Serializable]
    public class ExpirationModeException : Exception
    {
        public ExpirationModeException() { }

        protected ExpirationModeException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public ExpirationModeException(string message)
            : base(message) { }
    }
}
