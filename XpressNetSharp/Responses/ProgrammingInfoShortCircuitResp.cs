using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XpressNetSharp
{
    public class ProgrammingInfoShortCircuitResp : IResponse
    {
        public IList<byte> Payload { get; set; }
    }
}
