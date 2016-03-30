using System;

namespace XpressNetSharp
{
    /// <summary>
    /// Tells the command station to turn on power to the track (if it was switched off) and begin transmition
    /// of DCC packets to the track.
    /// </summary>
    /// <remarks>
    /// This causes termination of an emergency stop, an emergency off or the completion of programming on the
    /// programming track (service mode operations).
    /// </remarks>
    public class ResumeOperationsReqMessage : Message
    {
        /// <summary>
        /// Tells the command station to turn on power to the track (if it was switched off) and begin transmition
        /// of DCC packets to the track.
        /// </summary>
        /// <remarks>
        /// This causes termination of an emergency stop, an emergency off or the completion of programming on the
        /// programming track (service mode operations).
        /// </remarks>
        public ResumeOperationsReqMessage() 
            : base(PacketHeaderType.CommandStationOperationsRequest) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.CommandStationOperationRequest.ResumeOperations));
        }

        protected override bool ValidResponse(Packet packet)
        {
            if (packet.Header == PacketHeaderType.CommandStationOperationResponse && 
                packet.Payload[0] == Convert.ToByte(PacketIdentifier.CommandStationOperationResponse.NormalOperationsResumedBroadcast))
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
