using System.Collections.Generic;

namespace XpressNetSharp
{
    public class CmdStnSoftwareVersionResp : IResponse
    {
        public IList<byte> Payload { get; set; }

        public int Major { get { return Payload[1] >> 4; } }
        public int Minor { get { return Payload[1] & 0x0F; } }
        public int CmdStationTypeId { get { return Payload[2]; } }
    }
}
