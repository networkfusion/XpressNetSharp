using System;

namespace XpressNetSharp
{
    /// <summary>
    /// Tells the command station to switch into service mode and write the specified value to the decoder that is on the programming track
    /// using paged mode.
    /// </summary>
    /// <remarks>
    /// It is not possible to determine whether a decoder supports paged mode, therefore care must be taken as unexpected  results can occur 
    /// if it does not.
    /// 
    /// It is the responsibility of the command station to make the appropriate conversion of the CV into page and offset, and to set the
    /// page register appropriately before attempting to read the value.
    /// </remarks>
    public class PagedModeWriteReqMessage : Message
    {
        /// <summary>
        /// Tells the command station to switch into service mode and write the specified value to the decoder that is on the programming track
        /// using paged mode.
        /// </summary>
        /// <remarks>
        /// It is not possible to determine whether a decoder supports paged mode, therefore care must be taken as unexpected  results can occur 
        /// if it does not.
        /// 
        /// It is the responsibility of the command station to make the appropriate conversion of the CV into page and offset, and to set the
        /// page register appropriately before attempting to read the value.
        /// </remarks>
        /// <param name="cv">CV (1 - 256)</param>
        /// <param name="value">Value to write</param>
        public PagedModeWriteReqMessage(int cv, int value) 
            : base(PacketHeaderType.CommandStationOperationsRequest)
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.CommandStationOperationRequest.PagedModeWrite));

            if (cv >= XpressNetConstants.MIN_CV && cv <= XpressNetConstants.MAX_CV_8_BIT)
                Payload.Add(cv == XpressNetConstants.MAX_CV_8_BIT ? (byte)0x00 : Convert.ToByte(cv));
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
