using System;

namespace XpressNetSharp
{
    public class ServiceModeEntryBroadcastMessage : Packet
    {
        public ServiceModeEntryBroadcastMessage() : base(PacketHeaderType.CommandStationOperationResponse) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.CommandStationOperationResponse.ServiceModeEntryBroadcast));
        }
    }
}
