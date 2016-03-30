using System;

namespace XpressNetSharp
{
    public class TrackPowerOffBroadcastMessage : Packet
    {
        public TrackPowerOffBroadcastMessage() : base(PacketHeaderType.CommandStationOperationResponse) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.CommandStationOperationResponse.TrackPowerOffBroadcast));
        }
    }
}
