using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XpressNetSharp.Responses
{
    public class ProgrammingInfoDataByteNotFoundResp : IResponse
    {
        public IList<byte> Payload { get; set; }
    }
}
