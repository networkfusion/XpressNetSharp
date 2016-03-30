using System.Collections.Generic;

namespace XpressNetSharp
{
    public class LocoInfoResp_MultiUnitAddressResp : IResponse
    {
        public IList<byte> Payload { get; set; }

        public int Status { get; private set; } //this should refer to an enum
        public int SpeedStep { get; private set; } //this should refer to an enum
        public Direction Direction { get; private set; }
        public int Speed { get; private set; }
    }
}
