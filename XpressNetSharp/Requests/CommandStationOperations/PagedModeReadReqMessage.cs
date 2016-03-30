using System;

namespace XpressNetSharp
{
    /// <summary>
    /// Tells the command station to switch into service mode and read the decoder that is on the programming track using paged mode.
    /// </summary>
    /// <remarks>
    /// The read instruction does not require an answer by the command station! A request must be specifically requested with the 
    /// <see cref="ServiceModeResultsReqMessage"/>. Only after receiving a response to that message will determine whether the command was
    /// successful or not, or if the decoder supports paged CV mode. If the decoder could not be read using direct CV mode, the command
    /// station may try <seealso cref="RegisterModeReadReqMessage"/>. The device sending the request should examine the results to work out
    /// which mode was used to read the CV.
    /// 
    /// When the command station receives this message it will send out a "service mode entry" broadcast to all network participants which
    /// tells them to stop sending further instructions. Only the device that initiated the read request should continue to send service mode
    /// requests until the device instructs it to exit service mode.
    /// 
    /// It is the responsibility of the command station to make the appropriate conversion of the CV into page and offset, and to set the
    /// page register appropriately before attempting to read the value.
    /// </remarks>
    public class PagedModeReadReqMessage : Message
    {
        /// <summary>
        /// Tells the command station to switch into service mode and read the decoder that is on the programming track using paged mode.
        /// </summary>
        /// <remarks>
        /// The read instruction does not require an answer by the command station! A request must be specifically requested with the 
        /// <see cref="ServiceModeResultsReqMessage"/>. Only after receiving a response to that message will determine whether the command was
        /// successful or not, or if the decoder supports paged CV mode. If the decoder could not be read using direct CV mode, the command
        /// station may try <seealso cref="RegisterModeReadReqMessage"/>. The device sending the request should examine the results to work out
        /// which mode was used to read the CV.
        /// 
        /// When the command station receives this message it will send out a "service mode entry" broadcast to all network participants which
        /// tells them to stop sending further instructions. Only the device that initiated the read request should continue to send service mode
        /// requests until the device instructs it to exit service mode.
        /// 
        /// It is the responsibility of the command station to make the appropriate conversion of the CV into page and offset, and to set the
        /// page register appropriately before attempting to read the value.
        /// </remarks>
        /// <param name="cv">CV (1 - 256)</param>
        public PagedModeReadReqMessage(ushort cv) 
            : base(PacketHeaderType.CommandStationOperationsRequest) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.CommandStationOperationRequest.PagedModeRead));

            if (cv >= XpressNetConstants.MIN_CV && cv <= XpressNetConstants.MAX_CV_8_BIT)
            {
                Payload.Add(cv == XpressNetConstants.MAX_CV_8_BIT ? (byte)0x00 : Convert.ToByte(cv));
            }
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
