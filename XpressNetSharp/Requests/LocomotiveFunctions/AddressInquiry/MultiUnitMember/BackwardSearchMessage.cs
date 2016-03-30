using System;

namespace XpressNetSharp.Requests.LocomotiveFunctions.AddressInquiry.MultiUnitMember
{
    /// <summary>
    /// Requests the previous member in a multi-unit consist.
    /// </summary>
    /// <remarks>
    /// Not all command stations implement this instruction, so it should be used with care.
    /// 
    /// If the locomotive addresses within an MTR is not known, set the address to 0 and the last address within the MTR will be
    /// returned.
    /// </remarks>
    public class BackwardSearchMessage : Message
    {
        /// <summary>
        /// Requests the previous member in a multi-unit consist.
        /// </summary>
        /// <remarks>
        /// Not all command stations implement this instruction, so it should be used with care.
        /// 
        /// If the locomotive addresses within an MTR is not known, set the address to 0 and the last address within the MTR will be
        /// returned.
        /// </remarks>
        /// <param name="mtr">The multi-unit identifier (1 - 99)</param>
        /// <param name="address">Address of the locomotive (0 - 9999)</param>
        public BackwardSearchMessage(int mtr, int address) 
            : base(PacketHeaderType.LocomotiveFunction) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.LocomotiveFunctionRequest.AddressInquiryMultiUnitMember_BackwardSearch));

            byte[] data = new byte[3];

            if (mtr >= XpressNetConstants.MIN_MTR && mtr <= XpressNetConstants.MAX_MTR)
                data[0] = (byte)mtr;
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");

            if (address >= XpressNetConstants.MIN_LOCO_ADDRESS && address <= XpressNetConstants.MAX_LOCO_ADDRESS)
                ValueConverter.LocoAddress(address, out data[1], out data[2]);
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");

            Payload.AddRange(data);
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
