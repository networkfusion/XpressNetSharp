using System;

namespace XpressNetSharp
{
    /// <summary>
    /// Tells the command station to switch into service mode and write the specified value to the decoder that is on the programming track
    /// using direct mode.
    /// </summary>
    /// <remarks>
    /// Before a write instruction is used, the command station should be shifted into programming mode by issuing a direct mode read instruction.
    /// This should be used in order to determine that this mode is supported before issuing a write instruction.
    /// </remarks>
    public class DirectModeWriteReqMessage : Message
    {
        /// <summary>
        /// Tells the command station to switch into service mode and write the specified value to the decoder that is on the programming track
        /// using direct mode.
        /// </summary>
        /// <remarks>
        /// Before a write instruction is used, the command station should be shifted into programming mode by issuing a direct mode read instruction.
        /// This should be used in order to determine that this mode is supported before issuing a write instruction.
        /// </remarks>
        /// <param name="cv">CV (1 - 256)</param>
        /// <param name="value">Value to write</param>
        public DirectModeWriteReqMessage(int cv, int value) 
            : base(PacketHeaderType.CommandStationOperationsRequest)
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.CommandStationOperationRequest.DirectModeWrite));

            if (cv >= XpressNetConstants.MIN_CV && cv <= XpressNetConstants.MAX_CV_8_BIT)
                Payload.Add(cv == 256 ? (byte)0x00 : Convert.ToByte(cv));
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");


            if (value >= 0 && value < 256)
                Payload.Add(Convert.ToByte(value));
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");
        }

        //TODO: check that this actually happens as the specification is a little ambigous
        protected override bool ValidResponse(Packet packet)
        {
            if (packet.Header == PacketHeaderType.CommandStationOperationResponse && 
                packet.Payload[0] == Convert.ToByte(PacketIdentifier.CommandStationOperationResponse.ServiceModeEntryBroadcast))
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
