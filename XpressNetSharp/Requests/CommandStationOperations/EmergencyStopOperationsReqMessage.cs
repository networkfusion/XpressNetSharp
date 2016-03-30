using System;

namespace XpressNetSharp
{
    /// <summary>
    /// Tells the command station to immediately stop sending DCC packets and to switch off power to the track.
    /// </summary>
    public class EmergencyStopOperationsReqMessage : Message
    {
        /// <summary>
        /// Tells the command station to immediately stop sending DCC packets and to switch off power to the track.
        /// </summary>
        public EmergencyStopOperationsReqMessage() 
            : base(PacketHeaderType.CommandStationOperationsRequest) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.CommandStationOperationRequest.StopOperations));
        }

        protected override bool ValidResponse(Packet packet)
        {
            if (packet.Header == PacketHeaderType.CommandStationOperationResponse && 
                packet.Payload[0] == Convert.ToByte(PacketIdentifier.CommandStationOperationResponse.TrackPowerOffBroadcast))
            {
                ResponseData = new GeneralResponse();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
