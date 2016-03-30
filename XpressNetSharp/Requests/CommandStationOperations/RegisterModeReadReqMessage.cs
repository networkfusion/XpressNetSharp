using System;

namespace XpressNetSharp
{
    /// <summary>
    /// Tells the command station to switch into service mode and read the decoder that is on the programming track using register mode.
    /// </summary>
    /// <remarks>
    /// The read instruction does not require an answer by the command station! A request must be specifically requested with the 
    /// <see cref="ServiceModeResultsReqMessage"/>. Only after receiving a response to that message will determine whether the command was
    /// successful or not.
    /// 
    /// When the command station receives this message it will send out a "service mode entry" broadcast to all network participants which
    /// tells them to stop sending further instructions. Only the device that initiated the read request should continue to send service mode
    /// requests until the device instructs it to exit service mode.
    /// </remarks>
    public class RegisterModeReadReqMessage : Message
    {
        /// <summary>
        /// Tells the command station to switch into service mode and read the decoder that is on the programming track using register mode.
        /// </summary>
        /// <remarks>
        /// The read instruction does not require an answer by the command station! A request must be specifically requested with the 
        /// <see cref="ServiceModeResultsReqMessage"/>. Only after receiving a response to that message will determine whether the command was
        /// successful or not.
        /// 
        /// When the command station receives this message it will send out a "service mode entry" broadcast to all network participants which
        /// tells them to stop sending further instructions. Only the device that initiated the read request should continue to send service mode
        /// requests until the device instructs it to exit service mode.
        /// </remarks>
        /// <param name="register">Register (1 - 8)</param>
        public RegisterModeReadReqMessage(ushort register) 
            : base(PacketHeaderType.CommandStationOperationsRequest) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.CommandStationOperationRequest.RegisterModeRead));

            if (register > 0 && register < 9)
                Payload.Add(Convert.ToByte(register));
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");
        }

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
