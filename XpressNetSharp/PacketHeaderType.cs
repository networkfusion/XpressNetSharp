using System;

namespace XpressNetSharp
{
    [Flags]
    public enum PacketHeaderType
    {
        CommunicationsStateResponse = 0,
        UnusedHeaderTypeOf1 = 1,
        CommandStationOperationsRequest = 2,
        UnusedHeaderTypeOf3 = 3,
        AccessoryDecoderInformation = 4,
        AccessoryDecoderOperation = 5,
        CommandStationOperationResponse = 6,
        UnusedHeaderTypeOf7 = 7,
        EmergencyStopAll = 8,
        EmergencyStopSingleLoco = 9,
        UnusedHeaderTypeOf10orA = 10, //old xbus loco information requests
        UnusedHeaderTypeOf11orB = 11, //old xbus loco operation requests
        UnusedHeaderTypeOf12orC = 12, //old xbus loco double header requests
        UnusedHeaderTypeOf13orD = 13,
        LocomotiveFunction = 14,
        UnusedHeaderTypeOf15orF = 15,

    }
}
