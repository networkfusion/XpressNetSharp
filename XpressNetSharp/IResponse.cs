using System.Collections.Generic;

namespace XpressNetSharp
{
    public interface IResponse
    {
        IList<byte> Payload { get; set; }
    }
}
