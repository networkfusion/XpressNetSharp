using System;

namespace XpressNetSharp
{
    public class NormalOperationsResumedBroadcastMessage : Packet
    {
        public NormalOperationsResumedBroadcastMessage() : base(PacketHeaderType.CommandStationOperationResponse) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.CommandStationOperationResponse.NormalOperationsResumedBroadcast));
        }
    }
}
