using System;
using System.Runtime.Serialization;

namespace TestRail.Exceptions
{
    public class StepsExceptions : Exception
    {
        public StepsExceptions()
        {
        }

        protected StepsExceptions(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public StepsExceptions(string message) : base(message)
        {
        }

        public StepsExceptions(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}