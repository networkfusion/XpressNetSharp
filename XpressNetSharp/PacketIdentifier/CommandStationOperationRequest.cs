using System;

namespace XpressNetSharp.PacketIdentifier
{
    [Flags]
    public enum CommandStationOperationRequest : byte
    {
        //AckResp = 0x20, //THIS IS THE CHECKSUM!
        ServiceModeResults = 0x10,
        RegisterModeRead = 0x11,
        RegisterModeWrite = 0x12,
        PagedModeRead = 0x14,
        DirectModeRead = 0x15, //CV
        DirectModeWrite = 0x16,
        PagedModeWrite = 0x17,
        SoftwareVersion = 0x21,
        PowerUpMode = 0x22,
        Status = 0x24,
        StopOperations = 0x80,
        ResumeOperations = 0x81
    }
}
