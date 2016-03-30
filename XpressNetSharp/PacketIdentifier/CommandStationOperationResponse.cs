using System;

namespace XpressNetSharp.PacketIdentifier
{
    [Flags]
    public enum CommandStationOperationResponse  //Packet length does not include header or checksum and refers to the headers lower nibble!
    {
        TrackPowerOffBroadcast = 0x00,              //packet length = 1
        NormalOperationsResumedBroadcast = 0x01,    //packet length = 1
        ServiceModeEntryBroadcast = 0x02,           //packet length = 1
        ServiceModeForRegisterAndPagedMode = 0x10, //packet length = 3
        ProgrammingInfoCommandStationReady = 0x11, //packet length = 1      Not implemented?
        ProgrammingInfoShortCircuit = 0x12, //packet length = 1
        ProgrammingInfoDataByteNotFound = 0x13, //packet length = 1
        ServiceModeForDirectCvMode = 0x14, //packet length = 3
        ProgrammingInfoCommandStationBusy = 0x1f, //packet length = 1      Not implemented?
        CmdStnSoftwareVersion = 0x21, //packet length = 3
        CmdStnStatus = 0x22, //packet length = 2
        TransferErrors = 0x80, //packet length = 1
        CmdStnBusy = 0x81, //packet length = 1
        InstructionUnsupported = 0x82, //packet length = 1
    }
}
