using System.Collections.Generic;

namespace XpressNetSharp
{
    public class LocoInfoResp_DoubleHeaderResp : IResponse
    {
        public IList<byte> Payload { get; set; }

        public int Status { get; private set; } //this should refer to an enum
        public int SpeedStep { get; private set; } //this should refer to an enum
        public Direction Direction { get; private set; }
        public int Speed { get; private set; }
        public FunctionState Function1 { get; private set; }
        public FunctionState Function2 { get; private set; }
        public FunctionState Function3 { get; private set; }
        public FunctionState Function4 { get; private set; }
        public FunctionState Function5 { get; private set; }
        public FunctionState Function6 { get; private set; }
        public FunctionState Function7 { get; private set; }
        public FunctionState Function8 { get; private set; }
        public FunctionState Function9 { get; private set; }
        public FunctionState Function10 { get; private set; }
        public FunctionState Function11 { get; private set; }
        public FunctionState Function12 { get; private set; }
        public int SecondLocoAddress { get; private set; }
    }
}
