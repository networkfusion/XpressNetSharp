using System.Collections.Generic;

namespace XpressNetSharp
{
    public class AddressInquiryResp : IResponse
    {
        public IList<byte> Payload { get; set; }

        public int Address { get; set; }
        public int State { get; set; } //this should implement an enum
    }
}
