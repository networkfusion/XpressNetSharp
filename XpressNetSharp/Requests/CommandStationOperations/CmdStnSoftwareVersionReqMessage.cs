using System;

namespace XpressNetSharp
{
    /// <summary>
    /// Requests the command station to respond with it's version number.
    /// </summary>
    public class CmdStnSoftwareVersionReqMessage : Message
    {
        /// <summary>
        /// Requests the command station to respond with it's version number.
        /// </summary>
        public CmdStnSoftwareVersionReqMessage() 
            : base(PacketHeaderType.CommandStationOperationsRequest) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.CommandStationOperationRequest.SoftwareVersion));
        }

        protected override bool ValidResponse(Packet packet)
        {
            if (packet.Header == PacketHeaderType.CommandStationOperationResponse && 
                packet.Payload[0] == Convert.ToByte(PacketIdentifier.CommandStationOperationResponse.CmdStnSoftwareVersion))
            {
                ResponseData = new CmdStnSoftwareVersionResp();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
