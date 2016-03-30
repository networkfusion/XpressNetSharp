using System;

namespace XpressNetSharp.Requests.LocomotiveFunctions.AddressInquiry.MultiUnit
{
    /// <summary>
    /// Requests the next base address of an mtr.
    /// </summary>
    /// <remarks>
    /// If the MTR is not known, set the mtr to 0 and the first MTR will be
    /// returned.
    /// </remarks>
    public class ForwardSearchMessage : Message
    {
        /// <summary>
        /// Requests the next base address of an mtr.
        /// </summary>
        /// <remarks>
        /// If the MTR is not known, set the mtr to 0 and the first MTR will be
        /// returned.
        /// </remarks>
        /// <param name="mtr">The multi-unit identifier (0 - 99)</param>
        public ForwardSearchMessage(int mtr) 
            : base(PacketHeaderType.LocomotiveFunction) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.LocomotiveFunctionRequest.AddressInquiryMultiUnit_ForwardSearch));

            if (mtr >= (XpressNetConstants.MIN_MTR - 1) && mtr <= XpressNetConstants.MAX_MTR)
                Payload.Add(Convert.ToByte(mtr));
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");
        }

        protected override bool ValidResponse(Packet packet)
        {
            if (packet.Header == PacketHeaderType.LocomotiveFunction && (packet.Payload[0] >= 0x30 && packet.Payload[0] <= 0x34)) //TODO: unit test to check this works
            {
                ResponseData = new AddressInquiryResp();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
