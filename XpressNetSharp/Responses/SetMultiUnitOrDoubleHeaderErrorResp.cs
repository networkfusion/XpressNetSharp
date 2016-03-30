using System.Collections.Generic;

namespace XpressNetSharp
{
    public class SetMultiUnitOrDoubleHeaderErrorResp : IResponse
    {
        public IList<byte> Payload { get; set; }

        public int State { get; private set; } //TODO: this should implement an enum
    }
}
