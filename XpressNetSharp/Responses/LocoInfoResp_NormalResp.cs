using System.Collections.Generic;

namespace XpressNetSharp
{
    public class LocoInfoResp_NormalResp : IResponse
    {
        public IList<byte> Payload { get; set; }

        public int Status { get; set; } //this should refer to an enum
        public int SpeedStep { get; set; } //this should refer to an enum
        public Direction Direction { get; set; }
        public int Speed { get; set; }
        public FunctionState Function1 { get; set; }
        public FunctionState Function2 { get; set; }
        public FunctionState Function3 { get; set; }
        public FunctionState Function4 { get; set; }
        public FunctionState Function5 { get; set; }
        public FunctionState Function6 { get; set; }
        public FunctionState Function7 { get; set; }
        public FunctionState Function8 { get; set; }
        public FunctionState Function9 { get; set; }
        public FunctionState Function10 { get; set; }
        public FunctionState Function11 { get; set; }
        public FunctionState Function12 { get; set; }
    }
}
