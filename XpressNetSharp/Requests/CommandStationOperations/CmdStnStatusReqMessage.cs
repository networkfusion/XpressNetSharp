using System;

namespace XpressNetSharp
{
    /// <summary>
    /// Requests the command station to respond with it's current status.
    /// </summary>
    public class CmdStnStatusReqMessage : Message
    {
        /// <summary>
        /// Requests the command station to respond with it's current status.
        /// </summary>
        public CmdStnStatusReqMessage() 
            : base(PacketHeaderType.CommandStationOperationsRequest) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.CommandStationOperationRequest.Status));
        }

        protected override bool ValidResponse(Packet packet)
        {
            if (packet.Header == PacketHeaderType.CommandStationOperationResponse && 
                packet.Payload[0] == Convert.ToByte(PacketIdentifier.CommandStationOperationResponse.CmdStnStatus))
            {
                ResponseData = new CmdStnStatusResp();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
