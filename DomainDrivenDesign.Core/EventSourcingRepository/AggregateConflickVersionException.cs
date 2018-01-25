using System;
using System.Runtime.Serialization;

namespace DomainDrivenDesign.Core.EventSourcingRepository
{
    public class AggregateConflickVersionException : Exception
    {
        public AggregateConflickVersionException()
        {
        }

        public AggregateConflickVersionException(string message) : base(message)
        {
        }

        public AggregateConflickVersionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AggregateConflickVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}