using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace XpressNetSharp
{
    class XpressNetProtocolViolationException
    : Exception
    {
        public XpressNetProtocolViolationException()
        {
            XpressNetConstants.XpressNetTraceSource.TraceEvent(TraceEventType.Error, 0, "An error occured but no detail was given");
        }

        public XpressNetProtocolViolationException(string message)
            : base(message)
        {
            XpressNetConstants.XpressNetTraceSource.TraceEvent(TraceEventType.Error, 0, "An error occured: {0}", message);
        }

        public XpressNetProtocolViolationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected XpressNetProtocolViolationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
