using System;
using System.Collections.Generic;

namespace XpressNetSharp
{
    //this response can be generated for any of the programming requests but only if the Request for servicemoderesults is sent first

    public class ServiceModeProgrammingResultsResp : IResponse
    {
        public enum ProgrammingMode : byte
        {
            Direct = 0x14,
            RegisterOrPaged = 0x10
        }

        public IList<byte> Payload { get; set; }

        public ProgrammingMode Mode { get { return (ProgrammingMode)Enum.ToObject(typeof(ProgrammingMode), Payload[0]); } }
        //TODO: if we return this type for any service mode programming, we need to check payload has a value for bytes 1 and 2
        public int RegisterOrCv { get {return Payload[1]; } } //TODO: cv 256 should be converted from 0? TEST
        public int Value { get { return Payload[2]; } }
    }
}
