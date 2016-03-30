using System.Collections.Generic;

namespace XpressNetSharp
{
    public class FunctionTypeResp : IResponse
    {
        public IList<byte> Payload { get; set; }

        public FunctionType Function1 { get; private set; }
        public FunctionType Function2 { get; private set; }
        public FunctionType Function3 { get; private set; }
        public FunctionType Function4 { get; private set; }
        public FunctionType Function5 { get; private set; }
        public FunctionType Function6 { get; private set; }
        public FunctionType Function7 { get; private set; }
        public FunctionType Function8 { get; private set; }
        public FunctionType Function9 { get; private set; }
        public FunctionType Function10 { get; private set; }
        public FunctionType Function11 { get; private set; }
        public FunctionType Function12 { get; private set; }
    }
}
