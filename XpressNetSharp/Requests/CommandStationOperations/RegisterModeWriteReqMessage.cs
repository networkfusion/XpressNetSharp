using System;

namespace XpressNetSharp
{
    /// <summary>
    /// Tells the command station to switch into service mode and write the specified value to the decoder that is on the programming track
    /// using register mode.
    /// </summary>
    /// <remarks>
    /// Before a write instruction is used, the command station should be shifted into programming mode by issuing a read instruction.
    /// </remarks>
    public class RegisterModeWriteReqMessage : Message
    {
        /// <summary>
        /// Tells the command station to switch into service mode and write the specified value to the decoder that is on the programming track
        /// using register mode.
        /// </summary>
        /// <remarks>
        /// Before a write instruction is used, the command station should be shifted into programming mode by issuing a read instruction.
        /// </remarks>
        /// <param name="register">Register (1 - 8)</param>
        /// <param name="value">Value to write</param>
        public RegisterModeWriteReqMessage(int register, int value) 
            : base(PacketHeaderType.CommandStationOperationsRequest) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.CommandStationOperationRequest.RegisterModeWrite));

            if (register >= 0 && register < 9)
                Payload.Add(Convert.ToByte(register));
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");

            
            if (value >= 0 && value < 256)
            {
                    Payload.Add(Convert.ToByte(value));
            }
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
