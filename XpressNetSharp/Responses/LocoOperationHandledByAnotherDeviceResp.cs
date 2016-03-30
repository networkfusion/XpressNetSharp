using System.Collections.Generic;

namespace XpressNetSharp
{
    public class LocoOperationHandledByAnotherDeviceResp : IResponse
    {
        public IList<byte> Payload { get; set; }

        public int Address { get; private set; }

    }
}
