using System;
using System.Collections.Generic;

namespace XpressNetSharp
{
    public enum ResponseType
    {
        TrackPowerOffOrEmergencyStop = 0x00,
        NormalOperationsResumed = 0x01,
        ServiceModeEntry = 0x02,
        CommandSuccessOrResumedAfterError = 0x04,
        ProgrammingInfoCommandStationReady = 0x11,
        ProgrammingInfoShortCircuit = 0x12,
        ProgrammingInfoDatabyteNotFound = 0x13,
        ProgrammingInfoCommandStationBusy = 0x1f,
        TransferError = 0x80,
        CommandStationBusy = 0x81,
        InstructionNotSupported = 0x82,


    }

    public class GeneralResponse : IResponse
    {
        public IList<byte> Payload { get; set; }
        public ResponseType Type { get {return (ResponseType) Enum.Parse(typeof(ResponseType), Payload[0].ToString()) ;} }
    }
}
